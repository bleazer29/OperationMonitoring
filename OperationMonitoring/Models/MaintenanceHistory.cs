using OperationMonitoring.Models.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace OperationMonitoring.Models
{
    public class MaintenanceHistory: IHistory
    {
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public string Commentary { get; set; }
        public virtual Employee Author { get ; set; }
        [Required]
        public virtual Maintenance Maintenance { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
