import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiService } from '../Api/api.service';
import { WalletDto } from '../Api/ApiResult.interface';
import { ButtonComponent } from '../button/button.component';
import { ButtonConfig } from '../button/button-interface';
import { CommonModule } from '@angular/common';
import { Toast } from 'bootstrap';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cryptocurrencies',
  imports: [ButtonComponent, CommonModule],
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
        error: (error) => {
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

  deleteButton: ButtonConfig = {
    color: 'btn-danger',
    label: 'Delete Wallet',
    width: "fit-content",
    height: 'auto',
    type: 'button'
  }

  updateButton: ButtonConfig = {
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
}
