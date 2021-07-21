export class HourRegistration{

    id: number;
    startTime: Date;
    endTime: Date;
    description: string;
    workedHours: number;
    recreation: number;
    recreationTimestamp: Date;
    employerId: number;
    details: boolean;
    
    public constructor(init?: Partial<HourRegistration>) {
        Object.assign(this, init);
    }
}
