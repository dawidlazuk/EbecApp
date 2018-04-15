import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OrderDetailsComponent } from './orders/order-details/order-details.component';
import { OrderStatusToDisplayNamePipe } from './pipes/order-status-to-display-name.pipe';
import { OrdersListComponent } from './orders/orders-list/orders-list.component';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [
    OrdersListComponent,
    OrderStatusToDisplayNamePipe,
    OrderDetailsComponent
  ],
  entryComponents: [
    OrderDetailsComponent
  ],
})
export class SharedModule { }
