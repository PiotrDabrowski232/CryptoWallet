import { Component, input, Input } from '@angular/core';



@Component({
  selector: 'app-modal',
  imports: [],
  templateUrl: 'modal.component.html',
  styles: ``
})
export class ModalComponent {
  @Input() dataBsTarget!: string;
}
