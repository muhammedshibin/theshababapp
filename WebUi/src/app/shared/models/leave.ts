export interface Leave {
    id: number;
    leaveReason: string;
    fromDate: Date;
    toDate: Date;
    inmate: string;
    inmateId: number;
    status: string;
}