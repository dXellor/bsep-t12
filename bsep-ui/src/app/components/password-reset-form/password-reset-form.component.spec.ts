import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PasswordResetFormComponent } from './password-reset-form.component';

describe('PasswordResetFormComponent', () => {
  let component: PasswordResetFormComponent;
  let fixture: ComponentFixture<PasswordResetFormComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PasswordResetFormComponent]
    });
    fixture = TestBed.createComponent(PasswordResetFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
