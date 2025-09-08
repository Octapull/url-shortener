import { Component, ElementRef, EventEmitter, Input, OnDestroy, AfterViewInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UrlsService } from '../../services/urls.service';

export interface AdminLinkItem {
  shortCode: string;
  longUrl: string; // decode to full link
  createdAt: string; // ISO-like string
  clicks: number;
  status: 'Active' | 'Paused' | 'Disabled';
}

@Component({
  selector: 'app-admin-panel',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './admin-panel.component.html',
  styleUrls: ['./admin-panel.component.css']
})
export class AdminPanelComponent implements AfterViewInit, OnDestroy {
  @Input() links: AdminLinkItem[] = [];
  @Input() signedIn = false;
  @Input() expanded = false;
  @Input() isAdmin = false;
  @Input() token: string | null = null;

  @Output() toggle = new EventEmitter<void>();
  @Output() close = new EventEmitter<Event>();
  @Output() widthChange = new EventEmitter<number>();
  @Output() changed = new EventEmitter<void>();

  private ro?: ResizeObserver;

  sortAsc = false;

  constructor(private host: ElementRef<HTMLElement>, private urls: UrlsService) {}

  private readonly rowHeightPx = 44;
  private readonly headerHeightPx = 44;
  private readonly extraPaddingPx = 36;

  onToggle(): void { this.toggle.emit(); }
  onClose(ev: Event): void { this.close.emit(ev); }

  copyShort(item: AdminLinkItem): void {
    const base = 'http://localhost:5051';
    const url = `${base}/${item.shortCode}`;
    navigator.clipboard.writeText(url);
  }

  delete(item: AdminLinkItem): void {
    this.urls.deleteUrl(item.shortCode, this.isAdmin, this.token || undefined).subscribe(() => this.changed.emit());
  }

  disable(item: AdminLinkItem): void {
    this.urls.disableUrl(item.shortCode, this.isAdmin, this.token || undefined).subscribe(() => this.changed.emit());
  }

  get collapsedRowCount(): number {
    const n = this.links?.length ?? 0;
    return Math.min(n, 10);
  }

  get hasMore(): boolean {
    const n = this.links?.length ?? 0;
    return n > 10;
  }

  get collapsedHeightPx(): number {
    const hasItems = (this.links?.length ?? 0) > 0;
    const rows = this.collapsedRowCount > 0 ? this.collapsedRowCount : 1; // ensure space for empty state row
    const base = this.headerHeightPx + (rows * this.rowHeightPx);
    const extra = hasItems ? this.extraPaddingPx : (this.extraPaddingPx + 24); // extra room for hint when empty
    return base + extra;
  }

  get sortedLinks(): AdminLinkItem[] {
    const arr = [...(this.links ?? [])];
    arr.sort((a, b) => {
      const da = new Date(a.createdAt).getTime();
      const db = new Date(b.createdAt).getTime();
      return this.sortAsc ? (da - db) : (db - da);
    });
    return arr;
  }

  toggleSort(ev: Event): void {
    ev.stopPropagation();
    this.sortAsc = !this.sortAsc;
  }

  ngAfterViewInit(): void {
    const panel = this.host.nativeElement.querySelector('.admin-panel') as HTMLElement | null;
    if (!panel) return;
    const emit = () => this.widthChange.emit(panel.getBoundingClientRect().width);
    this.ro = new ResizeObserver(() => emit());
    this.ro.observe(panel);
    emit();
  }

  ngOnDestroy(): void {
    this.ro?.disconnect();
  }
}
