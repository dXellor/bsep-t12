import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class MonitoringService {
  private url: string = `${environment.apiUrl}/Monitoring`;
  constructor(private http: HttpClient) {}

  public downloadLatestLog() {
    return this.http.get(`${this.url}/logs`, {
      observe: 'response',
      responseType: 'blob',
    });
  }
}
