using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OperationMonitoring.Models
{
    public class MaintenanceType
    {
        public int Id { get; set; }
        public string Title { get; set; } //Внешний ремонт, списано, починено (на склад)
    }
}
