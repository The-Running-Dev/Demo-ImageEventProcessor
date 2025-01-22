import { TestBed, ComponentFixture } from '@angular/core/testing';
import { AppComponent } from './app.component';
import { ImageEventService } from '../services/image-event.service';
import { of } from 'rxjs';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { FormsModule } from '@angular/forms';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { DateTimeLocalPipe } from '../pipes/date-time-local.pipe';

describe('AppComponent', () => {
  let fixture: ComponentFixture<AppComponent>;
  let app: AppComponent;
  let imageEventService: jasmine.SpyObj<ImageEventService>;

  beforeEach(async () => {
    const spy = jasmine.createSpyObj('ImageEventService', ['fetchLatestEvent', 'postImageEvent']);

    await TestBed.configureTestingModule({
      declarations: [AppComponent, DateTimeLocalPipe],
      imports: [HttpClientTestingModule, FormsModule, MatToolbarModule, MatButtonModule],
      providers: [{ provide: ImageEventService, useValue: spy }]
    }).compileComponents();

    fixture = TestBed.createComponent(AppComponent);
    app = fixture.componentInstance;
    imageEventService = TestBed.inject(ImageEventService) as jasmine.SpyObj<ImageEventService>;
  });

  it('should create the app', () => {
    expect(app).toBeTruthy();
  });

  it('should render title', () => {
    fixture.detectChanges();
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('mat-toolbar span')?.textContent).toContain('Image Event UI');
  });

  it('should fetch the latest event on init', () => {
    const mockEvent = { image: { imageUrl: 'http://example.com/image.jpg', description: 'Test Image', timeStamp: new Date() }, hourlyCount: 5 } as any;
    imageEventService.fetchLatestEvent.and.returnValue(of(mockEvent));

    app.ngOnInit();
    expect(imageEventService.fetchLatestEvent).toHaveBeenCalled();
    expect(app.latestEvent).toEqual(mockEvent);
  });

  it('should post an image event', () => {
    const mockResponse = { ok: true } as any;
    imageEventService.postImageEvent.and.returnValue(of(mockResponse));

    app.imageUrl = 'http://example.com/image.jpg';
    app.description = 'Test Image';
    app.postImageEvent();

    expect(imageEventService.postImageEvent).toHaveBeenCalledWith({ imageUrl: 'http://example.com/image.jpg', description: 'Test Image' });
    expect(app.imageUrl).toBe('');
    expect(app.description).toBe('');
  });
});
