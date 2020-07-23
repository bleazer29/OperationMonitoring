using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OperationMonitoring.Models
{
   
    public class Nomenclature
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Specifications { get; set; } 
        public int Amount { get; set; }
        [ForeignKey("Provider")]
        public int ProviderId { get; set; }
        public virtual Provider Provider { get; set; }
    }
}
