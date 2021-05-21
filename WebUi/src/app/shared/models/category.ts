export class Category {
    id: number;
    createdOn?: Date = new Date();
    modfiedOn?: Date = new Date();
    name: string;
    isApplicableForVisitors: boolean = true;
    needToConsiderDays: boolean = true;
    defaultRate: number = 0;
    considerDefaultRate = false;
    coreCategory: boolean = false;
}
