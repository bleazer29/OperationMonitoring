using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OperationMonitoring.Models
{
    public class Maintenance
    {
        public int Id { get; set; }
        public virtual Equipment Equipment { get; set; }
        public virtual Employee Responsible { get; set; }
        public virtual Counterparty Counterparty { get; set; }
        /// <summary>
        /// внешний или внутренний ремонт (потом может добавятся ещё типы)
        /// </summary>
        public virtual MaintenanceType MaintenanceType { get; set; }
        /// <summary>
        /// плановый или аварийный (потом может добавятся ещё категории)
        /// </summary>
        public virtual MaintenanceCategory MaintenanceCategory { get; set; }
        public virtual Storage ReturnStorage { get; set; }
        public string MaintenanceReason { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EstimateDate { get; set; }
        public DateTime FinishDate { get; set; }
        public string Title { get { return "WO-" + Id + "/" + StartDate.Year; } }
        public bool IsOpened { get; set; }
    }
}
