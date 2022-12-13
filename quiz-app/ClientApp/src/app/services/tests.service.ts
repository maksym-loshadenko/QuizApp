import {Inject, Injectable} from '@angular/core';
import {HttpClient, HttpHeaders, HttpParams} from "@angular/common/http";
import {Observable} from "rxjs";
import {TestCheckModel} from "../models/test-check.model";

let BASE_URL = "https://localhost:44433/auth/";

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': "application/json"})
};

@Injectable({
  providedIn: 'root'
})
export class TestsService {

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') baseUrl: string
  )
  {
    BASE_URL = baseUrl;
  }

  getAvailableTests(): Observable<any> {
    return this.http.get(BASE_URL + "tests", httpOptions)
  }

  getTest(id: string): Observable<any> {
    const params = { "id": id };
    return this.http.get(BASE_URL + "test", { params: params } );
  }

  checkTest(testToCheck: TestCheckModel): Observable<any> {
    return this.http.post(BASE_URL + "test/check", testToCheck);
  }
}
