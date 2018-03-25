import { Component, OnInit } from '@angular/core';
import { ShoppingCartService } from '../services/shopping-cart-service';
import { Subscription } from 'rxjs/Subscription';
import { IProductType } from '../product/productType';

@Component({
  selector: 'app-shopping-cart',
  templateUrl: './shopping-cart.component.html',
  styleUrls: ['./shopping-cart.component.css']
})
export class ShoppingCartComponent implements OnInit {
  cartSubscription: Subscription;
  productsInCart: {productType: IProductType, amount: number}[];
  
  constructor(private _cart: ShoppingCartService) { 
    console.log(_cart);
  }

  ngOnInit() {
    this.cartSubscription = this._cart.onCartChanged$
      .subscribe(products => this.productsInCart = products)
  }

}
