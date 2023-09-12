import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MonthlyUsageComponent } from './monthly-usage.component';

describe('MonthlyUsageComponent', () => {
  let component: MonthlyUsageComponent;
  let fixture: ComponentFixture<MonthlyUsageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MonthlyUsageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MonthlyUsageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
