import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiService } from '../Api/api.service';
import { WalletDto } from '../Api/ApiResult.interface';

@Component({
  selector: 'app-cryptocurrencies',
  imports: [],
  templateUrl: './cryptocurrencies.component.html',
  styles: `
  .walletName{
    margin-left:4vw;
    margin-top: 5vh;
    margin-bottom: 3vh;
  }
  `
})
export class CryptocurrenciesComponent implements OnInit {
  queryParams: string | null = '';
  walletInfo: WalletDto | null = null;
  constructor(private route: ActivatedRoute, private apiService: ApiService) { }


  ngOnInit(): void {
    this.route.queryParamMap.subscribe(param => {
      this.queryParams = param.get('wallet');
    });
    if (this.queryParams)
      this.apiService.getWalletElements(this.queryParams).subscribe({
        next: data => {
          this.walletInfo = data;
        },
        error: (error) => {
          console.log(error)
        }
      })
  }
}
