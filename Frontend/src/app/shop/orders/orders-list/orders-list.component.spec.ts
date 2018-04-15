import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ShopOrdersListComponent } from './orders-list.component';

describe('OrdersListComponent', () => {
  let component: ShopOrdersListComponent;
  let fixture: ComponentFixture<ShopOrdersListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ShopOrdersListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ShopOrdersListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
