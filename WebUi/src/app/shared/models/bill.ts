export interface MonthlyBill {
    month: number
    year: number
    subTotal: number
    categoryWiseExpenses: CategoryWiseExpense[]
  }
  
  export interface CategoryWiseExpense {
    categoryId: number
    categoryName: string
    transactionDetails: TransactionDetail[]
    totalAmount: number
  }
  
  export interface TransactionDetail {
    transactionDetailName: string
    amount: number
  }