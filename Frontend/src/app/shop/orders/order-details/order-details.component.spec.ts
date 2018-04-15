import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ShopOrderDetailsComponent } from './order-details.component';

describe('OrderDetailsComponent', () => {
  let component: ShopOrderDetailsComponent;
  let fixture: ComponentFixture<ShopOrderDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ShopOrderDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ShopOrderDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
