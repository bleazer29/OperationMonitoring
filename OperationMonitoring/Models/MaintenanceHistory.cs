using Microsoft.AspNetCore.Identity;
using OperationMonitoring.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OperationMonitoring.Models
{
    public class MaintenanceHistory: IHistory
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Commentary { get; set; }
        public virtual Employee Author { get ; set; }
        public virtual Maintenance Maintenance { get; set; }
        public string Message { get; set; }
    }
}
