import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, ActivatedRoute, RouterModule } from '@angular/router';
import { first } from 'rxjs/operators';
import { AuthService } from '../../../core/services/auth.service';
import { AlertService } from '../../../core/services/alert.service';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

@Component({
 selector: 'app-login',
 templateUrl: './login.component.html',
 styleUrls: ['./login.component.css'],
 standalone: true,
 imports: [
   CommonModule,
   ReactiveFormsModule,
   RouterModule,
   MatCardModule,
   MatInputModule,
   MatButtonModule,
   MatProgressSpinnerModule
 ]
})
export class LoginComponent implements OnInit {
 loginForm: FormGroup | any;
 loading = false;
 submitted = false;
 returnUrl: string | any;
 error:any;
 

 constructor(
   private formBuilder: FormBuilder,
   private route: ActivatedRoute,
   private router: Router,
   private authService: AuthService,
   private alertService: AlertService
 ) {
   if (this.authService.currentUserValue) {
     this.router.navigate(['/']);
   }
 }

 ngOnInit() {
   this.loginForm = this.formBuilder.group({
     email: ['', [Validators.required, Validators.email]],
     password: ['', Validators.required]
   });

   this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
 }

 get f() { return this.loginForm.controls; }

 onSubmit() {
   this.submitted = true;

   if (this.loginForm.invalid) {
     return;
   }

   this.loading = true;
   this.authService.login(this.loginForm.value)
     .pipe(first())
     .subscribe({
       next: () => {
         this.alertService.success('Login successful').then(() => {
          this.router.navigate(['/surveys']);
         });
       },
       error: error => {
        let errorMessage = 'Login failed';
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