import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { LoginFormComponent } from "./login-form/login-form.component";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

// Material UI components
import { MatButtonModule } from "@angular/material/button";
import { MatToolbarModule } from "@angular/material/toolbar";
import { MatIconModule } from "@angular/material/icon";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatInputModule } from "@angular/material/input";
import { MatCardModule } from "@angular/material/card";
import { MatSnackBarModule } from "@angular/material/snack-bar";
import { MatListModule } from "@angular/material/list";
import { MatSidenavModule } from "@angular/material/sidenav";

// JWT Helper Service
import { JWT_OPTIONS, JwtHelperService } from "@auth0/angular-jwt";

// Auth Guard service
import { AuthGuardService as AuthGuard } from "./guards/auth-guard.service";

// Auth Interceptor Provider
import { authInterceptorProviders } from "./helpers/auth.interceptor";
import { TestsListComponent } from "./pages/tests-list/tests-list.component";

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    LoginFormComponent,
    TestsListComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot(([
      { path: "home", component: HomeComponent },
      { path: "tests", component: TestsListComponent },
      { path: "counter", component: CounterComponent },
      { path: "fetch-data", component: FetchDataComponent, canActivate: [AuthGuard]},
      { path: "login", component: LoginFormComponent },
      { path: "", redirectTo: "/home", pathMatch: "full" },
      { path: "**", redirectTo: "/home" }
    ]) as any),
    BrowserAnimationsModule,
    MatButtonModule,
    MatToolbarModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    MatCardModule,
    MatSnackBarModule,
    MatListModule,
    MatSidenavModule
  ],
  providers: [
    { provide: JWT_OPTIONS, useValue: JWT_OPTIONS },
    JwtHelperService,
    authInterceptorProviders
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
