using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OperationMonitoring.Models
{
    [Table("PartsHistory")]
    public class PartHistory
    {
        public int Id { get; set; }
        public int? EquipmentId { get; set; }
        public int PartId { get; set; }
        public DateTime Date { get; set; }
        public string Commentary { get; set; }
        public int? UserId { get; set; }
        public int PartOperatingTime { get; set; }
    }
}
