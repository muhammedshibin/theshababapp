using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class TransactionDetailSpecification : BaseSpecification<TransactionDetail>
    {
        public TransactionDetailSpecification(int month,int year,bool isAutoGenerated) :
            base(x => x.TransactionDate.Year == year && x.TransactionDate.Month == month && x.IsAutoGenerated == isAutoGenerated)
        {

        }
    }
}
