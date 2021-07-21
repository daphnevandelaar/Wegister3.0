import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { OAuthService } from 'angular-oauth2-oidc';
import { authCodeFlowConfig } from './authentication/auth-config';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'Wegister';
  
  constructor(private oauthService: OAuthService, private router: Router) {

    if(environment.production){
      this.oauthService.configure(authCodeFlowConfig);
      this.oauthService.setupAutomaticSilentRefresh();
  
      if (!this.oauthService.hasValidAccessToken()) {
        this.oauthService.loadDiscoveryDocumentAndLogin()
          .then(() => {
            if (!this.oauthService.hasValidAccessToken()) {}
          }).catch(err => {
            console.error(err);
          });
      }
      else{
        console.log( this.oauthService.getAccessToken()) 
      }
    }
  }
}
