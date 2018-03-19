import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';


import { AppComponent } from './app.component';
import { ProductComponent } from './product/product.component';
import { SingleTypeOrderComponentComponent } from './product/single-type-order-component/single-type-order-component.component';
import { MultiTypeOrderComponentComponent } from './product/multi-type-order-component/multi-type-order-component.component';
import { ProductsListComponent } from './products-list/products-list.component';
import { HttpClientModule } from '@angular/common/http';


@NgModule({
  declarations: [
    AppComponent,
    ProductComponent,
    SingleTypeOrderComponentComponent,
    MultiTypeOrderComponentComponent,
    ProductsListComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
