import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Employer } from '../models/employer.model';
import { OAuthService } from 'angular-oauth2-oidc';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class EmployerService {
  private baseUrl: string = environment.WEGISTER_API;
  private employerUrl: string = '/employer';

  private employerapiUrl: string = this.baseUrl + this.employerUrl;

  constructor(
    private httpClient: HttpClient,
    private oauthService: OAuthService
  ) { }

  addEmployer(employer: Employer){
    const httpOptions = { headers: new HttpHeaders({
      'Content-Type':  'application/json',
      'Authorization': 'Bearer ' + this.oauthService.getAccessToken()
    })};
    return this.httpClient.post<Employer>(this.employerapiUrl, employer, httpOptions);
  }

  getEmployers() : Observable<Employer[]>{
    const httpOptions = { headers: new HttpHeaders({
      'Content-Type':  'application/json',
      'Authorization': 'Bearer ' + this.oauthService.getAccessToken()
    })};
    return this.httpClient.get<Employer[]>(this.employerapiUrl, httpOptions);
  }

  updateEmployer(employer:Employer){
    const httpOptions = { headers: new HttpHeaders({
      'Content-Type':  'application/json',
      'Authorization': 'Bearer ' + this.oauthService.getAccessToken()
    })};
    return this.httpClient.put<Response>(`${this.employerapiUrl}/${employer.id}`, employer, httpOptions);
  }

  deleteEmployer(employer:Employer){
    const httpOptions = { headers: new HttpHeaders({
      'Content-Type':  'application/json',
      'Authorization': 'Bearer ' + this.oauthService.getAccessToken()
    })};
    return this.httpClient.delete<Response>(`${this.employerapiUrl}/${employer.id}`, httpOptions).pipe(catchError(this.handleError));
  }

  handleError(error: HttpErrorResponse) {
    let errorMessage = 'Unknown error!';
    console.log(error);
    if (error.error instanceof ErrorEvent) {
      // Client-side errors
      errorMessage = `Error: ${error.error.message}`;
    } else {
      // Server-side errors
      errorMessage = `Error Code: ${error.status}\nMessage: De werkgever kan niet worden verwijdert, omdat er uren geregistreerd zijn voor deze werkgever.`;
    }
    window.alert(errorMessage);
    return throwError(errorMessage);
  }
}
