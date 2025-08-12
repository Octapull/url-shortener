import { Component, AfterViewInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

declare global {
  interface Window { onRecaptchaLoad: () => void; grecaptcha: any; }
}

@Component({
  selector: 'app-url-shortener',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './url-shortener.component.html',
  styleUrls: ['./url-shortener.component.css']
})
export class UrlShortenerComponent implements AfterViewInit {
  longUrl = '';
  shortUrl: string | null = null;
  recaptchaToken: string | null = null;
  private recaptchaWidgetId: number | null = null;
  private recaptchaTries = 0;

  constructor(private http: HttpClient) {}

  ngAfterViewInit(): void {
    window.onRecaptchaLoad = () => this.renderRecaptcha();
    this.renderRecaptcha();
  }

  private renderRecaptcha(): void {
    if (this.recaptchaWidgetId !== null) return; 
    const container = document.getElementById('recaptcha-container');
    if (container && window.grecaptcha && window.grecaptcha.render) {
      const siteKey = '6LeIxAcTAAAAAJcZVRqyHh71UMIEGNQ_MXjiZKhI'; // Google public test key
      this.recaptchaWidgetId = window.grecaptcha.render(container, {
        sitekey: siteKey,
        callback: (token: string) => this.recaptchaToken = token,
        'expired-callback': () => this.recaptchaToken = null
      });
      return;
    }
    
    if (this.recaptchaTries < 20) {
      this.recaptchaTries++;
      setTimeout(() => this.renderRecaptcha(), 250);
    }
  }

  shortenUrl() {
    if (!this.longUrl) return;

    this.http.post<{ shortUrl: string }>(
      'http://localhost:5051/api/shorten',
      { LongUrl: this.longUrl }
    ).subscribe({
      next: (res) => this.shortUrl = res.shortUrl,
      error: (err) => console.error('Error:', err)
    });
  }

  copyToClipboard() {
    if (!this.shortUrl) return;

    navigator.clipboard.writeText(this.shortUrl).then(() => {
      const copyBtn = document.querySelector('.copy-btn') as HTMLElement;
      if (copyBtn) {
        copyBtn.classList.add('copied');
        setTimeout(() => {
          copyBtn.classList.remove('copied');
        }, 2000);
      }
    });
  }

  encodeURIComponent(value: string | null): string {
    return value ? encodeURIComponent(value) : '';
  }

  resetForm() {
    this.longUrl = '';
    this.shortUrl = null;
    this.recaptchaToken = null;
    this.recaptchaWidgetId = null;
    this.recaptchaTries = 0;
    setTimeout(() => this.renderRecaptcha(), 0);
  }
}
