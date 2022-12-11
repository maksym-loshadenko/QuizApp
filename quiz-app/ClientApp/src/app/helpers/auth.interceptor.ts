import { Injectable } from '@angular/core';
import {
  HTTP_INTERCEPTORS, HttpEvent,
  HttpInterceptor, HttpHandler,
  HttpRequest
} from '@angular/common/http';

import { TokenStorageService } from "../services/token-storage.service";
import { Observable } from 'rxjs';

const TOKEN_HEADER_KEY = 'Authorization';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private token: TokenStorageService) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    let authRequest = request;
    const token = this.token.getToken()

    if (token != null) {
      authRequest = request.clone({ headers: request.headers.set(TOKEN_HEADER_KEY, "Bearer " + token)});
    }

    return next.handle(authRequest);
  }
}

export const authInterceptorProviders = [
  { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }
];
