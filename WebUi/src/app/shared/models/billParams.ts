export class BillParams{
    
    /**
     *
     */
    constructor(pageNumber: number , pageSize: number , month: number , year: number) {
        this.month = month;
        this.year = year;
        this.pageNumber = pageNumber;
        this.pageSize = pageSize;        
    }
   
    
    pageNumber: number;
    pageSize: number;
    search: string;
    paidPredicate: string;
    month: number;
    year: number;
}