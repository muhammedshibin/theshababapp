using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
    public class BillSpecification : BaseSpecification<InmateBill>
    {
        public BillSpecification(int month,int year) : 
            base(bill => bill.Month == month && bill.Year == year)
        {
        }
        
        public BillSpecification(int billId):
            base(b => b.Id == billId)
        {
            AddInclude(b => b.BillItems);
        }
    }
}