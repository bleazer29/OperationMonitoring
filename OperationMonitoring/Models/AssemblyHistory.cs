using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OperationMonitoring.Models
{
    public class AssemblyHistory
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public virtual Equipment Equipment { get; set; }     
        public Doc Doc { get; set; } //или скан
        public ICollection<HistoryPart> HistoryParts { get; set; }
    }
}
