import { Injectable } from '@angular/core';
import { IProductType } from '../../product/productType';
import * as $ from 'jquery';

@Injectable()
export class OrdersService {
  makeOrderUrl = "http://localhost:49906/api/orders"

  constructor() { }

  sendNewOrder(teamId: number, products: {productType: IProductType, amount: number}[]){
    $.post({
      contentType: 'application/json',
      url: this.makeOrderUrl,
      data: this.createOrderRequest(1, products)
    });
  }

  private createOrderRequest(teamId: number, products: {productType: IProductType, amount: number}[]): string {    
    var productsByIndex: {[index: number]:number} = {}; 
    products.forEach(productAmount => 
      productsByIndex[productAmount.productType.id] = productAmount.amount
    );
    let order: any =
    {
      "teamId": teamId,
      "products": productsByIndex
    }
    return JSON.stringify(order);
  }
}
