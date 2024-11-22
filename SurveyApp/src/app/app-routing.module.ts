import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './core/guards/auth.guard';
import { LoginComponent } from './features/auth/login/login.component';
import { RegisterComponent } from './features/auth/register/register.component';

export const routes: Routes = [
  {
    path: 'auth',
    children: [
      { path: '', redirectTo: 'login', pathMatch: 'full' },
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterComponent }
    ]  
  },
  {
    path: 'surveys',
    loadChildren: () => import('./features/surveys/surveys-routing.module').then(m => m.SURVEY_ROUTES)
  },
  { path: '**', redirectTo: 'auth/login' } 
];