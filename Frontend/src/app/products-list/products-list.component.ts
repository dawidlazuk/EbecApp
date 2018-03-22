import { Component, OnInit } from '@angular/core';
import { Product } from '../product/product';
import { ProductType } from '../product/productType';
import { ProductsService } from '../services/products/products.service';

@Component({
  selector: 'app-products-list',
  templateUrl: './products-list.component.html',
  styleUrls: ['./products-list.component.css']
})
export class ProductsListComponent implements OnInit {
  lorem: string = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.";

  products: Product[] = [
    new Product("Gwóźdź", this.lorem, "https://thumbs.dreamstime.com/b/gw%C3%B3%C5%BAd%C5%BA-15784424.jpg", [new ProductType(null),new ProductType(null)]),
    new Product("Gwóźdź", this.lorem, "https://thumbs.dreamstime.com/b/gw%C3%B3%C5%BAd%C5%BA-15784424.jpg", [new ProductType(null)])
  ];

  errorMessage: string;

  constructor(private _productService: ProductsService) {
  }

  ngOnInit() {
    this._productService.getProducts()
      .subscribe(products => this.products = products,
                error => this.errorMessage = <any>error);
  }

}
