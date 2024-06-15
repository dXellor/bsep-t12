import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EnableTfaPageComponent } from './enable-tfa-page.component';

describe('EnableTfaPageComponent', () => {
  let component: EnableTfaPageComponent;
  let fixture: ComponentFixture<EnableTfaPageComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [EnableTfaPageComponent]
    });
    fixture = TestBed.createComponent(EnableTfaPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
