using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OperationMonitoring.Models
{
    public class HistoryPart
    {
        public int Id { get; set; }
        public virtual Part Part { get; set; }
        public virtual AssemblyHistory AssemblyHistory { get; set; }
    }
}
