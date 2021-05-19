namespace Core.DataFilters
{
    public class TransactionsFilter : BaseFilter
    {
        public TransactionsFilter()
        {
            
        }
        public TransactionsFilter(int month,int year,bool isPagingNeeded)
        {
            Month = month;
            Year = year;
            IsPagingNeeded = isPagingNeeded;
        }
              
        public int? Month { get; set; }      
        public int? Year { get; set; }
        private string _categoryName;
        public int? PaidBy { get; set; }
        public int? PaidTo { get; set; }
        public string CategoryName
        {
            get => _categoryName; 
            set => _categoryName = value.ToLower(); 
        }
       
        public bool IsPagingNeeded = true;
       
    }
}