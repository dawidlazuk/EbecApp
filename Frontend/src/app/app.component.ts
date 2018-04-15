import { Component } from '@angular/core';
import { ProductsService } from './services/products/products.service';
import * as $ from "jquery";
import { ShoppingCartService } from './services/shopping-cart-service';
import { OrdersService } from './services/orders/orders.service';
import { TeamsService } from './services/teams/teams.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [
    ProductsService,
    ShoppingCartService,
    OrdersService,
    TeamsService
  ]
})
export class AppComponent {
  title = 'app';

  showHideCart(showCart: boolean){
    if(showCart){
      $('app-shopping-cart').css("display", "block");
      $('app-products-list').attr("class","half-wide");
    }
    else{
      $('app-shopping-cart').css("display", "none");
      $('app-products-list').attr("class","full-wide");
    }
  }
}
