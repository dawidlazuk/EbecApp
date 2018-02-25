import { Component, OnInit } from '@angular/core';
import { Product } from './product';
import { ProductType } from './productType';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {
  lorem: string = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.";

  product: Product = new Product("Gwóźdź", this.lorem, "https://thumbs.dreamstime.com/b/gw%C3%B3%C5%BAd%C5%BA-15784424.jpg", [new ProductType(),new ProductType()]);

  constructor() { }

  ngOnInit() {
  }

}
