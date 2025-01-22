import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { ImageEventService } from './image-event.service';
import {ApiResponse, Image} from '../models';
import {environment} from '../../environments/environment';

describe('ImageEventService', () => {
  let service: ImageEventService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [ImageEventService]
    });

    service = TestBed.inject(ImageEventService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should fetch the latest event', () => {
    const mockResponse: ApiResponse = {
      image: { imageUrl: 'http://example.com/image.jpg', description: 'Test Image', timeStamp: '' } as Image,
      hourlyCount: 5
    } as ApiResponse;

    service.fetchLatestEvent().subscribe(response => {
      expect(response).toEqual(mockResponse);
    });

    const req = httpMock.expectOne(`\`${environment.apiUrl}/${environment.getLatestImageUrl}\`;`);
    expect(req.request.method).toBe('GET');
    req.flush(mockResponse);
  });

  it('should post an image event', () => {
    const mockEvent = { imageUrl: 'http://example.com/image.jpg', description: 'Test Image' } as any;
    const mockResponse = { ok: true } as any;

    service.postImageEvent(mockEvent).subscribe(response => {
      expect(response).toEqual(mockResponse);
    });

    const req = httpMock.expectOne(environment.apiUrl);
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual(mockEvent);
    req.flush(mockResponse);
  });
});
