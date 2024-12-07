import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-cryptocurrencies',
  imports: [],
  templateUrl: './cryptocurrencies.component.html',
  styles: ``
})
export class CryptocurrenciesComponent implements OnInit {
  queryParams: string | null = '';

  constructor(private route: ActivatedRoute) { }


  ngOnInit(): void {
    this.route.queryParamMap.subscribe(param => {
      this.queryParams = param.get('wallet');
    });
  }

}
