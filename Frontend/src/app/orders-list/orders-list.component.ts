import { Component, OnInit } from '@angular/core';
import { OrdersService } from '../services/orders/orders.service';
import { IOrder } from '../order';
import { OrderStatus } from '../order-status.enum';

@Component({
  selector: 'app-orders-list',
  templateUrl: './orders-list.component.html',
  styleUrls: ['./orders-list.component.css']
})
export class OrdersListComponent implements OnInit {
  //TODO remove
  teamId: number = 1;

  OrderStatus = OrderStatus;
  orders: IOrder[];
  errorMessage: any;


  constructor(private _ordersService: OrdersService) { }

  ngOnInit() {
    this._ordersService.getOrders(this.teamId)
    .subscribe(orders => this.orders = orders,
              error => this.errorMessage = <any>error);
  }

}
