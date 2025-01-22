import {Component, OnInit} from '@angular/core';
import {interval} from 'rxjs';
import {ImageEventService} from '../services/image-event.service';
import {ApiResponse} from '../models';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  standalone: false
})
export class AppComponent implements OnInit {
  latestEvent?: ApiResponse | null;
  imageUrl: string = 'http://…';
  description: string = 'This is a great image';

  constructor(private imageEventService: ImageEventService) {
  }

  ngOnInit(): void {
    interval(5000).subscribe(() => this.fetchLatestEvent());

    this.fetchLatestEvent();
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
    const imageEvent = {
      imageUrl: this.imageUrl,
      description: this.description
    };

    this.imageEventService.postImageEvent(imageEvent).subscribe(response => {
      if (response) {
        console.log('Image event posted successfully');

        this.fetchLatestEvent();

        this.imageUrl = '';
        this.description = '';
      }
    });
  }
}
