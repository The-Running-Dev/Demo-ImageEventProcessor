import { Component, OnInit, AfterViewInit, ViewChild, ElementRef } from '@angular/core';

import {delay, interval} from 'rxjs';
import {ImageEventService} from '../services/image-event.service';
import {ApiResponse} from '../models';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  standalone: false
})
export class AppComponent implements OnInit, AfterViewInit {
  @ViewChild('imageUrlInput') imageUrlInput!: ElementRef;
  latestEvent?: ApiResponse | null;
  imageUrl: string = 'https://i.pinimg.com/736x/e5/b9/81/e5b98110fcd62d6ebe0e636262170175.jpg';
  description: string = 'This is a one funny dog!';
  isPosting: boolean = false;

  constructor(private imageEventService: ImageEventService) {
  }

  ngOnInit(): void {
    interval(5000).subscribe(() => this.fetchLatestEvent());

    this.fetchLatestEvent();
  }

  ngAfterViewInit(): void {
    this.imageUrlInput.nativeElement.focus();
    this.imageUrlInput.nativeElement.select();
  }

  fetchLatestEvent(): void {
    this.imageEventService.fetchLatestEvent().subscribe(data => {
      if (data && data.image) {
        this.latestEvent = data;
      } else if (data) {
        console.log(data.message);
      }
    });
  }

  postImageEvent(): void {
    this.isPosting = true;
    const imageEvent = {
      imageUrl: this.imageUrl,
      description: this.description
    };

    this.imageEventService.postImageEvent(imageEvent).pipe(
      delay(2000) // Add a 2-second delay
    ).subscribe(response => {
      if (response) {
        console.log('Image event posted successfully');

        this.fetchLatestEvent();

        this.imageUrl = '';
        this.description = '';
      }

      this.isPosting = false;
    });
  }
}
