import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TfaPageComponent } from './tfa-page.component';

describe('TfaPageComponent', () => {
  let component: TfaPageComponent;
  let fixture: ComponentFixture<TfaPageComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TfaPageComponent]
    });
    fixture = TestBed.createComponent(TfaPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
