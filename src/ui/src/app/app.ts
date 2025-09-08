import { Component, signal, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { UrlShortenerComponent } from './components/url-shortener/url-shortener.component';
import { AdminPanelComponent, AdminLinkItem } from './components/admin-panel/admin-panel.component';
import { QuickShortenerComponent } from './components/quick-shortener/quick-shortener.component';
import { UrlsService, UrlPanelDto } from './services/urls.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterModule, UrlShortenerComponent, AdminPanelComponent, QuickShortenerComponent],
  templateUrl: './app.html',
  styleUrls: ['./app.css']
})
export class App implements OnDestroy {
  readonly isSignedIn = signal<boolean>(false);
  readonly panelExpanded = signal<boolean>(false);
  readonly username = signal<string>('Username');
  readonly token = signal<string | null>(null);
  readonly isAdmin = signal<boolean>(false);
  readonly links = signal<AdminLinkItem[]>([]);

  private onAuthLogout = () => {
    this.token.set(null);
    this.isSignedIn.set(false);
    this.isAdmin.set(false);
    this.username.set('Username');
    this.links.set([]);
    this.panelExpanded.set(false);
  };

  constructor(private urls: UrlsService) {
    const saved = localStorage.getItem('auth_token');
    if (saved) {
      this.token.set(saved);
      try {
        const payload = JSON.parse(atob(saved.split('.')[1] || '')) as Record<string, unknown>;
        const role = (payload['role'] || payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']) as string | undefined;
        this.isAdmin.set(role === 'Admin');
        this.username.set((payload['name'] as string) || (payload['email'] as string) || 'User');
        this.isSignedIn.set(true);
        this.refreshUrls();
      } catch {}
    }
    window.addEventListener('message', this.messageHandler);
    window.addEventListener('auth:logout', this.onAuthLogout);
  }

  ngOnDestroy(): void {
    window.removeEventListener('message', this.messageHandler);
    window.removeEventListener('auth:logout', this.onAuthLogout);
  }

  onGoogleSignIn(): void {
    const url = 'http://localhost:5051/auth/google-login';
    window.open(url, '_blank', 'width=500,height=600');
  }

  onLogout(): void {
    localStorage.removeItem('auth_token');
    this.onAuthLogout();
  }

  private messageHandler = (event: MessageEvent) => {
    if (!event?.data || typeof event.data !== 'object') return;
    const { token, name, email } = event.data as { token?: string; name?: string; email?: string };
    if (!token) return;
    localStorage.setItem('auth_token', token);
    this.token.set(token);
    this.username.set(name || email || 'User');
    try {
      const payload = JSON.parse(atob(token.split('.')[1] || '')) as Record<string, unknown>;
      const role = (payload['role'] || payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']) as string | undefined;
      this.isAdmin.set(role === 'Admin');
    } catch {}
    this.isSignedIn.set(true);
    this.refreshUrls();
  };

  togglePanel(): void { if (!this.isSignedIn()) return; this.panelExpanded.set(!this.panelExpanded()); }
  closePanel(event?: Event): void { if (event) event.stopPropagation(); this.panelExpanded.set(false); }

  onQuickShorten(url: string): void {
    const token = this.token() || undefined;
    if (!url) { this.refreshUrls(); return; }
    if ((this.urls as any).shortenUrl) {
      (this.urls as any).shortenUrl(url, token).subscribe({
        next: () => this.refreshUrls(),
        error: () => this.refreshUrls()
      });
    } else {
      this.refreshUrls();
    }
  }

  private mapDtoToItem(d: UrlPanelDto | any): AdminLinkItem {
    return {
      shortCode: d.shortCode ?? d.ShortCode ?? d.code ?? d.Code ?? '',
      longUrl: d.longUrl ?? d.LongUrl ?? d.url ?? '',
      createdAt: d.createdAt ?? d.CreatedAt ?? d.created_at ?? '',
      clicks: d.clicks ?? d.Clicks ?? d.clickCount ?? d.ClickCount ?? 0,
      status: (d.status ?? d.Status ?? 'Active') as 'Active' | 'Paused' | 'Disabled'
    };
  }

  refreshUrls(): void {
    const token = this.token() || undefined;
    const handle = (list: UrlPanelDto[]) => this.links.set((list || []).map(l => this.mapDtoToItem(l)));
    if (this.isAdmin()) {
      this.urls.getAdminUrls(token).subscribe({ next: handle, error: () => this.urls.getUserUrls(token).subscribe({ next: handle }) });
    } else {
      this.urls.getUserUrls(token).subscribe({ next: handle, error: () => this.urls.getAdminUrls(token).subscribe({ next: handle }) });
    }
  }
}