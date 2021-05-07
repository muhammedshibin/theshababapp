export interface Transaction {
    id: number;
    name: string;
    transactionDate: Date;
    isExpense: boolean;
    paidPartyId: number;
    paidParty: string;
    paidToId: number;
    paidToParty: string;
    amount: number;
    categoryId: number;
    category: string;
}