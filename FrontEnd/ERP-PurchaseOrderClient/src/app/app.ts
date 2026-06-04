import { Component, signal,inject  } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { AuthService } from './core/services/auth/auth.service';

@Component({
  selector: 'app-root',
  imports: [CommonModule,RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('ERP-PurchaseOrderClient');
    private authService = inject(AuthService);
role =
    localStorage.getItem(
      'role'
    );
  logout() {
    this.authService.logout();
  }
}
