using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OperationMonitoring.Models
{
    public class Part
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string SerialNum { get; set; }
        public int OperationTime { get; set; }
        public int WarningTime { get; set; }
        public virtual Equipment Equipment { get; set; }
        public virtual EquipmentStatus Status { get; set; }
        public bool IsUsed { get; set; }
        public virtual Part Parent { get; set; }
        public virtual Nomenclature Nomenclature{get;set;}
    }
}
