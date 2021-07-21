import { HourRegistration } from '../../shared/models/hourregistration-model';

export class HourRegistrationList{
    hourRegistrations: HourRegistration[];
    totalWorkedHoursInSeconds: number;
    details: boolean;
}