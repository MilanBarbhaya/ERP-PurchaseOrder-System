import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PoListComponent } from './po-list';

describe('PoList', () => {
  let component: PoListComponent;
  let fixture: ComponentFixture<PoListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PoListComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(PoListComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
