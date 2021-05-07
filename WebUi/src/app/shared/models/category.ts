export interface Category {
    id: number;
    createdOn: Date;
    modfiedOn: Date;
    name: string;
    isApplicableForVisitors: boolean;
    needToConsiderDays: boolean;
    defaultRate: number;
}
