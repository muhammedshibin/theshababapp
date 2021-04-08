using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class BillDetail : BaseEntity
    {
        public BillDetail(string itemName, int itemCategoryId, string itemCategoryName, double amount InmateBill inmateBill)
        {
            ItemName = itemName;
            ItemCategoryId = itemCategoryId;
            ItemCategoryName = itemCategoryName;
            Amount = amount;
            this.inmateBill = inmateBill;
        }

        public string ItemName { get; set; }
        public int ItemCategoryId { get; set; }
        public string ItemCategoryName { get; set; }
        public double Amount { get; set; }
        public int InmateBillId { get; set; }
        public InmateBill inmateBill { get; set; }
    }
}
