using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataFilters
{
    public class LeavesFilter : BaseFilter
    {
        public int? LeaveId { get; set; }
        public int? InmateId { get; set; }
    }
}
