using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OperationMonitoring.Models
{
    public class Specification
    {
        public int Id { get; set; }
        public int OperatingTime { get; set; }
        public double Weight { get; set; }
        public string Material { get; set; }
        public double Height { get; set; }
        public virtual UsageType UsageType { get; set; }
    }
}
