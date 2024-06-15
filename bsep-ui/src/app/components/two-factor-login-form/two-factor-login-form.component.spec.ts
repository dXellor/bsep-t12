import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TwoFactorLoginFormComponent } from './two-factor-login-form.component';

describe('TwoFactorLoginFormComponent', () => {
  let component: TwoFactorLoginFormComponent;
  let fixture: ComponentFixture<TwoFactorLoginFormComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TwoFactorLoginFormComponent]
    });
    fixture = TestBed.createComponent(TwoFactorLoginFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
