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
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateStart { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateDue { get; set; }
        public bool IsOpen { get; set; }
    }
}