import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SurveyListComponent } from './survey-list/survey-list.component';
import { SurveyCreateComponent } from './survey-create/survey-create.component';
import { SurveyDetailComponent } from './survey-detail/survey-detail.component';
import { AuthGuard } from '../../core/guards/auth.guard';

 
export const SURVEY_ROUTES: Routes = [
  { path: '', loadComponent: () => import('./survey-list/survey-list.component').then(m => m.SurveyListComponent) },
  {
    path: 'create', 
    loadComponent: () => import('./survey-create/survey-create.component').then(m => m.SurveyCreateComponent),
    canActivate: [AuthGuard]
  },
  { path: ':id', loadComponent: () => import('./survey-detail/survey-detail.component').then(m => m.SurveyDetailComponent) }
];

@NgModule({
  imports: [RouterModule.forChild(SURVEY_ROUTES)],
  exports: [RouterModule]
})
export class SurveysRoutingModule { }
