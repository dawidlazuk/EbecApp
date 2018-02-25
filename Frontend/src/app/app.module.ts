import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';


import { AppComponent } from './app.component';
import { ProductComponent } from './product/product.component';
import { OneTypeOrderComponentComponent } from './product/one-type-order-component/one-type-order-component.component';
import { MultiTypeOrderComponentComponent } from './product/multi-type-order-component/multi-type-order-component.component';


@NgModule({
  declarations: [
    AppComponent,
    ProductComponent,
    OneTypeOrderComponentComponent,
    MultiTypeOrderComponentComponent
  ],
  imports: [
    BrowserModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
