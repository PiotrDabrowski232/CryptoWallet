import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiService } from '../Api/api.service';
import { WalletDto } from '../Api/ApiResult.interface';
import { ButtonComponent } from '../button/button.component';
import { ButtonConfig } from '../button/button-interface';
import { CommonModule } from '@angular/common';
import { Toast } from 'bootstrap';
import { Router } from '@angular/router';
import { ModalComponent } from '../modal/modal.component';
import { InputComponent } from '../input/input.component';
import { InputConfig } from '../input/input.interface';
import { FormsModule } from '@angular/forms';
import { NewCryptoDto } from '../Api/ApiResult.interface';

@Component({
  selector: 'app-cryptocurrencies',
  imports: [ButtonComponent, CommonModule, ModalComponent, InputComponent, FormsModule],
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
  toastMessage: string = '';
  cryptoNames: string[] = [];
  addCryptoModal: string = 'addCryptoModal';
  cryptoAmount: number = 0;
  selectedCrypto: string = '';

  constructor(private route: ActivatedRoute, private apiService: ApiService, private router: Router) { }

  showToast(color: string, text: string): void {
    const toastElement = document.getElementById("liveToast");

    if (toastElement) {

      this.toastMessage = text;
      toastElement.classList.add(color)

      const toast = new Toast(toastElement);
      toast.show();

      setTimeout(() => {
        toast.hide()
        toastElement.classList.remove(color)
      }, 3000);
    }
  }

  ngOnInit(): void {
    this.route.queryParamMap.subscribe(param => {
      this.queryParams = param.get('wallet');
    });
    if (this.queryParams)
      this.apiService.getWalletElements(this.queryParams).subscribe({
        next: data => {
          this.walletInfo = data;
        },
        error: () => {
          this.showToast("text-bg-danger", "Some problem occured")
        }
      })

    this.apiService.getCryptoNames().subscribe({
      next: data => {
        this.cryptoNames = data;
      },
      error: () => {
        this.showToast("text-bg-danger", "Some problem occured")
      }
    })
  }

  deleteWallet(): void {
    if (this.queryParams)
      this.apiService.removeWallet(this.queryParams).subscribe({
        next: data => {
          if (data) {
            this.showToast("text-bg-success", "Wallet removed successfully")
            setTimeout(() => {
              this.router.navigate(['']);
            }, 2500);

          }
          else {
            this.showToast("text-bg-danger", "Some problem occured")
          }
        },
        error: () => {
          this.showToast("text-bg-danger", "Some problem occured")
        }
      })
  }

  addCrypto(): void {
    if (this.queryParams != null) {
      const newCrypto: NewCryptoDto = {
        walletId: this.queryParams,
        name: this.selectedCrypto,
        value: this.cryptoAmount
      }

      this.apiService.newCrypto(newCrypto).subscribe({
        next: data => {
          console.log(data)
        },
        error: error => {
          this.showToast("text-bg-danger", error)
        }
      })

    }
  }

  onWCryptoAmountChange(newValue: number): void {
    this.cryptoAmount = newValue;
  }

  onCryptoChange(event: Event) {
    const target = event.target as HTMLSelectElement;
    this.selectedCrypto = target.value;
  }

  deleteButton: ButtonConfig = {
    color: 'btn-danger',
    label: 'Delete Wallet',
    width: "fit-content",
    height: 'auto',
    type: 'button'
  }

  addCurrencyButton: ButtonConfig = {
    color: 'btn-primary',
    label: 'Add Cryptocurrency',
    width: "fit-content",
    height: 'auto',
    type: 'button'
  }

  turnConversionOnButton: ButtonConfig = {
    color: 'btn-success',
    label: 'Turn On Conversion',
    width: "fit-content",
    height: 'auto',
    type: 'button'
  }

  CryptoAmountInput: InputConfig<number> = {
    type: "Number",
    label: "Cryptocurrency amount"
  }

  closeButton: ButtonConfig = {
    color: 'btn-secondary',
    label: 'Close',
    width: "fit-content",
    height: 'auto',
    type: 'button'
  }

  addButton: ButtonConfig = {
    color: 'btn-success',
    label: 'Add',
    width: "fit-content",
    height: 'auto',
    type: 'button'
  }
}
