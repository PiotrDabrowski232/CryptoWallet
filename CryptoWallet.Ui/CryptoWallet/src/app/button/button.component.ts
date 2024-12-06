import { Component, Input } from '@angular/core';
import { ButtonConfig } from './button-interface';

@Component({
  selector: 'app-button',
  standalone: true,
  imports: [],
  template: `
    <button 
    [type]="config.type || 'button'"
     class="btn"
     [class]="[config.color, config.additionalClass]"
     [style]="{
      width: config.width || 'auto',
      height: config.height || 'auto'
     }"
     >
      {{config.label || 'button'}}
    </button>
  `,
  styles: ``
})
export class ButtonComponent {
  @Input() config!: ButtonConfig;
}
