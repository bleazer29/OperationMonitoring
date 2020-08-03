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
        public virtual Counterparty Counterparty { get; set; }
        public string AgreementNumber { get; set; }
        public virtual Doc Doc { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateDue { get; set; }
        public bool IsOpen { get; set; }
    }
}