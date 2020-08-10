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
        [Required]
        public virtual Part Part { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public EquipmentStatus Status { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
