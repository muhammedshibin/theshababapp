using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class BillDto
    {
        public int Id { get; set; }
        public string Month { get; set; }
        public int Year { get; set; }
        public int BillAmount { get; set; }
        public string Inmate { get; set; }
        public int InmateId { get; set; }
        public int PaymentStatus { get; set; }
        public List<BillDetailDto> BillItems { get; set; }
    }

    public class BillDetailDto
    {
        public string ItemName { get; set; }
        public string ItemCategoryName { get; set; }
        public double Amount { get; set; }
    }
}
