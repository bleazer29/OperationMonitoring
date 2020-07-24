using Microsoft.AspNetCore.Identity;
using OperationMonitoring.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OperationMonitoring.Models
{
    public class EquipmentHistory: IHistory
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public virtual Doc Doc { get; set; }
        public string Commentary { get; set; }
        public virtual Employee User { get; set; }       
        public virtual Equipment Equipment { get; set; }
        public int EquipmentOperatingTime { get; set; }
        public virtual EquipmentStatus StatusFrom { get; set; }
        public virtual EquipmentStatus StatusTo { get; set; }
    }
}
