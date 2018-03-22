import { Component } from '@angular/core';
import { ProductsService } from './services/products/products.service';
import { ShoppingCartService } from './shopping-cart-service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [
    ProductsService,
    ShoppingCartService
  ]
})
export class AppComponent {
  title = 'app';
}
