import { Component, OnInit, ViewChild } from '@angular/core';
import { ButtonComponent } from '../button/button.component';
import { ButtonConfig } from '../button/button-interface';
import { WalletBasicInfo } from '../Api/ApiResult.interface';
import { CommonModule } from '@angular/common';
import { InputComponent } from '../input/input.component';
import { InputConfig } from '../input/input.interface';
import { ApiService } from '../Api/api.service';
import { Modal, Toast } from 'bootstrap';
import { Router } from '@angular/router';

@Component({
  selector: 'app-wallets',
  imports: [ButtonComponent, CommonModule, InputComponent],
  templateUrl: './wallets.components.html',
  styles: `
    ::ng-deep .newWallet { 
        position: absolute;
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
        padding-left: 7vw;
        padding-right: 3vw;
      }

      .card{
        margin: 1rem;
      }

      .noWallets{
        position: fixed;
        top:20vh;
        left: 42%;
      }`
})
export class WalletsComponent implements OnInit {

  @ViewChild(InputComponent) inputComponent!: InputComponent;

  newWalletName: string = '';
  toastMessage: string = '';
  walletList: WalletBasicInfo[] = [];

  constructor(private apiService: ApiService, private router: Router) { }

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
      this.apiService.newWallet(this.newWalletName).subscribe({
        next: data => {
          this.showToast("text-bg-success", "Wallet added successfully")
          this.newWalletName = '';
          this.inputComponent.clearInput();

          const modal = document.getElementById("exampleModal");

          if (modal) {
            const modalInstance = Modal.getInstance(modal);
            console.log(modalInstance)
            modalInstance?.hide();
          }

          const backdrop = document.querySelector('.modal-backdrop');
          if (backdrop) {
            backdrop.remove();
          }

          this.navigateToWallet(data);

        },
        error: (error) => {
          if (error)
            this.showToast("text-bg-danger", error)
        }
      })
    }
  }

  navigateToWallet(param: string): void {
    this.router.navigate(['/Wallet'], {
      queryParams: { wallet: param }
    });
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
