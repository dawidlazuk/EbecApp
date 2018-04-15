import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CustomerComponent } from './customer.component';
import { RouterModule } from '@angular/router';
import { NavigationBarComponent } from './navigation-bar/navigation-bar.component';
import { ProductsListComponent } from './products-list/products-list.component';
import { ShoppingCartComponent } from './shopping-cart/shopping-cart.component';
import { ProductComponent } from './product/product.component';
import { SingleTypeOrderComponentComponent } from './product/single-type-order-component/single-type-order-component.component';
import { MultiTypeOrderComponentComponent } from './product/multi-type-order-component/multi-type-order-component.component';
import { CustomerOrdersListComponent } from './orders/orders-list/orders-list.component';
import { OrderDetailsComponent } from './orders/order-details/order-details.component';
import { SharedModule } from '../shared/shared.module';
import { TeamDetailsComponent } from './team-details/team-details.component';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    SharedModule
  ],
  declarations: [
    ShoppingCartComponent,
    TeamDetailsComponent,    
    ProductComponent,
    SingleTypeOrderComponentComponent,
    MultiTypeOrderComponentComponent,
    ProductsListComponent,
    NavigationBarComponent,
    CustomerComponent,  
    CustomerOrdersListComponent,
    OrderDetailsComponent
  ],
  entryComponents: [
    OrderDetailsComponent
  ],
})
export class CustomerModule { }
