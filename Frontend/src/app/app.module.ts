import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule, Routes, RouterOutlet } from '@angular/router';
import { MatDialogModule } from '@angular/material/dialog';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { ProductComponent } from './product/product.component';
import { SingleTypeOrderComponentComponent } from './product/single-type-order-component/single-type-order-component.component';
import { MultiTypeOrderComponentComponent } from './product/multi-type-order-component/multi-type-order-component.component';
import { ProductsListComponent } from './products-list/products-list.component';
import { HttpClientModule } from '@angular/common/http';
import { NavigationBarComponent } from './navigation-bar/navigation-bar.component';
import { ShoppingCartComponent } from './shopping-cart/shopping-cart.component';
import { OrdersListComponent } from './orders-list/orders-list.component';
import { OrderStatusToDisplayNamePipe } from './pipes/order-status-to-display-name.pipe';
import { OrderDetailsComponent } from './order-details/order-details.component';
import { TeamDetailsComponent } from './team-details/team-details.component';

const appRoutes: Routes = [
  { path: 'products', component: ProductsListComponent },
  { path: 'orders', component: OrdersListComponent },
  { path: 'cart', component: ShoppingCartComponent },

  { path: '**', component: ProductsListComponent }
];


@NgModule({
  declarations: [
    AppComponent,
    ProductComponent,
    SingleTypeOrderComponentComponent,
    MultiTypeOrderComponentComponent,
    ProductsListComponent,
    NavigationBarComponent,
    ShoppingCartComponent,
    OrdersListComponent,
    OrderStatusToDisplayNamePipe,
    OrderDetailsComponent,
    TeamDetailsComponent
  ],
  entryComponents: [
    OrderDetailsComponent
  ],
  imports: [
    RouterModule.forRoot(
      appRoutes,
      { enableTracing: true }
    ),
    BrowserModule,
    HttpClientModule,
    MatDialogModule,
    BrowserAnimationsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
