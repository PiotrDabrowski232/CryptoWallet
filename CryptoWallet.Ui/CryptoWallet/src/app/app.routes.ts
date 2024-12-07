import { Routes } from '@angular/router';
import { CryptocurrenciesComponent } from './cryptocurrencies/cryptocurrencies.component';
import { WalletsComponent } from './wallets/wallets.component'

export const routes: Routes = [
    {
        path: 'Wallet',
        component: CryptocurrenciesComponent
    },
    {
        path: '',
        component: WalletsComponent
    }
];
