using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OperationMonitoring.Models
{
    public class Maintenance
    {
        public int Id { get; set; }
        public virtual Equipment Equipment { get; set; }
        public virtual Employee Responsible { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EstimateDate { get; set; }
        public DateTime FinishDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public virtual Counterparty Counterparty { get; set; }
        public virtual MaintenanceType MaintenanceType { get; set; }
        public virtual Storage ReturnStorage { get; set; }
        public string Title { get { return "WO-" + Id + "/" + StartDate.Year; } }
        public bool IsOpened { get; set; }
    }
}
