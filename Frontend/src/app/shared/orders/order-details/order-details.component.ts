import { Component, OnInit, Inject } from '@angular/core';
import { IOrder } from '../order';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-order-details',
  templateUrl: './order-details.component.html',
  styleUrls: ['./order-details.component.css']
})
export class OrderDetailsComponent implements OnInit {
  
  order: IOrder;

  constructor(
    public dialogRef: MatDialogRef<OrderDetailsComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) 
    {
      this.order = data.order;
      console.log(this.order.products);      
    }

  ngOnInit() {
  }

}
