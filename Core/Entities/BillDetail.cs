using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class BillDetail : BaseEntity
    {
        public BillDetail()
        {

        }
        public BillDetail(string itemName, int itemCategoryId, string itemCategoryName, decimal amount ,InmateBill inmateBill)
        {
            ItemName = itemName;
            ItemCategoryId = itemCategoryId;
            ItemCategoryName = itemCategoryName;
            Amount = amount;
            InmateBill = inmateBill;
        }

        public string ItemName { get; set; }
        public int ItemCategoryId { get; set; }
        public string ItemCategoryName { get; set; }
        public decimal Amount { get; set; }
        public int InmateBillId { get; set; }
        public InmateBill InmateBill { get; set; }
    }
}
