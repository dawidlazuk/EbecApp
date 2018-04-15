import { Component, OnInit, Input } from '@angular/core';
import { ProductType } from '../../../shared/products/productType';

@Component({
  selector: 'app-multi-type-order-component',
  templateUrl: './multi-type-order-component.component.html',
  styleUrls: ['./multi-type-order-component.component.css']
})
export class MultiTypeOrderComponentComponent implements OnInit {
  @Input() types: ProductType[];

  constructor() { 
  }

  ngOnInit() {
  }

}
