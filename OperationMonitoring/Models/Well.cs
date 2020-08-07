using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OperationMonitoring.Models
{
    public class Well
    {
        public int Id { get; set; }
        [Required]
        public virtual Counterparty Counterparty { get; set; }
        [Required]
        public string Title { get; set; }
        public string Location { get; set; }
    }
}
