import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OrderStatusToDisplayNamePipe } from './pipes/order-status-to-display-name.pipe';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations:[
    OrderStatusToDisplayNamePipe
  ],
  exports: [
    OrderStatusToDisplayNamePipe
  ]
})
export class SharedModule { }
