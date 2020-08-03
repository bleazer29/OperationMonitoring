using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OperationMonitoring.Models
{
    public class PartHistory
    {
        public int Id { get; set; }
        public virtual Part Part { get; set; }
        public DateTime Date { get; set; }
        public EquipmentStatus Status { get; set; }
        public string Message { get; set; }
    }
}
