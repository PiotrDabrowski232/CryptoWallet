import { Component, EventEmitter, input, Input, Output, output } from '@angular/core';
import { InputConfig } from './input.interface';

@Component({
  selector: 'app-input',
  imports: [],
  template: `
    <div class="form-floating mb-3">
      <input #WalletNameAdded [type]="config.type" class="form-control" id="floatingInput" [value]="name" [placeholder]="config.label" (input)="onInput($event)">
      <label for="floatingInput">{{config.label}}</label>
    </div>
  `,
  styles: ``
})
export class InputComponent<T = string> {
  @Input() config!: InputConfig<T>;
  @Input() name?: string | number | null = null;
  @Output() providedValue = new EventEmitter<T>();

  onInput(event: Event): void {
    const input = event.target as HTMLInputElement;
    let value: any = input.value;

    if (this.config.type === 'Number')
      value = Number(value);
    if (this.config.type === 'Text')
      value = String(value);

    this.providedValue.emit(value as T);
  }

  clearInput(): void {
    const input = document.getElementById("floatingInput") as HTMLInputElement;
    input.value = '';
  }
}
