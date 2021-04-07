using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class BillDetail : BaseEntity
    {
        public string ItemName { get; set; }
        public int ItemCategoryId { get; set; }
        public string ItemCategoryName { get; set; }
        public double Amount { get; set; }
        public int InmateBillId { get; set; }
        public InmateBill inmateBill { get; set; }
    }
}
