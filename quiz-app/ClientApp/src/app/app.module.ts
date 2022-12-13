import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { LoginFormComponent } from "./pages/login-form/login-form.component";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { TestsListComponent, TestOverviewComponent } from "./pages/tests-list/tests-list.component";

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
import { MatTableModule } from "@angular/material/table";
import { MatDialogModule } from "@angular/material/dialog";
import { MatCheckboxModule } from "@angular/material/checkbox";
import { MatRadioModule } from "@angular/material/radio";
import {TestFormComponent, TestResultComponent} from "./pages/test-form/test-form.component";

// JWT Helper Service
import { JWT_OPTIONS, JwtHelperService } from "@auth0/angular-jwt";

// Auth Guard service
import { AuthGuardService as AuthGuard } from "./guards/auth-guard.service";

// Auth Interceptor Provider
import { authInterceptorProviders } from "./helpers/auth.interceptor";

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    LoginFormComponent,
    TestsListComponent,
    TestOverviewComponent,
    TestFormComponent,
    TestResultComponent
  ],
  imports: [
    BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot(([
      {path: "quizzes", component: TestsListComponent, canActivate: [AuthGuard]},
      {path: "quiz", component: TestFormComponent, canActivate: [AuthGuard]},
      {path: "login", component: LoginFormComponent},
      {path: "", redirectTo: "/quizzes", pathMatch: "full"},
      {path: "**", redirectTo: "/quizzes"}
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
    MatSidenavModule,
    MatTableModule,
    MatDialogModule,
    MatCheckboxModule,
    MatRadioModule
  ],
  providers: [
    { provide: JWT_OPTIONS, useValue: JWT_OPTIONS },
    JwtHelperService,
    authInterceptorProviders
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
