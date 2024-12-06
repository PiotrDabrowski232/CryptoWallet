import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, catchError } from 'rxjs';
import { WalletBasicInfo } from './ApiResult.interface';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private baseURL = 'https://localhost:7086';

  constructor(private http: HttpClient) { }

  getWallets(): Observable<WalletBasicInfo[]> {
    return this.http.get<WalletBasicInfo[]>(`${this.baseURL}/Wallets`)
      .pipe(
        catchError((error) => {
          console.error('API Error:', error);
          throw error;
        })
      );
  }
}
