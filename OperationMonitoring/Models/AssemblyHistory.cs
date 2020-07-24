using Microsoft.AspNetCore.Identity;
using OperationMonitoring.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OperationMonitoring.Models
{
    public class AssemblyHistory: IHistory
    {
        public int Id { get; set; }
        public virtual Equipment Equipment { get; set; }            
        public ICollection<PartHistory> HistoryParts { get; set; }
        public DateTime Date { get; set; }
        public virtual Doc Doc { get; set; }
        public string Commentary { get; set; }
        public virtual Employee User { get; set; }
    }
}
