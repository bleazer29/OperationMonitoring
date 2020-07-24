﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OperationMonitoring.Models
{
    public class Well
    {
        public int Id { get; set; }
        public virtual Counterparty Counterparty { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
    }
}
