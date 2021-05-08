using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataFilters
{
    public class BillFilter : BaseFilter
    {
        public int? Month { get; set; }
        public int? Year { get; set; }
        public string PaidPredicate { get; set; }
    }
}
