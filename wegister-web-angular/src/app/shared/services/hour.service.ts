import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { OAuthService } from 'angular-oauth2-oidc';
import { environment } from 'src/environments/environment';
import { HourRegistrationList } from 'src/app/shared/models/hourregistration-list-model';
import { throwError, Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { HourRegistration } from '../models/hourregistration-model';

@Injectable({
  providedIn: 'root'
})
export class HourService {
  private baseUrl: string = environment.WEGISTER_API;
  private hourUrl: string = '/hourregistration';
  private apiUrl: string = this.baseUrl + this.hourUrl;
  
  constructor(
    private httpClient: HttpClient,
    private oauthService: OAuthService
  ) { }

  getHourRegistrations(){
    const httpOptions = { headers: new HttpHeaders({
      'Content-Type':  'application/json',
      'Authorization': 'Bearer ' + this.oauthService.getAccessToken()
    })};
    return this.httpClient.get<HourRegistrationList>(`${this.apiUrl}`, httpOptions);
  }

  getHourRegistrationsByEmployerId(employerId: number) : Observable<HourRegistrationList>{
    const httpOptions = { headers: new HttpHeaders({
      'Content-Type':  'application/json',
      'Authorization': 'Bearer ' + this.oauthService.getAccessToken(),
      'Access-Control-Allow-Origin': '*'
    })};
    return this.httpClient.get<HourRegistrationList>(`${this.apiUrl}/employer/${employerId}`, httpOptions);
  }

  getHourRegistrationsByWeekAndEmployerId(weeknumber: number, employerId: number) : Observable<HourRegistrationList>{
    const httpOptions = { headers: new HttpHeaders({
      'Content-Type':  'application/json',
      'Authorization': 'Bearer ' + this.oauthService.getAccessToken(),
      'Access-Control-Allow-Origin': '*'
    })};
    return this.httpClient.get<HourRegistrationList>(`${this.apiUrl}/week/${weeknumber}/${employerId}`, httpOptions);
  }

  getHourRegistrationsByMonthAndEmployerId(month: number, employerId: number) : Observable<HourRegistrationList>{
    const httpOptions = { headers: new HttpHeaders({
      'Content-Type':  'application/json',
      'Authorization': 'Bearer ' + this.oauthService.getAccessToken(),
      'Access-Control-Allow-Origin': '*'
    })};
    return this.httpClient.get<HourRegistrationList>(`${this.apiUrl}/month/${month}/${employerId}`, httpOptions);
  }

  getActiveRegistration(){
    const httpOptions = { headers: new HttpHeaders({
      'Content-Type':  'application/json',
      'Authorization': 'Bearer ' + this.oauthService.getAccessToken()
    })};
    return this.httpClient.get<HourRegistration>(`${this.apiUrl}/active`, httpOptions);
  }

  startAutomaticRegistration(hourRegistration: HourRegistration){
    const httpOptions = { headers: new HttpHeaders({
      'Content-Type':  'application/json',
      'Authorization': 'Bearer ' + this.oauthService.getAccessToken()
    })};
    return this.httpClient.post<Response>(`${this.apiUrl}`, hourRegistration, httpOptions).pipe(catchError(this.handleError));
  }

  stopAutomaticRegistration(hourRegistration: any){
    const httpOptions = { headers: new HttpHeaders({
      'Content-Type':  'application/json',
      'Authorization': 'Bearer ' + this.oauthService.getAccessToken()
    })};
    return this.httpClient.put<Response>(`${this.apiUrl}/active`, hourRegistration, httpOptions).pipe(catchError(this.handleError));
  }

  addHour(hourRegistration: HourRegistration){
    const httpOptions = { headers: new HttpHeaders({
      'Content-Type':  'application/json',
      'Authorization': 'Bearer ' + this.oauthService.getAccessToken()
    })};
    return this.httpClient.post<any>(this.apiUrl, hourRegistration, httpOptions).pipe(catchError(this.handleError));
  }

  updateRegistration(hourRegistration: any){
    const httpOptions = { headers: new HttpHeaders({
      'Content-Type':  'application/json',
      'Authorization': 'Bearer ' + this.oauthService.getAccessToken()
    })};
    return this.httpClient.put<Response>(`${this.apiUrl}/${hourRegistration.id}`, hourRegistration, httpOptions).pipe(catchError(this.handleError));
  }

  deleteteRegistration(hourRegistration: HourRegistration){
    const httpOptions = { headers: new HttpHeaders({
      'Content-Type':  'application/json',
      'Authorization': 'Bearer ' + this.oauthService.getAccessToken()
    })};
    return this.httpClient.delete<Response>(`${this.apiUrl}/${hourRegistration.id}`, httpOptions).pipe(catchError(this.handleError));
  }

  handleError(error: HttpErrorResponse) {
    let errorMessage = 'Unknown error!';
    if (error.error instanceof ErrorEvent) {
      // Client-side errors
      errorMessage = `Error: ${error.error.message}`;
    } else {
      // Server-side errors
      errorMessage = `Controleer of alle velden ingevoerd zijn. ERROR MESSAGE: ${error.message}`;
    }
    window.alert(errorMessage);
    return throwError(errorMessage);
  }
}
