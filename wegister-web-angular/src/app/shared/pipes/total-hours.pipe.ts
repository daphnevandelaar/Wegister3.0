import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'totalHours'
})
export class TotalHoursPipe implements PipeTransform {

  transform(value: number, ...args: unknown[]): unknown {
    var hours =   Math.round((value / 3600));
    var minutes = Math.round((value / 60) % 60);
    var seconds = Math.round((value % 60));
    
    var formattedHours = ("0" + hours).slice(-2);
    var formattedMinutes = ("0" + minutes).slice(-2);
    var formattedSeconds = ("0" + seconds).slice(-2);

    return `${formattedHours}:${formattedMinutes}:${formattedSeconds}`;
  }

}
