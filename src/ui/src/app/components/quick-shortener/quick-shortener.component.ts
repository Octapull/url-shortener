import { Component, EventEmitter, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ShortenerService } from '../../services/shortener.service';

@Component({
  selector: 'app-quick-shortener',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './quick-shortener.component.html',
  styleUrls: ['./quick-shortener.component.css']
})
export class QuickShortenerComponent {
  longUrl = '';
  shortUrl: string | null = null;
  isLoading = false;
  @Output() shorten = new EventEmitter<string>();

  constructor(private shortener: ShortenerService) {}

  onShorten(): void {
    const url = this.longUrl?.trim();
    if (!url || this.isLoading) { return; }
    this.isLoading = true;
    this.shortener.shorten(url).subscribe({
      next: (res) => {
        this.shortUrl = res.shortUrl;
        this.isLoading = false;
        this.shorten.emit(url);
      },
      error: () => { this.isLoading = false; }
    });
  }

  copy(): void {
    if (!this.shortUrl) return;
    navigator.clipboard.writeText(this.shortUrl);
  }

  shortenAnother(): void {
    this.longUrl = '';
    this.shortUrl = null;
  }

  shareUrl(network: 'facebook' | 'twitter' | 'reddit'): void {
    if (!this.shortUrl) return;
    const enc = encodeURIComponent(this.shortUrl);
    let href = '';
    if (network === 'facebook') href = `https://www.facebook.com/sharer/sharer.php?u=${enc}`;
    if (network === 'twitter') href = `https://twitter.com/intent/tweet?url=${enc}`;
    if (network === 'reddit') href = `https://reddit.com/submit?url=${enc}`;
    window.open(href, '_blank');
  }
}
