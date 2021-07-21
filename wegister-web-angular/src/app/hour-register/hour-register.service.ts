import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { HourRegistration } from '../models/hour-registration-model';
import { Observable, throwError } from 'rxjs';
import { OAuthService } from 'angular-oauth2-oidc';
import { catchError } from 'rxjs/operators';
import { WorkWeek } from '../models/workweek-model';

@Injectable({
  providedIn: 'root'
})
export class HourRegisterService {
  private baseUrl: string = environment.WEGISTER_API;
  private hourRegistrationUrl: string = '/hourregistration';
  private workWeekUrl: string = '/workweek';
  private mailUrl: string = '/email';

  private hourapiUrl: string = this.baseUrl + this.hourRegistrationUrl;
  private weekapiUrl: string = this.baseUrl + this.workWeekUrl;
  private mailapiUrl: string = this.baseUrl + this.mailUrl;


  constructor(
    private httpClient: HttpClient,
    private oauthService: OAuthService
  ) {   }

  addHourRegistration(hourRegistration: HourRegistration){
    const httpOptions = { headers: new HttpHeaders({
      'Content-Type':  'application/json',
      'Authorization': 'Bearer ' + this.oauthService.getAccessToken()
    })};
    return this.httpClient.post<Response>(this.hourapiUrl, hourRegistration, httpOptions);
  }

  postHour(hourRegistration: HourRegistration){
    const httpOptions = { headers: new HttpHeaders({
      'Content-Type':  'application/json',
      'Authorization': 'Bearer ' + this.oauthService.getAccessToken()
    })};
    return this.httpClient.post<any>(this.hourapiUrl, hourRegistration, httpOptions).pipe(catchError(this.handleError));
  }

  handleError(error: HttpErrorResponse) {
    let errorMessage = 'Unknown error!';
    if (error.error instanceof ErrorEvent) {
      // Client-side errors
      errorMessage = `Error: ${error.error.message}`;
    } else {
      // Server-side errors
      errorMessage = `Controleer of alle velden ingevuld zijn`;
    }
    window.alert(errorMessage);
    return throwError(errorMessage);
  }

  testRegistration(newApi) : Observable<HourRegistration[]>{
    const httpOptions = { headers: new HttpHeaders({
      'Content-Type':  'application/json',
      'Authorization': 'Bearer ' + this.oauthService.getAccessToken()
    })};
    return this.httpClient.get<HourRegistration[]>(newApi, httpOptions);
  }

  getHourRegistrations() : Observable<HourRegistration[]>{
    const httpOptions = { headers: new HttpHeaders({
      'Content-Type':  'application/json',
      'Authorization': 'Bearer ' + this.oauthService.getAccessToken(),
      'Access-Control-Allow-Origin': '*'
    })};
    return this.httpClient.get<HourRegistration[]>(this.hourapiUrl, httpOptions);
  }

  getWeeknumbers() : Observable<WorkWeek[]>{
    const httpOptions = { headers: new HttpHeaders({
      'Content-Type':  'application/json',
      'Authorization': 'Bearer ' + this.oauthService.getAccessToken(),
      'Access-Control-Allow-Origin': '*'
    })};
    return this.httpClient.get<WorkWeek[]>(this.weekapiUrl, httpOptions);
  }

  getHourRegistrationsByWeek(weeknumber: number) : Observable<HourRegistration[]>{
    const httpOptions = { headers: new HttpHeaders({
      'Content-Type':  'application/json',
      'Authorization': 'Bearer ' + this.oauthService.getAccessToken(),
      'Access-Control-Allow-Origin': '*'
    })};
    return this.httpClient.get<HourRegistration[]>(`${this.hourapiUrl}/${weeknumber}`, httpOptions);
  }

  getHourRegistrationsByWeekAndEmployerId(weeknumber: number, employerId: number) : Observable<HourRegistration[]>{
    const httpOptions = { headers: new HttpHeaders({
      'Content-Type':  'application/json',
      'Authorization': 'Bearer ' + this.oauthService.getAccessToken(),
      'Access-Control-Allow-Origin': '*'
    })};
    return this.httpClient.get<HourRegistration[]>(`${this.hourapiUrl}/${weeknumber}/${employerId}`, httpOptions);
  }

  getHourRegistrationsByMonthAndEmployerId(month: number, employerId: number) : Observable<HourRegistration[]>{
    const httpOptions = { headers: new HttpHeaders({
      'Content-Type':  'application/json',
      'Authorization': 'Bearer ' + this.oauthService.getAccessToken(),
      'Access-Control-Allow-Origin': '*'
    })};
    return this.httpClient.get<HourRegistration[]>(`${this.hourapiUrl}/month/${month}/${employerId}`, httpOptions);
  }

  mailWorkedHours(){
    const httpOptions = { headers: new HttpHeaders({
      'Content-Type':  'application/json',
      'Authorization': 'Bearer ' + this.oauthService.getAccessToken(),
      'Access-Control-Allow-Origin': '*'
    })};
    return this.httpClient.get<HourRegistration[]>(`${this.mailapiUrl}`, httpOptions);
  }
}
