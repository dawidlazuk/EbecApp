import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MultiTypeOrderComponentComponent } from './multi-type-order-component.component';

describe('MultiTypeOrderComponentComponent', () => {
  let component: MultiTypeOrderComponentComponent;
  let fixture: ComponentFixture<MultiTypeOrderComponentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MultiTypeOrderComponentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MultiTypeOrderComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
