import { Component, OnInit, Input } from '@angular/core';
import { ProductType } from '../productType';
import { ShoppingCartService } from '../../shopping-cart-service';
import * as $ from "jquery";

@Component({
  selector: 'app-single-type-order-component',
  templateUrl: './single-type-order-component.component.html',
  styleUrls: ['./single-type-order-component.component.css']
})
export class SingleTypeOrderComponentComponent implements OnInit {
  @Input() type: ProductType;

  constructor(private _cart: ShoppingCartService) { }

  ngOnInit() {
  }

  onAddProductToCart(amount: number){
    console.log('amount: ',amount);
    if(this._cart.addProduct(this.type, amount) == false)
      alert('Product not added');
  }
}
