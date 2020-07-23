using Microsoft.AspNetCore.Identity;
using OperationMonitoring.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OperationMonitoring.Models
{
    public class OrderHistory : IHistory
    { 
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public virtual Doc Doc { get; set; }
        public string Commentary { get; set; }
        public virtual IdentityUser User { get; set; }    
        public virtual Equipment Equipment { get; set; }
        public virtual Counterparty Counterparty { get; set; }
        public virtual Agreement Agreement { get; set; }
        public virtual Well Well { get; set; }
        public string OperationInfo { get; set; } // client name, contract, well location...       
        public DateTime DateFinish { get; set; }
    }
}
