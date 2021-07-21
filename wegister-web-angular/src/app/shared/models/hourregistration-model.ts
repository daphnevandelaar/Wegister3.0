export class HourRegistration{
    guid: string;
    id: number;
    startTime: Date;
    endTime: Date;
    description: string;
    recreation: string;
    employerId: string;
    weekNumber: string;
    totalWorkedHoursDayInSeconds: number;
    details: boolean;

    public constructor(init?: Partial<HourRegistration>) {
        Object.assign(this, init);
    }
}
