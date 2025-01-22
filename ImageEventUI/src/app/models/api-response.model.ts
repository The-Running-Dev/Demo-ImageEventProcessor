import {Image} from './image.model';

export interface ApiResponse {
  image: Image;
  hourlyCount: number;
  message: string;
}
