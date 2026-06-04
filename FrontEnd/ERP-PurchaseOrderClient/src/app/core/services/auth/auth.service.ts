import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LoginResponse } from '../../../core/models/login-response';
import { Router } from '@angular/router';
@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private router = inject(Router);
  private http = inject(HttpClient);

  private apiUrl =
    'http://localhost:5237/api/auth';

  login(data: any): Observable<LoginResponse> {

  return this.http.post<LoginResponse>(
    `${this.apiUrl}/login`,
    data
  );
}
logout(): void {

    // remove auth data
    localStorage.removeItem('token');
    localStorage.removeItem('username');
    localStorage.removeItem('role');

    // redirect to login page
    this.router.navigate(['/login']);
  }
}