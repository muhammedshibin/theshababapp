export interface Inmate {
    id: number;
    fullName: string;
    dateOfBirth: string;
    address: string;
    emailAddress: string;
    phoneNumber: string;
    pictureUrl: string;
    inmatePhoto: File;
    status: number;
    isVisit: boolean;
    isInmateOnTopBed: boolean;
    age: number;
    amountDue: number;
    savings: number;
  }