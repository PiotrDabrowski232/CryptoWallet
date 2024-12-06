import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ButtonComponent } from './button/button.component';
import { ButtonConfig } from './button/button-interface';
import { WalletBasicInfo } from './Api/ApiResult.interface';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, ButtonComponent, CommonModule],
  template: `
  
    <h4>CryptoWallet</h4>
    <app-button [config]="primaryButton"></app-button>
    
    <div *ngIf="walletList.length > 0; else noWallets">
      <div class="cards">
        <div class="card" style="width: 18rem;" *ngFor="let wallet of walletList">
          <div class="card-body">
            <h5 class="card-title">{{wallet.Name}}</h5>
            <h6 class="card-subtitle mb-2 text-body-secondary">Cryptocurrency Count: {{wallet.CryptoCount}}</h6>
            <app-button [config]="cardButton"></app-button>
          </div>
        </div>
      </div>
    </div>

    <ng-template #noWallets >
      <p class="noWallets">No wallets in the system. Add a new wallet.</p>
    </ng-template>

    <router-outlet />
  `,
  styles: `
    ::ng-deep .newWallet { 
        position: fixed;
        right: 12vw;
        top: 10vh;
      }
      h4{
        margin-top:4vh;
        text-align: center;
      }
      .cards{
        display: flex;
        flex-wrap:wrap;
        padding-top: 5rem;
        padding-left: 3vw;
        padding-right: 3vw;
      }
      .card{
        margin: 2rem;
      }
      .noWallets{
        position: fixed;
        top:20vh;
        left: 42%;
      }`
})
export class AppComponent {
  title = 'CryptoWallet';

  primaryButton: ButtonConfig = {
    color: 'btn-primary',
    label: 'Add Wallet',
    width: "8vw",
    height: '5vh',
    additionalClass: "newWallet",
    type: 'button'
  }

  cardButton: ButtonConfig = {
    color: 'btn-info',
    label: 'Check Details',
    width: "fit-content",
    height: 'auto',
    type: 'button'
  }

  walletList: WalletBasicInfo[] = [
    {
      Id: 'f47b6549-45a9-4e57-b80d-93f3c9e6b207',
      Name: 'Bitcoin Wallet',
      CryptoCount: 5
    },
    {
      Id: 'a1b2c3d4-56e7-8f9g-12h3-45i6j7k8l9m0',
      Name: 'Ethereum Wallet',
      CryptoCount: 12
    },
    {
      Id: '123e4567-e89b-12d3-a456-426614174000',
      Name: 'Litecoin Wallet',
      CryptoCount: 3
    },
    {
      Id: '123e4222-e89b-12d3-a456-426614174000',
      Name: 'Litecoin Wallet',
      CryptoCount: 3
    },
    {
      Id: '123e4511-e89b-12d3-a456-426614174000',
      Name: 'Litecoin Wallet',
      CryptoCount: 3
    },
    {
      Id: '123e4242-e89b-12d3-a456-426614174000',
      Name: 'Litecoin Wallet',
      CryptoCount: 3
    }
  ];

}
