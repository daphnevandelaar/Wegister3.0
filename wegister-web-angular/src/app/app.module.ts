import { BrowserModule } from '@angular/platform-browser';
import { NgModule, LOCALE_ID } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { MomentModule } from 'ngx-moment';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { RouterModule } from '@angular/router';
// import { HourRegisterComponent } from './hour-register/hour-register.component';
import { HomepageComponent } from './homepage/homepage.component';
import { OAuthModule, OAuthStorage } from 'angular-oauth2-oidc';
import { LogoutComponent } from './authentication/logout/logout/logout.component';
import { LoginComponent } from './authentication/login/login/login.component';
import { HourOverviewComponent } from './hour/hour-overview/hour-overview.component';
import { WeekSelectorComponent } from './hour/hour-overview/week-selector/week-selector.component';
import { EmployerRegisterComponent } from './employer/employer-register/employer-register.component';
import { EmployerDropdownComponent } from './employer/employer-dropdown/employer-dropdown.component';
import { EmployerSelectorComponent } from './hour/hour-overview/employer-selector/employer-selector.component';
import { DateSliderComponent } from './hour/hour-overview/date-slider/date-slider.component';
import { HourHomeComponent } from './hour/hour-home/hour-home.component';
import { EmployerHomeComponent } from './employer/employer-home/employer-home.component';
import { MailComponent } from './mail/mail.component';
import { registerLocaleData } from '@angular/common';
import localeNl from '@angular/common/locales/nl';
import { HourRegisterAutomaticComponent } from './hour/hour-register-automatic/hour-register-automatic.component';
import { HourRegisterComponent } from './hour/hour-register/hour-register.component';
import { TotalHoursPipe } from './shared/pipes/total-hours.pipe';

registerLocaleData(localeNl, 'nl')

@NgModule({
  declarations: [
    AppComponent,
    HomepageComponent,
    NavMenuComponent,
    HourRegisterComponent,
    LogoutComponent,
    LoginComponent,
    HourOverviewComponent,
    WeekSelectorComponent,
    EmployerRegisterComponent,
    EmployerDropdownComponent,
    EmployerSelectorComponent,
    DateSliderComponent,
    HourHomeComponent,
    MailComponent,
    HourRegisterAutomaticComponent,
    TotalHoursPipe
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    AppRoutingModule,
    OAuthModule.forRoot(),
    FormsModule,
    NgbModule,  
    MomentModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomepageComponent },
      { path: 'hour', component: HourHomeComponent },
      { path: 'hour-register-automatic', component: HourRegisterAutomaticComponent },
      { path: 'employer', component: EmployerHomeComponent },
      { path: 'hour-register', component: HourRegisterComponent },
      { path: 'hour-overview', component: HourOverviewComponent },
      { path: 'employer-register', component: EmployerRegisterComponent },
      { path: 'logout', component: LogoutComponent }
    ])
  ],
  providers: [
    {provide: OAuthStorage, useFactory: storageFactory},
    {provide: LOCALE_ID, useValue: 'nl'}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

export function storageFactory(): OAuthStorage {
  return localStorage;
}
