import { Component, EventEmitter, Input, Output, output } from '@angular/core';
import { InputConfig } from './input.interface';

@Component({
  selector: 'app-input',
  imports: [],
  template: `
    <div class="form-floating mb-3">
      <input #WalletNameAdded [type]="config.type" class="form-control" id="floatingInput" [placeholder]="config.label" (input)="onInput($event)">
      <label for="floatingInput">{{config.label}}</label>
    </div>
  `,
  styles: ``
})
export class InputComponent<T = string> {
  @Input() config!: InputConfig<T>;
  @Output() providedWalletName = new EventEmitter<T>();

  onInput(event: Event): void {
    const input = event.target as HTMLInputElement;
    let value: any = input.value;

    if (this.config.type === 'Number')
      value = Number(value);
    if (this.config.type === 'Text')
      value = String(value);

    this.providedWalletName.emit(value as T);
  }
  clearInput(): void {
    const input = document.getElementById("floatingInput") as HTMLInputElement;
    input.value = '';
  }
}
