import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdministratorUserManagingPageComponent } from './administrator-user-managing-page.component';

describe('AdministratorUserManagingPageComponent', () => {
  let component: AdministratorUserManagingPageComponent;
  let fixture: ComponentFixture<AdministratorUserManagingPageComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AdministratorUserManagingPageComponent]
    });
    fixture = TestBed.createComponent(AdministratorUserManagingPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
