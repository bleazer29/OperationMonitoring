using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OperationMonitoring.Models
{
    public class Order
    {
        public int Id { get; set; }
        public virtual Agreement Agreement { get; set; }
        public virtual Well Well { get; set; }
        public virtual Equipment Equipment { get; set; }
        public virtual Employee Responsible { get; set; }
        public string DeliveryLocation { get; set; }    
        public DateTime DateStart { get; set; }
        public DateTime EstimateDate { get; set; }
        public DateTime DateFinish { get; set; }
    }
}
