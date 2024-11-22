import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormArray, Validators, ReactiveFormsModule, FormControl } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { SurveyService } from '../../../core/services/survey.service';
import { AlertService } from '../../../core/services/alert.service';
import { CreateSurveyRequest } from '../../../core/models/survey.models';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

@Component({
  selector: 'app-survey-create',
  templateUrl: './survey-create.component.html',
  styleUrls: ['./survey-create.component.css'],
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule,
    MatCardModule,
    MatInputModule,
    MatFormFieldModule,
    MatButtonModule,
    MatIconModule,
    MatProgressSpinnerModule
  ]
})
export class SurveyCreateComponent implements OnInit {
  surveyForm: FormGroup | any;
  loading = false;

  constructor(
    private formBuilder: FormBuilder,
    private surveyService: SurveyService,
    private router: Router,
    private alertService: AlertService
  ) { }

  ngOnInit(): void {
    this.surveyForm = this.formBuilder.group({
      title: ['', [Validators.required, Validators.minLength(3)]],
      options: this.formBuilder.array([
        this.formBuilder.control('', Validators.required),
        this.formBuilder.control('', Validators.required)
      ])
    });
    this.surveyForm.valueChanges.subscribe(() => {
      console.log('Form validity:', this.surveyForm.valid);
    });
  }

  

  get isFormValid(): boolean {
    return this.surveyForm?.valid && !this.loading;
  }
  get options(): FormArray {
    return this.surveyForm.get('options') as FormArray;
  }

  createOption(): FormGroup | any{
    return this.formBuilder.control('', Validators.required);
  }

  addOption(): void {
    if (this.options.length < 10) { // Maksimum 10 seçenek
      this.options.push(this.createOption());
    } else {
      this.alertService.warning('Maximum 10 options allowed');
    }
  }

  removeOption(index: number): void {
    if (this.options.length > 2) {
      this.options.removeAt(index);
    }
  }

  onSubmit(): void {
    if (this.surveyForm.invalid) {
      this.alertService.error('Please fill all required fields correctly');
      return;
    }
    
    this.loading = true;
    const request: CreateSurveyRequest = {
      title: this.surveyForm.get('title')?.value,
      options: this.options.value,
    };

    this.surveyService.createSurvey(request)
      .subscribe({
        next: () => {
          this.alertService.success('Survey created successfully').then(() => {
            this.router.navigate(['/surveys']);
          });
        },
        error: error => {
          let errorMessage = 'Failed to create survey';
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