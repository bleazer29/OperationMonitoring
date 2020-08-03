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
        public string InventoryNum { get; set; }
        public int OperatingTime { get; set; }
        public int WarningTime { get; set; }
        public int Amount { get; set; }
        public virtual Equipment Equipment { get; set; }
        public virtual EquipmentStatus Status { get; set; }
        public bool IsUsed { get; set; }
        public virtual Part Parent { get; set; }
        public virtual Nomenclature Nomenclature{ get; set; }
    }
}
