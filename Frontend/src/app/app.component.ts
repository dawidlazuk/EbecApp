import { Component } from '@angular/core';
import { ProductsService } from './services/products/products.service';
import * as $ from "jquery";
import { ShoppingCartService } from './services/shopping-cart-service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [
    ProductsService,
    ShoppingCartService
  ]
})
export class AppComponent {
  title = 'app';

  showHideCart(showCart: boolean){
      $('app-shopping-cart').css("display", showCart ? "block" : "none");
  }
}
