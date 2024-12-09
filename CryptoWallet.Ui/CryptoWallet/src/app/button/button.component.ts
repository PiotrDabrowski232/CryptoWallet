import { Component, Input } from '@angular/core';
import { ButtonConfig } from './button-interface';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-button',
  standalone: true,
  imports: [CommonModule],
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
      {{config.label}}
      <i *ngIf="config.icon" class="bi " [class]="config.icon"></i>
    </button>
  `,
  styles: ``
})
export class ButtonComponent {
  @Input() config!: ButtonConfig;
}
