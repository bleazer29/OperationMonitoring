using Microsoft.AspNetCore.Identity;
using OperationMonitoring.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OperationMonitoring.Models
{
    public class OrderHistory : IHistory
    { 
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public string Commentary { get; set; }
        public virtual Employee Author { get; set; }
        [Required]
        public virtual Order Order { get; set; }
        public int? OperatingTime { get; set; } // minutes
        [Required]
        public string Message { get; set; }
    }
}
