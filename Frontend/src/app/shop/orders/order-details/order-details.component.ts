import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { IOrder } from '../../../shared/orders/order';

@Component({
  selector: 'app-order-details',
  templateUrl: './order-details.component.html',
  styleUrls: ['./order-details.component.css']
})
export class ShopOrderDetailsComponent implements OnInit {
  
  order: IOrder;

  constructor(
    public dialogRef: MatDialogRef<ShopOrderDetailsComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) 
    {
      this.order = data.order;     
    }

  ngOnInit() {
  }

}
