import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { SidenavComponent } from './sidenav.component';
import { SharedModule } from '../../shared/shared.module';
import { SidenavService } from './sidenav.service';

describe('SidenavComponent', () => {
  let component: SidenavComponent;
  let fixture: ComponentFixture<SidenavComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [
        SharedModule
      ],
      declarations: [SidenavComponent],
      providers: [SidenavService]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SidenavComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
