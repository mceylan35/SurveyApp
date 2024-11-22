import { Component, OnInit } from '@angular/core';
import { SurveyService } from '../../../core/services/survey.service';
import { Survey } from '../../../core/models/survey.models';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

@Component({
  selector: 'app-survey-list',
  templateUrl: './survey-list.component.html',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatProgressSpinnerModule
  ]
})
export class SurveyListComponent implements OnInit {
  surveys: Survey[] = [];
  loading = false;
  error = '';
  currentPage = 1;
  pageSize = 10;
  totalPages = 0;

  constructor(private surveyService: SurveyService) { }

  ngOnInit(): void {
    this.loadSurveys();
  }

  loadSurveys(): void {
    this.loading = true;
    this.surveyService.getSurveys(this.currentPage, this.pageSize)
      .subscribe({
        next: (response) => {
          if (response.isSuccess) {
          this.surveys = response.value.items;
          this.totalPages = response.value.totalPages;
          this.loading = false;
        } else {
          this.error = response.error;
          this.loading = false;
        }
        },
        error: error => {
          this.error = error.message || 'An error occurred while loading surveys';
          this.loading = false;
        }
      });
  }

  nextPage(): void {
    debugger;
    if (this.currentPage < this.totalPages) {
      this.currentPage++;
      this.loadSurveys();
    }
  }

  previousPage(): void {
    debugger;
    if (this.currentPage > 1) {
      this.currentPage--;
      this.loadSurveys();
    }
  }
}