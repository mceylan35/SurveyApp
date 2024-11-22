import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Survey, CreateSurveyRequest, VoteRequest } from '../models/survey.models';

@Injectable({
  providedIn: 'root'
})
export class SurveyService {
  constructor(private http: HttpClient) {}

  getSurveys(pageNumber: number, pageSize: number): Observable<PaginatedResponse<Survey>> {
    const params = new HttpParams()
      .set('page', pageNumber.toString())
      .set('pageSize', pageSize.toString());

    return this.http.get<any>(`${environment.apiUrl}/surveys`, { params });
  }

  getSurveyById(id: string): Observable<Survey> {
    return this.http.get<Survey>(`${environment.apiUrl}/surveys/${id}`);
  }

  createSurvey(request: CreateSurveyRequest): Observable<boolean> {
    return this.http.post<boolean>(`${environment.apiUrl}/surveys`, request);
  }

  vote(request: VoteRequest): Observable<boolean> {
    return this.http.post<boolean>(`${environment.apiUrl}/surveys/${request.surveyId}/vote`, request);
  }
}

export interface PaginatedResponse<T> {
  isSuccess: boolean;
  value: {
    items: T[];
    pageNumber: number;
    totalPages: number;
    totalCount: number;
  };
  error: string;
  isFailure: boolean;
}