﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OperationMonitoring.Models
{
    public class Maintenance
    {
        public int Id { get; set; }
        public virtual Part Part { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public string WorkOrder { get { return "WO-" + Id + "/" + StartDate.Year; } }
        public bool IsOpened { get; set; }
    }
}
