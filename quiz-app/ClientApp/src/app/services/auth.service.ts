import {Inject, Injectable} from "@angular/core";
import { HttpClient, HttpHeaders} from "@angular/common/http";
import { Observable } from "rxjs";
import {TokenStorageService} from "./token-storage.service";
import {JwtHelperService} from "@auth0/angular-jwt";
import {Router} from "@angular/router";

let AUTH_API = "https://localhost:44433/auth/";

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': "application/json"})
};

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(
    private jwtHelper: JwtHelperService,
    private http: HttpClient,
    private tokenStorage: TokenStorageService,
    private router: Router,
    @Inject('BASE_URL') baseUrl: string
  )
  {
    AUTH_API = baseUrl;
  }

  isAuthenticated(): boolean {
    const token = this.tokenStorage.getToken();

    if (token == null)
      return false;

    return !this.jwtHelper.isTokenExpired(token);
  }

  login(username: string, password: string): Observable<any> {
    return this.http.post(AUTH_API + "auth/sign-in", {
      username,
      password
    }, httpOptions);
  }

  signOut(): void {
    this.tokenStorage.signOut();
    this.router.navigate(['login']);
  }
}
