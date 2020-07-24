using Microsoft.AspNetCore.Identity;
using OperationMonitoring.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OperationMonitoring.Models
{
    public class MaintenanceHistory: IHistory
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } // start
        public DateTime DateFinish { get; set; }
        public virtual Doc Doc { get; set; }
        public string Commentary { get; set; }
        public virtual Employee User { get ; set; } 
        public virtual Part Part { get; set; }
        public virtual MaintenanceType MaintenanceType { get; set; }
        public string WorkOrder { get { return "WO-" + Id + "/" + Date.Year; } }
        public bool IsOpened { get; set; }
    }
}
