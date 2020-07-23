using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OperationMonitoring.Models
{
    public class Nomenclature
    {
        public int Id { get; set; }
        public string Name { get; set; }    
        public virtual Provider Provider { get; set; }
        public virtual Specification Specification { get; set; }
    }
}
