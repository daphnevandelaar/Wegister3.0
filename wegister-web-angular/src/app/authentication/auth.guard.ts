import { Injectable } from '@angular/core';
import { CanActivate } from '@angular/router';
import { OAuthService } from 'angular-oauth2-oidc';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  
  constructor(private readonly authService: OAuthService) { }

  canActivate(): boolean {
    if(environment.production)
      return this.authService.hasValidAccessToken()
    else
      return true;
  }
  
}
