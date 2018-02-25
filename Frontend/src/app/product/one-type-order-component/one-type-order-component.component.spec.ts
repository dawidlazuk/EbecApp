import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OneTypeOrderComponentComponent } from './one-type-order-component.component';

describe('OneTypeOrderComponentComponent', () => {
  let component: OneTypeOrderComponentComponent;
  let fixture: ComponentFixture<OneTypeOrderComponentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OneTypeOrderComponentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OneTypeOrderComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
