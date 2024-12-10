import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable, catchError, throwError } from 'rxjs';
import { CryptocurrencyDto, NewCryptoDto, UpdateCryptoDto, ValidationErrors, WalletBasicInfo, WalletDto } from './ApiResult.interface';

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

  getWalletElements(walletId: string, turnOnConverison?: boolean): Observable<WalletDto> {
    return this.http.get<WalletDto>(`${this.baseURL}/Wallet?id=${walletId}${turnOnConverison !== undefined ? `&conversion=${turnOnConverison}` : ''}`)
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
      responseType: 'json'
    })
      .pipe(
        catchError((error) => handleErrors(error))
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

  updateCrypto(updateCrypto: UpdateCryptoDto): Observable<boolean> {
    return this.http.put<boolean>(`${this.baseURL}/UpdateCrypto`, JSON.stringify(updateCrypto), {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
      responseType: 'json'
    })
      .pipe(
        catchError((error) => handleErrors(error))
      )
  }

  removeWallet(walletId: string): Observable<boolean> {
    return this.http.delete<boolean>(`${this.baseURL}/Remove?id=${walletId}`)
      .pipe(
        catchError((error) => {
          throw error;
        })
      );
  }

  removeCrypto(cryptoId: string): Observable<boolean> {
    return this.http.delete<boolean>(`${this.baseURL}/RemoveCrypto?id=${cryptoId}`)
      .pipe(
        catchError((error) => {
          console.error('API Error:', error);
          throw error;
        })
      );
  }
}


export function handleErrors(error: HttpErrorResponse) {
  console.log(error)
  if (error.status === 400 && error.error && error.error.errors) {
    return throwError(() => error.error.errors as ValidationErrors);
  }

  return throwError(() => new Error('Unexpected error occurred'));
}