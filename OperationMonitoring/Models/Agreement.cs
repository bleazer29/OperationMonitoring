using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace OperationMonitoring.Models
{
    public class Agreement
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Counterparty not specified")]
        public virtual Counterparty Counterparty { get; set; }
        [Required(ErrorMessage = "Agreement number not specified")]
        public string AgreementNumber { get; set; }
        public virtual Doc Doc { get; set; }
        [Required(ErrorMessage = "Start date not specified")]
        public DateTime DateStart { get; set; }
        public DateTime DateDue { get; set; }
        public bool IsOpen { get; set; }
    }
}