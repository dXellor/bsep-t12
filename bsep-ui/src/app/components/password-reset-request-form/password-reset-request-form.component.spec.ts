import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PasswordResetRequestFormComponent } from './password-reset-request-form.component';

describe('PasswordResetRequestFormComponent', () => {
  let component: PasswordResetRequestFormComponent;
  let fixture: ComponentFixture<PasswordResetRequestFormComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PasswordResetRequestFormComponent]
    });
    fixture = TestBed.createComponent(PasswordResetRequestFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
