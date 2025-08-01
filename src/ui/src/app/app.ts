import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  template: `<h1>Hello, {{ title() }}</h1>`,
  styles: [`
    h1 {
      color: blue;
    }
  `]
})
export class App {
  protected readonly title = signal('url-shortener-ui');
}