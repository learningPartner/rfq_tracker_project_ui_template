import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UnauthorisedAccess } from './unauthorised-access';

describe('UnauthorisedAccess', () => {
  let component: UnauthorisedAccess;
  let fixture: ComponentFixture<UnauthorisedAccess>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UnauthorisedAccess]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UnauthorisedAccess);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
