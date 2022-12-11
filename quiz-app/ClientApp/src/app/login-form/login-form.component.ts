import { Component, OnInit } from '@angular/core';
import { AuthService } from "../services/auth.service";
import { TokenStorageService } from "../services/token-storage.service";
import { MatSnackBar, MatSnackBarRef } from "@angular/material/snack-bar";
import {ActivatedRoute, Router} from "@angular/router";

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.css']
})
export class LoginFormComponent implements OnInit {
  hide = true;

  form: any = {
    username: null,
    password: null
  };
  isSignedIn = false;
  isSignInFailed = false;
  errorMessage = '';
  roles: string[] = [];
  redirectUrl: string | null = null;

  constructor(
    private authService: AuthService,
    private tokenStorage: TokenStorageService,
    private _snackBar: MatSnackBar,
    private route: ActivatedRoute,
    private router: Router
) {}

  ngOnInit(): void {
    if (this.tokenStorage.getToken()) {
      this.isSignedIn = true;
      this.roles = this.tokenStorage.getUser().roles;
    }

    this.route.queryParams
      .subscribe(params => {
          this.redirectUrl = params.redirect_to;
        }
      );
  }

  onSubmit(): void {
    const { username, password } = this.form;

    this.authService.login(username, password).subscribe(
      data => {
        this.tokenStorage.saveToken(data.result.authToken);
        this.tokenStorage.saveUser(data);
        this.isSignInFailed = false;
        this.isSignedIn = true;
        this.roles = this.tokenStorage.getUser().roles;
        this.router.navigate(["/" + this.redirectUrl]);
      },
      err => {
        this.errorMessage = err.error.error.message;
        this.isSignInFailed = true;
        this.openError(this.errorMessage);
      }
    )
  }

  openError(message: string) {
    this._snackBar.open(message, "", {
      duration: 5000
    });
  }

  reloadPage(): void {
    window.location.reload();
  }
}
