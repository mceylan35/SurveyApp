import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpErrorResponse } from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { AuthService } from '../services/auth.service';
import { environment } from '../../../environments/environment';
import { Router  } from '@angular/router';
@Injectable()
export class JwtInterceptor implements HttpInterceptor { 
  constructor(private authService: AuthService, private router: Router) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    
    const currentUser = this.authService.currentUserValue;
    const isApiUrl = request.url.startsWith(environment.apiUrl);

    if (currentUser && isApiUrl) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${currentUser.value.token}`
        }
      });
    }

    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse) => {
       
        if (error.status === 401) {
         
          this.authService.logout(); 
          this.router.navigate(['/auth/login']); 
        }
        
        return throwError(() => error);
      })
    );
  }
}
