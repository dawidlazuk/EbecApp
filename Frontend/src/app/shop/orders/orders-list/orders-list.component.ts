import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ShopOrderDetailsComponent } from '../order-details/order-details.component';
import { OrdersService } from '../../../services/orders/orders.service';
import { IOrder } from '../../../shared/orders/order';
import { OrderStatus } from '../../../shared/orders/order-status.enum';

@Component({
  selector: 'app-orders-list',
  templateUrl: './orders-list.component.html',
  styleUrls: ['./orders-list.component.css']
})
export class ShopOrdersListComponent implements OnInit {
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
        let detailsDialog = this.dialog.open(ShopOrderDetailsComponent, {
          data: { order: order }
        });
    });
  }

  modifyOrderState(order: IOrder, status: OrderStatus): void{
    alert("modify order state clicked");
    this._ordersService.requestStatusChange(order.id, status)
      .subscribe(order => this.refreshOrders());
  }

  cancelOrder(order: IOrder): void{
    if(confirm("Are you sure to cancel order " + order.id + "?")) {   
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
  }

  refreshOrders(): void{
    this._ordersService.getOrders()
    .subscribe(orders => {
      this.orders = orders;
      console.log(orders)
    },
    error => this.errorMessage = <any>error);
  }
}
