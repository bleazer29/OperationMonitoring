using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OperationMonitoring.Models
{
    public class Maintenance
    {
        public int Id { get; set; }
        public virtual Equipment Equipment { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateDue { get; set; }
        public DateTime DateFinish { get; set; }
    }
}
