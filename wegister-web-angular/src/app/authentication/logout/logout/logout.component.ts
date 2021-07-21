import { Component, OnInit } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';

@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html'
})
export class LogoutComponent{

  constructor(
    private oauthService: OAuthService
  ) {
    this.oauthService.logOut();
    location.reload();
  }
}
