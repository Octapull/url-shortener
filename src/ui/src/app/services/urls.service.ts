import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface UrlPanelDto {
  shortCode: string;
  longUrl: string;
  createdAt: string;
  clicks: number;
  status: 'Active' | 'Paused' | 'Disabled';
}

@Injectable({ providedIn: 'root' })
export class UrlsService {
  private readonly base = 'http://localhost:5051';

  constructor(private http: HttpClient) {}

  private authHeaders(token?: string): HttpHeaders | undefined {
    return token ? new HttpHeaders({ Authorization: `Bearer ${token}` }) : undefined;
  }

  getUserUrls(token?: string): Observable<UrlPanelDto[]> {
    return this.http.get<UrlPanelDto[]>(`${this.base}/api/user/urls`, { headers: this.authHeaders(token) });
  }

  getAdminUrls(token?: string): Observable<UrlPanelDto[]> {
    return this.http.get<UrlPanelDto[]>(`${this.base}/api/admin/urls`, { headers: this.authHeaders(token) });
  }

  deleteUrl(code: string, isAdmin: boolean, token?: string): Observable<void> {
    const path = isAdmin ? `/api/admin/urls/delete/${code}` : `/api/user/urls/delete/${code}`;
    return this.http.delete<void>(`${this.base}${path}`, { headers: this.authHeaders(token) });
  }

  disableUrl(code: string, isAdmin: boolean, token?: string): Observable<void> {
    const path = isAdmin ? `/api/admin/urls/disable/${code}` : `/api/user/urls/disable/${code}`;
    return this.http.post<void>(`${this.base}${path}`, {}, { headers: this.authHeaders(token) });
  }

  shortenUrl(longUrl: string, token?: string): Observable<{ shortUrl: string }> {
    return this.http.post<{ shortUrl: string }>(`${this.base}/api/shorten`, { LongUrl: longUrl }, { headers: this.authHeaders(token) });
  }
}
