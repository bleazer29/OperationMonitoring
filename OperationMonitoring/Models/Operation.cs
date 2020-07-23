using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OperationMonitoring.Models
{
    public class Operation
    {
        public int Id { get; set; }
        public virtual Equipment Equipment { get; set; }
        public int? ClientId { get; set; }
        public int? ContractId { get; set; }
        public int? WellId { get; set; }
        public string OperationInfo { get; set; } // client name, contract, well location... 
        public DateTime DateStart { get; set; }
        public DateTime DateFinish { get; set; }
       

    }
}
