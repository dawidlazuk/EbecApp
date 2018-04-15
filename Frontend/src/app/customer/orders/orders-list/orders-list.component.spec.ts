import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerOrdersListComponent } from './orders-list.component';

describe('OrdersListComponent', () => {
  let component: CustomerOrdersListComponent;
  let fixture: ComponentFixture<CustomerOrdersListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CustomerOrdersListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CustomerOrdersListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
