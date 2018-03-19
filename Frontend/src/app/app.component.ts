import { Component } from '@angular/core';
import { ProductsService } from './services/products/products.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [ProductsService]
})
export class AppComponent {
  title = 'app';
}
