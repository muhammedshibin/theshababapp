export interface BillItem {
    itemName: string;
    itemCategoryName: string;
    amount: number;
}

export interface InmateBill {
    id: number;
    month: string;
    year: number;
    billAmount: number;
    inmate: string;
    inmateId: number;
    paymentStatus: number;
    billItems: BillItem[];
}
