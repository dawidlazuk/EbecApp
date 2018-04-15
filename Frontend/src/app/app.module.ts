import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule, Routes, RouterOutlet } from '@angular/router';
import { MatDialogModule } from '@angular/material/dialog';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { SharedModule } from './shared/shared.module';
import { OrdersListComponent } from './shared/orders/orders-list/orders-list.component';
import { CustomerModule } from './customer/customer.module';
import { CustomerComponent } from './customer/customer.component';
import { ProductsListComponent } from './customer/products-list/products-list.component';
import { ShoppingCartComponent } from './customer/shopping-cart/shopping-cart.component';

const appRoutes: Routes = [  
  { path: 'customer', component: CustomerComponent, 
    children: [
      { path: 'products', component: ProductsListComponent },
      { path: 'orders', component: OrdersListComponent },
      { path: 'cart', component: ShoppingCartComponent },
      { path: '**', component: ProductsListComponent }
    ]},
    { path: '**', redirectTo: 'customer', pathMatch: 'full'} //temporary
  //{ path: '**', component: CustomerComponent } //set for login
];


@NgModule({
  declarations: [
    AppComponent,
  ],
 
  imports: [
    RouterModule.forRoot(
      appRoutes,
      { enableTracing: true }
    ),
    BrowserModule,
    HttpClientModule,
    MatDialogModule,
    BrowserAnimationsModule,
    
    SharedModule,
    CustomerModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
