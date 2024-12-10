import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiService } from '../Api/api.service';
import { CryptocurrencyDto, ValidationErrors, WalletDto } from '../Api/ApiResult.interface';
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
import { UpdateCryptoDto } from '../Api/ApiResult.interface';

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
  @ViewChild(InputComponent) inputComponent!: InputComponent;

  queryParams: string | null = '';
  walletInfo: WalletDto | null = null;
  toastMessage: string = '';
  updateCryptoValue: number | null = null;
  cryptoNames: string[] = [];
  addCryptoModal: string = 'addCryptoModal';
  updateCryptoModal: string = 'updateCryptoModal';
  cryptoAmount: number = 0;
  selectedCrypto: string = '';
  validationErrors: ValidationErrors | null = null;

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
          if (data)
            this.showToast("text-bg-success", `Crypto added`);
          this.validationErrors = null;
          if (this.queryParams)
            this.apiService.getWalletElements(this.queryParams).subscribe({
              next: data => {
                this.walletInfo = data;
              }
            })
          this.inputComponent.clearInput();
          var selector = document.getElementById("cryptoSelector") as HTMLInputElement;
          selector.value = "1"
        },
        error: (error) => {
          this.validationErrors = error as ValidationErrors;

          this.showToast("text-bg-danger", "some problem occured")
        }
      })
    }
  }

  RemoveCryptoFromWallet(cryptoId: string): void {
    this.apiService.removeCrypto(cryptoId).subscribe({
      next: data => {
        if (data)
          this.showToast("text-bg-success", `Crypto removed`);
        if (this.queryParams)
          this.apiService.getWalletElements(this.queryParams).subscribe({
            next: data => {
              this.walletInfo = data;
            }
          })
      },
      error: () => {
        this.showToast("text-bg-danger", "some problem occured")
      }
    })
  }

  updateCryptoCurrency(): void {
    this.cryptoToUpdate.name = this.selectedCrypto;
    this.cryptoToUpdate.value = this.cryptoAmount;
    this.cryptoToUpdate.walletId = this.queryParams ?? ''
    this.apiService.updateCrypto(this.cryptoToUpdate).subscribe({
      next: data => {
        if (data)
          this.showToast("text-bg-success", `Crypto updated`);
        this.validationErrors = null;
        if (this.queryParams)
          this.apiService.getWalletElements(this.queryParams).subscribe({
            next: data => {
              this.walletInfo = data;
            }
          })
      },
      error: (error) => {
        this.validationErrors = error as ValidationErrors;

        this.showToast("text-bg-danger", "some problem occured")
      }
    })
  }

  CalculateSum(): number {
    if (this.walletInfo)
      return this.walletInfo.currencies.reduce((sum, item) => sum + (item.coinPrice || 0) * (item.value || 0), 0);
    else
      return 0;
  }


  TurnOnConversion(): void {
    if (this.queryParams)
      this.apiService.getWalletElements(this.queryParams, true).subscribe({
        next: data => {
          this.walletInfo = data;
        },
        error: () => {
          this.showToast("text-bg-danger", "Some problem occured")
        }
      })
  }

  onWCryptoAmountChange(newValue: number): void {
    this.cryptoAmount = newValue;
  }

  onCryptoChange(event: Event) {
    const target = event.target as HTMLSelectElement;
    this.selectedCrypto = target.value;
  }

  setValues(crypto: CryptocurrencyDto): void {
    var selector = document.getElementById("cryptoUpdateSelector") as HTMLInputElement;
    selector.value = crypto.name;
    this.selectedCrypto = crypto.name;
    this.cryptoToUpdate.value = crypto.value;
    this.cryptoToUpdate.id = crypto.id;
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
    color: 'btn-primary',
    label: 'Add',
    width: "fit-content",
    height: 'auto',
    type: 'button'
  }

  removeCryptoButton: ButtonConfig = {
    color: 'btn-danger',
    width: "fit-content",
    height: 'auto',
    type: 'button',
    icon: 'bi-trash'
  }

  updateCryptoButton: ButtonConfig = {
    color: 'btn-warning',
    width: "fit-content",
    height: 'auto',
    type: 'button',
    icon: 'bi-list'
  }

  cryptoToUpdate: UpdateCryptoDto = {
  }
}
