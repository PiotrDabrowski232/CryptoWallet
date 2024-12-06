import { Component, OnInit, ViewChild } from '@angular/core';
import { ButtonComponent } from './button/button.component';
import { ButtonConfig } from './button/button-interface';
import { WalletBasicInfo } from './Api/ApiResult.interface';
import { CommonModule } from '@angular/common';
import { InputComponent } from './input/input.component';
import { InputConfig } from './input/input.interface';
import { ApiService } from './Api/api.service';


@Component({
  selector: 'app-root',
  imports: [ButtonComponent, CommonModule, InputComponent],
  templateUrl: './app.components.html',
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
export class AppComponent implements OnInit {
  title = 'CryptoWallet';

  @ViewChild(InputComponent) inputComponent!: InputComponent;

  newWalletName: string = '';
  walletList: WalletBasicInfo[] = [];

  constructor(private apiService: ApiService) { }

  ngOnInit(): void {
    this.apiService.getWallets().subscribe({
      next: data => {
        this.walletList = data;
      },
      error: (error) => {
        console.log(error)
      }
    })
  }

  onWalletNameChange(newName: string): void {
    this.newWalletName = newName;
  }

  addWallet(): void {
    if (this.newWalletName.trim()) {
      const newWallet: WalletBasicInfo = {
        id: crypto.randomUUID(),
        name: this.newWalletName,
        cryptoCount: 0
      };
      this.walletList.push(newWallet);
      this.newWalletName = '';
      this.inputComponent.clearInput();
    }
  }

  walletName: InputConfig = {
    type: "Text",
    label: "Wallet Name"
  }

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

  closeButton: ButtonConfig = {
    color: 'btn-secondary',
    label: 'close',
    width: "fit-content",
    height: 'auto',
    type: 'button'
  }

  saveButton: ButtonConfig = {
    color: 'btn-success',
    label: 'add',
    width: "fit-content",
    height: 'auto',
    type: 'button'
  }
}
