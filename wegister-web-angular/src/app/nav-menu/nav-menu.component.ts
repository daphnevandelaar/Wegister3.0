import { Component } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})
export class NavMenuComponent {
  isExpanded = false;
  public isCollapsed = true;
  user: string; 
  title: string = environment.TITLE;
  develop = environment.production;

  constructor(
    private oauthService: OAuthService,
    private router: Router
  ) {  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  logOut(){
    this.oauthService.logOut();
    this.router.navigateByUrl(environment.AUTH_API + '/account/logout')
  }
}
