import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShopComponent } from './shop.component';
import { NavigationBarComponent } from './navigation-bar/navigation-bar.component';
import { SharedModule } from '../shared/shared.module';
import { RouterModule } from '@angular/router';
import { ShopOrdersListComponent } from './orders/orders-list/orders-list.component';
import { ShopOrderDetailsComponent } from './orders/order-details/order-details.component';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    SharedModule
  ],
  declarations: [
    ShopComponent,
    NavigationBarComponent,
    ShopOrdersListComponent,
    ShopOrderDetailsComponent
  ],
  entryComponents: [
    ShopOrderDetailsComponent
  ],
})
export class ShopModule { }
