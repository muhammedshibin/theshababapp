export interface ITenant {
    id: number;
    fullName: string;
    dateOfBirth: Date;
    address: string;
    emailAddress: string;
    phoneNumber: string;
    pictureUrl: string;
    inmatePhoto: File;
    status: number;
    isVisit: boolean;
    isInmateOnTopBed: boolean;
}

export class Tenant implements ITenant{
    id: number;
    fullName: string;
    dateOfBirth: Date;
    address: string;
    emailAddress: string;
    phoneNumber: string;
    pictureUrl: string;
    inmatePhoto: File;
    status: number;
    isVisit: boolean;
    isInmateOnTopBed: boolean;    
}