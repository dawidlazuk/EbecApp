import { Injectable } from '@angular/core';
import { IProductType } from '../../product/productType';
import * as $ from 'jquery';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { IOrder, Order } from '../../order';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class OrdersService {
  ordersControllerUrl = "http://localhost:49906/api/orders"
  
  constructor(private _http: HttpClient) { }

  sendNewOrder(teamId: number, products: {productType: IProductType, amount: number}[]){
    let makeOrderUrl = this.ordersControllerUrl;
    $.post({
      contentType: 'application/json',
      url: makeOrderUrl,
      data: this.createOrderRequest(1, products)
    });
  }

  getOrders(teamId: number): Observable<IOrder[]>{
    let getOrdersUrl = this.ordersControllerUrl + "?teamId=" + teamId;

    return this._http.get<Order[]>(getOrdersUrl)
      .do(this.mapOrders)
      .catch(this.handleError);
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

  private mapOrders(orders: IOrder[]){     
    let orderObjects: IOrder[] = [];
    orders.forEach(order => {
        let tempOrder = new Order();
        tempOrder.status = order.status;
        tempOrder.value = order.value;
        tempOrder.modifiedDate = order.modifiedDate;
        tempOrder.createdDate = order.createdDate;
        orderObjects.push(tempOrder);
    });
    return orderObjects;
  }

  private handleError(err: HttpErrorResponse){
    console.log(err.message);
    return Observable.throw(err.message);
  }
}
