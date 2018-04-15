import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';
import * as $ from 'jquery';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { IProductType } from '../../shared/products/productType';
import { ShoppingCartService } from './shopping-cart-service';
import { OrdersService } from '../../services/orders/orders.service';

@Component({
  selector: 'app-shopping-cart',
  templateUrl: './shopping-cart.component.html',
  styleUrls: ['./shopping-cart.component.css']
})
export class ShoppingCartComponent implements OnInit {


  cartSubscription: Subscription;
  productsInCart: {productType: IProductType, amount: number}[];
  
  constructor(private _cart: ShoppingCartService,
              private _orders: OrdersService) { 
    console.log(_cart);
  }

  ngOnInit() {
    this.cartSubscription = this._cart.onCartChanged$
      .subscribe(products => this.productsInCart = products)
  }

  removeProduct(productType: IProductType){
      this._cart.removeProduct(productType);
  }

  makeOrder(){
    if(this.productsInCart.length > 0){
      this._orders.sendNewOrder(1, this.productsInCart);
      this._cart.clear();    
    }
  } 

  private handleError(err: HttpErrorResponse){
    console.log(err.message);
    return Observable.throw(err.message);
  }
}
