import { Injectable } from '@angular/core';
import * as $ from 'jquery';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { IOrder, Order } from '../../shared/orders/order';
import { IProductType } from '../../shared/products/productType';
import { Team } from '../../shared/teams/team';
import 'rxjs/add/operator/map';
import { OrderStatus } from '../../shared/orders/order-status.enum';

@Injectable()
export class OrdersService {  
  ordersCustomerControllerUrl = "http://localhost:49906/api/customer/orders"
  ordersShopControllerUrl = "http://localhost:49906/api/shop/orders"

  constructor(private _http: HttpClient) { }

  sendNewOrder(teamId: number, products: {productType: IProductType, amount: number}[]){
    let makeOrderUrl = this.ordersCustomerControllerUrl;
    $.post({
      contentType: 'application/json',
      url: makeOrderUrl,
      data: this.createOrderRequest(1, products)
    });
  }

  getOrders(): Observable<IOrder[]>{
    let getOrdersUrl = this.ordersShopControllerUrl;

    return this._http.get<Order[]>(getOrdersUrl)
      .catch(this.handleError)
  }

  getTeamOrders(teamId: number): Observable<IOrder[]>{
    let getOrdersUrl = this.ordersCustomerControllerUrl + "/byTeam?teamId=" + teamId;

    return this._http.get<Order[]>(getOrdersUrl)
      .catch(this.handleError);
  }

  getOrderDetails(order: IOrder): Observable<any>{
    let getOrderDetailsUrl = this.ordersCustomerControllerUrl + "/" + order.id + "/details";

    return this._http.get<any>(getOrderDetailsUrl)
      .catch(this.handleError);    
  }

  cancelOrder(order: IOrder): Observable<IOrder> {
    let deleteOrderUrl = this.ordersCustomerControllerUrl + "/" + order.id;
    
    return this._http.delete<IOrder>(deleteOrderUrl)
      .catch(this.handleError);
  }

  requestStatusChange(orderId: number, status: OrderStatus): Observable<IOrder> {
    let changeStateUrl = this.ordersShopControllerUrl + "/" + orderId;
    
    var requestBody = JSON.stringify({"status" : status });
    alert(requestBody);

    return this._http.put<IOrder>(changeStateUrl, requestBody, { headers: {'Content-Type': 'application/json'} })
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

  private mapOrders(orders: any[]){     
    for(let order of orders)
    {
        let tempOrder = new Order();
        tempOrder.id = order.id;
        tempOrder.team = new Team();
        tempOrder.team.id = order.teamId;
        tempOrder.team.name = order.teamName;
        tempOrder.status = order.status;
        tempOrder.value = order.value;
        tempOrder.modifiedDate = order.modifiedDate;
        tempOrder.createdDate = order.createdDate;
        order = tempOrder;
    }
  }

  private mapProducts(products: any): any{
    let result : {productType: IProductType, amount: number}[] = [];
    for(let product of products)
      result.push({productType: product.productType, amount: product.amount});

    console.log("result: " + result);
    return result;
  }

  private handleError(err: HttpErrorResponse){
    console.error(err.message);
    alert("An error occured, see console.");
    return Observable.throw(err.message);
  }
}
