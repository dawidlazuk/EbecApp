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
import { TeamDetailsComponent } from './team-details/team-details.component';
import { SharedModule } from './shared/shared.module';
import { OrdersListComponent } from './shared/orders/orders-list/orders-list.component';

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
    TeamDetailsComponent
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
    SharedModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
