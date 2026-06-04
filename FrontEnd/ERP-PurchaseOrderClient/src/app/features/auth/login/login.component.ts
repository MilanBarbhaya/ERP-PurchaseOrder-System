import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { AuthService } from '../../../core/services/auth/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './login.component.html',
   styleUrls: ['./login.component.css']
})

export class LoginComponent {

  private fb = inject(FormBuilder);
  private authService = inject(AuthService);
  private router = inject(Router);

  form = this.fb.group({
    username: [''],
    password: ['']
  });

test(){
     console.log('Login Clicked');
}

  login() {
     console.log('Login Clicked');

    this.authService
      .login(this.form.value)
      .subscribe({
        next: (response) => {

          // JWT Storage
          localStorage.setItem(
            'token',
            response.token
          );

          localStorage.setItem(
            'username',
            response.username
          );

          localStorage.setItem(
            'role',
            response.role
          );

          console.log('Login Success');
          console.log(response);
          this.router.navigate(['/purchase-orders']);

        },
        error: (error) => {
          console.error(error);
          alert('Invalid username or password');
        }
      });
  }
}