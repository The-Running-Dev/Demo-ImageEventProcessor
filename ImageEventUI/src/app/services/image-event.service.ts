import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ApiResponse } from '../models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ImageEventService {
  private postImageUrl = environment.apiUrl;
  private getImageUrl = `${environment.apiUrl}/${environment.getLatestImageUrl}`;

  constructor(private http: HttpClient) {}

  fetchLatestEvent(): Observable<ApiResponse | null> {
    return this.http.get<ApiResponse>(this.getImageUrl).pipe(
      catchError(this.handleError)
    );
  }

  postImageEvent(imageEvent: any): Observable<ApiResponse | null> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });

    return this.http.post<ApiResponse>(this.postImageUrl, imageEvent, { headers }).pipe(
      catchError(this.handleError)
    );
  }

  private handleError(error: HttpErrorResponse): Observable<null> {
    if (error.error instanceof ErrorEvent) {
      console.error('An error occurred:', error.error.message);
    } else {
      console.error(
        `Backend returned code ${error.status}, ` +
        `body was: ${error.error}`);
    }

    return of(null);
  }
}
