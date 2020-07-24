using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OperationMonitoring.Models
{
    public class Equipment
    {
        public int Id { get; set; }
        public virtual EquipmentStatus Status { get; set; }
        public virtual Department Department { get; set; }
        public virtual Category Category { get; set; }
        public virtual Types Type { get; set; }
        public string Title { get; set; }
        public string SerialNum { get; set; }
        public int DiameterOuter { get; set; }
        public int DiameterInner { get; set; }
        public int Length { get; set; }
        public int OperatingTime { get; set; }
        public int WarningTime { get; set; }
    }
}
