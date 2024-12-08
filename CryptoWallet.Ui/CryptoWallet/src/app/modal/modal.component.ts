import { Component, Input } from '@angular/core';



@Component({
  selector: 'app-modal',
  imports: [],
  templateUrl: 'modal.component.html',
  styles: ``
})
export class ModalComponent {
  @Input() dataBsToggle!: string;
  @Input() dataBsTarget!: string;
}
