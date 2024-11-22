import { Component, OnInit, ViewChild, ElementRef, AfterViewInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { Chart, registerables } from 'chart.js'; // registerables'ı import edin
import { SurveyService } from '../../../core/services/survey.service';
import { AuthService } from '../../../core/services/auth.service';
import { AlertService } from '../../../core/services/alert.service';
import { Survey } from '../../../core/models/survey.models';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatRadioModule } from '@angular/material/radio';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

// Chart.js'i kaydedin
Chart.register(...registerables);

@Component({
  selector: 'app-survey-detail',
  templateUrl: './survey-detail.component.html',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    RouterModule,
    MatCardModule,
    MatRadioModule,
    MatButtonModule,
    MatProgressSpinnerModule
  ]
})
export class SurveyDetailComponent implements OnInit, AfterViewInit {
  @ViewChild('surveyChart') surveyChart!: ElementRef; 
  
  survey!: Survey;
  loading = false;
  error = '';
  selectedOptionId = '';
  chart: Chart | undefined;

  constructor(
    public route: ActivatedRoute,
    public surveyService: SurveyService,
    public authService: AuthService,
    private alertService: AlertService
  ) { }

  ngOnInit(): void {
    const surveyId = this.route.snapshot.paramMap.get('id');
    if (surveyId) {
      this.loadSurvey(surveyId);
    }
  }

  ngAfterViewInit() {
    // Survey yüklendiğinde chart'ı oluştur
    if (this.survey?.totalVotes > 0) {
      this.createChart();
    }
  }

  loadSurvey(id: string): void {
    this.loading = true;
    this.surveyService.getSurveyById(id)
      .subscribe({
        next: (response) => {
          if (response.isSuccess) {
            this.survey = response.value;
          
            if (this.survey.totalVotes > 0) {
              setTimeout(() => {
                this.createChart();
              }, 0);
            }
           
          } else {
            this.error = response.error;
            this.alertService.error(response.error);
          }
          this.loading = false;
        },
        error: error => {
          let errorMessage = 'Failed to load survey';
          if (error?.error) {
            errorMessage = typeof error.error === 'string' 
              ? error.error 
              : error.error.error || error.error.message;
          }
          this.error = errorMessage;
          this.alertService.error(errorMessage);
          this.loading = false;
        }
      });
  }

  createChart(): void {
    if (this.chart) {
      this.chart.destroy();
    }

    if (!this.surveyChart) return;

    const ctx = this.surveyChart.nativeElement.getContext('2d');
    
    this.chart = new Chart(ctx, {
      type: 'bar',
      data: {
        labels: this.survey.options.map(o => o.text),
        datasets: [{
          label: 'Votes',
          data: this.survey.options.map(o => o.voteCount),
          backgroundColor: 'rgba(54, 162, 235, 0.5)',
          borderColor: 'rgba(54, 162, 235, 1)',
          borderWidth: 1
        }]
      },
      options: {
        responsive: true,
        scales: {
          y: {
            beginAtZero: true,
            ticks: {
              stepSize: 1
            }
          }
        },
        plugins: {
          legend: {
            display: false
          }
        }
      }
    });
  }

  submitVote(): void {
    if (!this.selectedOptionId) {
      this.alertService.warning('Please select an option');
      return;
    }

    this.loading = true;
    this.surveyService.vote({
      surveyId: this.survey.id,
      optionId: this.selectedOptionId
    }).subscribe({
      next: () => {
        this.alertService.success('Vote submitted successfully');
        this.loadSurvey(this.survey.id);
      },
      error: error => {
        let errorMessage = 'Failed to submit vote';
        if (error?.error) {
          errorMessage = typeof error.error === 'string' 
            ? error.error 
            : error.error.error || error.error.message;
        }
        this.error = errorMessage;
        this.alertService.error(errorMessage);
        this.loading = false;
      }
    });
  }
}