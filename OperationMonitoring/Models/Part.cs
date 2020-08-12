using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OperationMonitoring.Models
{
    public class Part
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string InventoryNum { get; set; }
        [Required]
        public int OperatingTime { get; set; }
        [Required]
        public int WarningTime { get; set; }
        [Required]
        public int Amount { get; set; }
        public virtual Equipment Equipment { get; set; }
        [Required]
        public virtual EquipmentStatus Status { get; set; }
        public bool IsUsed { get; set; }
        public virtual Part Parent { get; set; }
        [Required]
        public virtual Nomenclature Nomenclature{ get; set; }
    }
}
