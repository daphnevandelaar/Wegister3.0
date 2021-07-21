import { Status } from '../shared/models/status-model';

export class WorkWeek{
    id: string;
    weekNumber: number;
    startDate: Date;
    endDate: Date;
    status: Status;
}