import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from "../models/user-interface";
import {RoleChangeRequest} from "../models/requests/role-change-request";

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = 'http://localhost:5213/api/User';

  constructor(private http: HttpClient) { }

  getAllUsers(): Observable<User[]> {
    return this.http.get<User[]>(`${this.apiUrl}`);
  }

  update(user: User): Observable<User> {
    return this.http.put<User>(`${this.apiUrl}`, user);
  }

  changeRole(request: RoleChangeRequest): Observable<User> {
    const url = `${this.apiUrl}/changerole`;
    return this.http.put<User>(url, request);
  }

  public deleteUserByEmail(email: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/deleteUserByEmail?email=${email}`);
  }

  public blockUser(email: string) {
    return this.http.put(`${this.apiUrl}/block?email=${email}`, null);
  }
}
