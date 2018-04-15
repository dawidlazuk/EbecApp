import { Component, OnInit } from '@angular/core';
import { ShoppingCartService } from './shopping-cart/shopping-cart-service';
import * as $ from "jquery";

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.css'],
  providers: [
    ShoppingCartService
  ]
})
export class CustomerComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

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
