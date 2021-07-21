import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { OAuthService } from 'angular-oauth2-oidc';

@Injectable({
  providedIn: 'root'
})
export class EmailService {
  private baseUrl: string = environment.WEGISTER_API;
  private mailUrl: string = '/email';
  private apiUrl: string = this.baseUrl + this.mailUrl;
  
  constructor(
    private httpClient: HttpClient,
    private oauthService: OAuthService
  ) { }

  mailByWeekIds(weekIds){
    const httpOptions = { headers: new HttpHeaders({
      'Content-Type':  'application/json',
      'Authorization': 'Bearer ' + this.oauthService.getAccessToken()
    })};
    return this.httpClient.post(`${this.apiUrl}`, weekIds, httpOptions);
  }
}
