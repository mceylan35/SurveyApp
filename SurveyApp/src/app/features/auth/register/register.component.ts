import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { first } from 'rxjs/operators';
import { AuthService } from '../../../core/services/auth.service';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { BehaviorSubject } from 'rxjs';
import { AuthResponse } from '../../../core/models/auth.models';
import { AlertService } from '../../../core/services/alert.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatInputModule,
    MatButtonModule,
    MatProgressSpinnerModule,
    RouterModule
  ]
})
export class RegisterComponent implements OnInit{
  registerForm: FormGroup | any;
  loading = false;
  submitted = false;
  error = ''; 
  currentUser$: any;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private authService: AuthService,
    private alertService: AlertService
  ) { 
  }

  ngOnInit() {
    
    this.registerForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', Validators.required]
    });
  
    this.registerForm.addValidators(
      this.passwordMatchValidator.bind(this)
    );
  }

  get f() { return this.registerForm.controls; }

  passwordMatchValidator(g: FormGroup) {
    
    const password = g.get("password")?.value;
    const confirmPassword = g.get("confirmPassword")?.value;
    return password === confirmPassword ? null : { 'mismatch': true };
  }

  onSubmit() { 
    this.submitted = true;

    if (this.registerForm.invalid) {
      this.alertService.error("Form Not Valid");
      return;
    }

    this.loading = true;
    this.authService.register({
      email: this.f['email'].value,
      password: this.f['password'].value
    })
    .pipe(first())
    .subscribe({
      next: () => {
        this.alertService.success('Registration successful!').then(() => {
          this.router.navigate(['/auth/login']);
        });
      },
      error: error => {
        let errorMessage = 'Registration failed';
       if (error?.error) {
         errorMessage = typeof error.error === 'string' 
           ? error.error 
           : error.error.error || error.error.message;
       }
       this.alertService.error(errorMessage);
       this.loading = false;
      }
    });
  }
}
