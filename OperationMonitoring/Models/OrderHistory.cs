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
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public string Commentary { get; set; }
        public virtual Employee Author { get; set; }
        public virtual Order Order { get; set; }
        public int? OperatingTime { get; set; } // minutes?
    }
}
