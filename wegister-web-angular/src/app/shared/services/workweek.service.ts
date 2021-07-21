import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { OAuthService } from 'angular-oauth2-oidc';
import { WorkweekEntry } from '../models/workweek-entry.model';
import { Observable } from 'rxjs';
import { WorkWeek } from 'src/app/models/workweek-model';

@Injectable({
  providedIn: 'root'
})
export class WorkweekService {
  private baseUrl: string = environment.WEGISTER_API;
  private workWeekUrl: string = '/workweek';
  private apiUrl: string = this.baseUrl + this.workWeekUrl;

  constructor(
    private httpClient: HttpClient,
    private oauthService: OAuthService
  ) {   }

  getWorkedYears()  : Observable<WorkweekEntry[]>{
    const httpOptions = { headers: new HttpHeaders({
      'Content-Type':  'application/json',
      'Authorization': 'Bearer ' + this.oauthService.getAccessToken()
    })};
    return this.httpClient.get<WorkweekEntry[]>(`${this.apiUrl}/year`, httpOptions)
  }

  getWeeks() : Observable<WorkWeek[]>{
    const httpOptions = { headers: new HttpHeaders({
      'Content-Type':  'application/json',
      'Authorization': 'Bearer ' + this.oauthService.getAccessToken(),
      'Access-Control-Allow-Origin': '*'
    })};
    return this.httpClient.get<WorkWeek[]>(this.apiUrl, httpOptions);
  }

  getWeeksPerYear(year: number)  : Observable<WorkWeek[]>{
    const httpOptions = { headers: new HttpHeaders({
      'Content-Type':  'application/json',
      'Authorization': 'Bearer ' + this.oauthService.getAccessToken(),
      'Access-Control-Allow-Origin': '*'
    })};
    return this.httpClient.get<WorkWeek[]>(`${this.apiUrl}/${year}`, httpOptions);
  }

  getMonthsPerYear(year: number)  : Observable<WorkweekEntry[]>{
    const httpOptions = { headers: new HttpHeaders({
      'Content-Type':  'application/json',
      'Authorization': 'Bearer ' + this.oauthService.getAccessToken(),
      'Access-Control-Allow-Origin': '*'
    })};
    return this.httpClient.get<WorkweekEntry[]>(`${this.apiUrl}/month/${year}`, httpOptions);
  }
  getWeeknumbers() : Observable<WorkWeek[]>{
    const httpOptions = { headers: new HttpHeaders({
      'Content-Type':  'application/json',
      'Authorization': 'Bearer ' + this.oauthService.getAccessToken(),
      'Access-Control-Allow-Origin': '*'
    })};
    return this.httpClient.get<WorkWeek[]>(this.apiUrl, httpOptions);
  }
}
