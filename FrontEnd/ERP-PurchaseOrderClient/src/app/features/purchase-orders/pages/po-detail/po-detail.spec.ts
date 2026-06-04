import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PoDetailComponent } from './po-detail';

describe('PoDetail', () => {
  let component: PoDetailComponent;
  let fixture: ComponentFixture<PoDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PoDetailComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(PoDetailComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
