using System;
using System.ComponentModel.DataAnnotations;

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
