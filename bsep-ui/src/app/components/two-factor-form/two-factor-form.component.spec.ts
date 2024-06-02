import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TwoFactorFormComponent } from './two-factor-form.component';

describe('TwoFactorFormComponent', () => {
  let component: TwoFactorFormComponent;
  let fixture: ComponentFixture<TwoFactorFormComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TwoFactorFormComponent]
    });
    fixture = TestBed.createComponent(TwoFactorFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
