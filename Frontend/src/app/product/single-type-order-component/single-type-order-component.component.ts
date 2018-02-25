import { Component, OnInit, Input } from '@angular/core';
import { ProductType } from '../productType';

@Component({
  selector: 'app-single-type-order-component',
  templateUrl: './single-type-order-component.component.html',
  styleUrls: ['./single-type-order-component.component.css']
})
export class SingleTypeOrderComponentComponent implements OnInit {
  @Input() type: ProductType;

  constructor() { }

  ngOnInit() {
  }

}
