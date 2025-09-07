import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class ShortenerService {
  private readonly apiUrl = 'http://localhost:5051/api/shorten';

  constructor(private http: HttpClient) {}

  shorten(longUrl: string, recaptchaToken?: string | null): Observable<{ shortUrl: string }> {
    const payload: any = { LongUrl: longUrl };
    if (recaptchaToken) { payload.RecaptchaToken = recaptchaToken; }
    return this.http.post<{ shortUrl: string }>(this.apiUrl, payload);
  }
}
