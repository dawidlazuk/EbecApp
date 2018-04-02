import { Component, OnInit } from '@angular/core';
import { ShoppingCartService } from '../services/shopping-cart-service';
import { Subscription } from 'rxjs/Subscription';
import { IProductType } from '../product/productType';
import * as $ from 'jquery';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

@Component({
  selector: 'app-shopping-cart',
  templateUrl: './shopping-cart.component.html',
  styleUrls: ['./shopping-cart.component.css']
})
export class ShoppingCartComponent implements OnInit {

  makeOrderUrl = "http://localhost:49906/api/orders"

  cartSubscription: Subscription;
  productsInCart: {productType: IProductType, amount: number}[];
  
  constructor(private _cart: ShoppingCartService,
              private _http: HttpClient) { 
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
    console.log("makeOrderPressed");
    console.log("data: " + this.createOrderRequest());
    $.post({
      // type: "POST",
      contentType: 'application/json',
      url: this.makeOrderUrl,
      data: this.createOrderRequest()
    })

    // this._http.post(this.makeOrderUrl,this.createOrderRequest())
    //   .catch(this.handleError);
  }

  createOrderRequest(): string {    
    var products: {[index: number]:number} = {}; 
    this.productsInCart.forEach(productAmount => 
      products[productAmount.productType.id] = productAmount.amount
    );
    let order: any =
    {
      "teamId": "1", //TODO change
      "products": products
    }
    return JSON.stringify(order);
  }

  private handleError(err: HttpErrorResponse){
    console.log(err.message);
    return Observable.throw(err.message);
  }
}
