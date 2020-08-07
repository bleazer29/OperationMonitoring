using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OperationMonitoring.Models
{
    public class MaintenanceType
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } //Внешний ремонт, списано, починено (на склад)
    }
}
