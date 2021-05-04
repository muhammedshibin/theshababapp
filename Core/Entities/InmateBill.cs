﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class InmateBill : BaseEntity
    {
        [Required]
        public int Month { get; set; }
        [Required]
        public int Year { get; set; }
        public double BillAmount { get; set; }
        public Inmate Inmate { get; set; }
        public int InmateId { get; set; }
        public List<BillDetail> BillItems { get; set; }
    }
}
