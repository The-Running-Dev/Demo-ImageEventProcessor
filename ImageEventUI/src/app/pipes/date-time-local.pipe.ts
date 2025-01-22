// src/app/pipes/date-time-local.pipe.ts
import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  standalone: false,
  name: 'dateTimeLocal'
})
export class DateTimeLocalPipe implements PipeTransform {
  transform(value: string): string {
    const date = new Date(value);
    return date.toLocaleString();
  }
}
