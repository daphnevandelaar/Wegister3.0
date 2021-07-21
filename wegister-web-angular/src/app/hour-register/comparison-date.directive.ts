import { FormGroup } from '@angular/forms';

export function ComparisonDateValidator(control: FormGroup) {
  if(control.get('startTime').value && control.get('endTime').value){

    var startDate = new Date(control.get('startTime').value);
    var endDate = new Date(control.get('endTime').value);

    if (
      startDate.getDay() != endDate.getDay() || 
      startDate.getMonth() != endDate.getMonth()
    ) { 
      // console.log('uncorrespondingDay');
      return 'uncorrespondingDay'; 
    }
    else if (startDate.getTime() > endDate.getTime()) {
      // console.log('negativeHour');
      return 'negativeHour';
    }
    else {
      // console.log('null');
      return null;
    }
  }
}
