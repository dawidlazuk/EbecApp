import { Component } from '@angular/core';
import { ProductsService } from './services/products/products.service';
import { OrdersService } from './services/orders/orders.service';
import { TeamsService } from './services/teams/teams.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [
    ProductsService,
    OrdersService,
    TeamsService
  ]
})
export class AppComponent {
  title = 'app';


}
