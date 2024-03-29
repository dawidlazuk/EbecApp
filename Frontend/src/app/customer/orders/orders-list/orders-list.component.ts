import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { OrderDetailsComponent } from '../order-details/order-details.component';
import { OrdersService } from '../../../services/orders/orders.service';
import { IOrder } from '../../../shared/orders/order';
import { OrderStatus } from '../../../shared/orders/order-status.enum';

@Component({
  selector: 'app-orders-list',
  templateUrl: './orders-list.component.html',
  styleUrls: ['./orders-list.component.css']
})
export class CustomerOrdersListComponent implements OnInit {
  //TODO remove
  teamId: number = 1;

  OrderStatus = OrderStatus;
  orders: IOrder[];
  errorMessage: any;

  constructor(private _ordersService: OrdersService,
              public dialog: MatDialog) { }

  ngOnInit() {
    this.refreshOrders();
  }

  showDetails(order: IOrder): void{
    this._ordersService.getOrderDetails(order)
      .subscribe(details => {
        order.products = details.products;    
        console.log("prod: " + order.products);        
        let detailsDialog = this.dialog.open(OrderDetailsComponent, {
          data: { order: order }
        });
    });
  }

  cancelOrder(order: IOrder): void{
    this._ordersService.cancelOrder(order)
      .subscribe(
        cancelledOrder =>{
          if(cancelledOrder.status === OrderStatus.cancelled)
            this.refreshOrders();
          else
            alert("The order has not been cancelled.");
        }
      );
  }

  refreshOrders(): void{
    this._ordersService.getTeamOrders(this.teamId)
    .subscribe(orders => this.orders = orders,
              error => this.errorMessage = <any>error);
  }
}
