import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdministratorProfilePageComponent } from './administrator-profile-page.component';

describe('AdministratorProfilePageComponent', () => {
  let component: AdministratorProfilePageComponent;
  let fixture: ComponentFixture<AdministratorProfilePageComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AdministratorProfilePageComponent]
    });
    fixture = TestBed.createComponent(AdministratorProfilePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
