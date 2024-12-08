import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, catchError } from 'rxjs';
import { CryptocurrencyDto, NewCryptoDto, WalletBasicInfo, WalletDto } from './ApiResult.interface';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private baseURL = 'https://localhost:7086';

  constructor(private http: HttpClient) { }

  getCryptoNames(): Observable<string[]> {
    return this.http.get<string[]>(`${this.baseURL}/Names`)
      .pipe(
        catchError((error) => {
          console.error('API Error:', error);
          throw error;
        })
      );
  }

  getWallets(): Observable<WalletBasicInfo[]> {
    return this.http.get<WalletBasicInfo[]>(`${this.baseURL}/Wallets`)
      .pipe(
        catchError((error) => {
          console.error('API Error:', error);
          throw error;
        })
      );
  }

  getWalletElements(walletId: string): Observable<WalletDto> {
    return this.http.get<WalletDto>(`${this.baseURL}/Wallet?id=${walletId}`)
      .pipe(
        catchError((error) => {
          console.error('API Error:', error);
          throw error;
        })
      );
  }

  newWallet(WalletName: string): Observable<string> {
    return this.http.post<string>(`${this.baseURL}/CreateWallet`, JSON.stringify(WalletName), {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
      responseType: 'text' as 'json'
    })
      .pipe(
        catchError((error) => {
          console.error('API Error:', error);
          throw error.error;
        })
      );
  }

  newCrypto(newCrypto: NewCryptoDto): Observable<CryptocurrencyDto> {
    return this.http.post<CryptocurrencyDto>(`${this.baseURL}/NewCrypto`, JSON.stringify(newCrypto), {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
      responseType: 'text' as 'json'
    })
      .pipe(
        catchError((error) => {
          console.error('API Error:', error.error);
          throw error.error;
        })
      );
  }

  renameWallet(WalletName: string, Id: string): Observable<boolean> {
    return this.http.put<boolean>(`${this.baseURL}/Wallet?id=${Id}`, JSON.stringify(WalletName), {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
      responseType: 'text' as 'json'
    })
      .pipe(
        catchError((error) => {
          console.error('API Error:', error);
          throw error.error;
        })
      );
  }

  removeWallet(walletId: string): Observable<boolean> {
    return this.http.delete<boolean>(`${this.baseURL}/Remove?id=${walletId}`)
      .pipe(
        catchError((error) => {
          console.error('API Error:', error);
          throw error;
        })
      );
  }
}
