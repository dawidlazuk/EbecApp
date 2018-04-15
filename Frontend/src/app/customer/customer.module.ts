import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TeamDetailsComponent } from '../team-details/team-details.component';
import { CustomerComponent } from './customer.component';
import { RouterModule } from '@angular/router';
import { NavigationBarComponent } from './navigation-bar/navigation-bar.component';
import { ProductsListComponent } from './products-list/products-list.component';
import { ShoppingCartComponent } from './shopping-cart/shopping-cart.component';
import { ProductComponent } from './product/product.component';
import { SingleTypeOrderComponentComponent } from './product/single-type-order-component/single-type-order-component.component';
import { MultiTypeOrderComponentComponent } from './product/multi-type-order-component/multi-type-order-component.component';

@NgModule({
  imports: [
    CommonModule,
    RouterModule
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
  ]
})
export class CustomerModule { }
