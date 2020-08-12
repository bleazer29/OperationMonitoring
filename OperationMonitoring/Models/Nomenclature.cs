using Microsoft.AspNetCore.Cors;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OperationMonitoring.Models
{
    public class Nomenclature
    {
        public int Id { get; set; }
        [Required]
        public string VendorCode { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public virtual Provider Provider { get; set; }
        public virtual Specification Specification { get; set; }
    }
}
