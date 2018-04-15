import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import * as $ from 'jquery';

@Component({
  selector: 'app-navigation-bar',
  templateUrl: './navigation-bar.component.html',
  styleUrls: ['./navigation-bar.component.css']
})
export class NavigationBarComponent implements OnInit {
  @Output() onCartClicked = new EventEmitter<boolean>();
  isCartShown: boolean = false;

  constructor() { }

  ngOnInit() {
  }

  showHideCart(){
      this.isCartShown = !this.isCartShown;
      this.onCartClicked.emit(this.isCartShown);
      if(this.isCartShown)
        $("#cart-button").attr("class","btn btn-success");
      else
        $("#cart-button").attr("class","btn btn-default");      
  }
}
