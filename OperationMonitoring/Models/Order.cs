using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OperationMonitoring.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public virtual Agreement Agreement { get; set; }
        [Required]
        public virtual Well Well { get; set; }
        [Required]
        public virtual Equipment Equipment { get; set; }
        public virtual Employee Responsible { get; set; }
        public string DeliveryLocation { get; set; }
        [Required]
        public DateTime DateStart { get; set; }
        public DateTime EstimateDate { get; set; }
        public DateTime DateFinish { get; set; }
        public bool IsOpen { get; set; }
    }
}
