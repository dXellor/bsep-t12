import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class SecretService {
  private url: string = `${environment.apiUrl}/Secret`;

  constructor(private http: HttpClient, private toastr: ToastrService) {}

  public getSecret(): void {
    const headers = new HttpHeaders().set(
      'Content-Type',
      'text/plain; charset=utf-8'
    );
    this.http
      .get<string>(this.url, { responseType: 'text' } as Record<
        string,
        unknown
      >)
      .subscribe({
        next: (res) => {
          console.log(res);
          this.toastr.info(res, 'VPN Secret', {
            timeOut: 5000,
            closeButton: true,
            progressBar: true,
            extendedTimeOut: 2000,
          });
        },

        error: (err) => {
          this.toastr.error(err.error, 'VPN Secret Error', {
            closeButton: true,
            progressBar: true,
            extendedTimeOut: 2000,
          });
        },
      });
  }
}
