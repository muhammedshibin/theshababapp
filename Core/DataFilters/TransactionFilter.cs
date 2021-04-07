namespace Core.DataFilters
{
    public class TransactionsFilter
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
        private const int MaxPageSize = 50;
        private int _pageSize = 6;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
        public int? Month { get; set; }
        public int PageIndex { get; set; } = 1;
        public int? Year { get; set; }
        public string Sort { get; set; }
        private string _categoryName;
        public string CategoryName
        {
            get => _categoryName; 
            set => _categoryName = value.ToLower(); 
        }
        private string _search;
        public bool IsPagingNeeded = true;
        public string Search
        {
            get => _search; 
            set => _search = value.ToLower(); 
        }
    }
}