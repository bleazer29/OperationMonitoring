using OperationMonitoring.Models.Interfaces;
using System;

namespace OperationMonitoring.Models
{
    public class EquipmentHistory: IHistory
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Commentary { get; set; }
        public virtual Employee Author { get; set; }       
        public virtual Equipment Equipment { get; set; }
        public virtual EquipmentStatus Status { get; set; }
        public string Message { get; set; }
    }
}
