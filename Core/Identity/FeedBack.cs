﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Identity
{
    public class FeedBack
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public string WrittenBy { get; set; }
    }
}
