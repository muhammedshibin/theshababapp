export class TransactionParams {
  pageNumber = 1;
  pageSize =  10;
  search: string;
  month?: number;
  year?: number;
  paidBy?: number;
  paidTo?: number;
  sortBy: string;
}
