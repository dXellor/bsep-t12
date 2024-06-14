import { Injectable } from '@angular/core';
import {HttpClient, HttpResponse} from "@angular/common/http";
import {Observable} from "rxjs";
import {User} from "../models/user-interface";
import {RoleChangeRequest} from "../models/requests/role-change-request";
import {Advertisement} from "../models/advertisement";
import {ClickRequest} from "../models/click-request";

@Injectable({
  providedIn: 'root'
})
export class AdsService {

  private apiUrl = 'http://localhost:5213/api/advertisement';

  constructor(private http: HttpClient) { }

  getAllAds(): Observable<Advertisement[]> {
    return this.http.get<Advertisement[]>(`${this.apiUrl}`);
  }

  click(request: ClickRequest): Observable<HttpResponse<string>> {
    return this.http.post<string>(`${this.apiUrl}/click`, request, { observe: 'response' });
  }
}
