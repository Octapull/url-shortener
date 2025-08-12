import { Component } from '@angular/core';
import { UrlShortenerComponent } from './components/url-shortener/url-shortener.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [UrlShortenerComponent],
  template: `
    <img src="/octapull-logo.png" alt="Octapull" class="brand logo" />
    <div class="center-layout">
      <app-url-shortener></app-url-shortener>
    </div>
  `
})
export class App {
}