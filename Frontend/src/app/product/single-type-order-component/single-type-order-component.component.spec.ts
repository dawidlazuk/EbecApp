import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SingleTypeOrderComponentComponent } from './single-type-order-component.component';

describe('SingleTypeOrderComponentComponent', () => {
  let component: SingleTypeOrderComponentComponent;
  let fixture: ComponentFixture<SingleTypeOrderComponentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SingleTypeOrderComponentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SingleTypeOrderComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
