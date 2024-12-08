import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiService } from '../Api/api.service';

@Component({
  selector: 'app-cryptocurrencies',
  imports: [],
  templateUrl: './cryptocurrencies.component.html',
  styles: ``
})
export class CryptocurrenciesComponent implements OnInit {
  queryParams: string | null = '';

  constructor(private route: ActivatedRoute, private apiService: ApiService) { }


  ngOnInit(): void {
    this.route.queryParamMap.subscribe(param => {
      this.queryParams = param.get('wallet');
    });
    if (this.queryParams)
      this.apiService.getWalletElements(this.queryParams).subscribe({
        next: data => {
          console.log(data);
        },
        error: (error) => {
          console.log(error)
        }
      })
  }



}
