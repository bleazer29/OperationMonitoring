using Microsoft.AspNetCore.Identity;
using OperationMonitoring.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OperationMonitoring.Models
{
    public class Assemble
    {
        public int Id { get; set; }
        public virtual Equipment Equipment { get; set; }
        public virtual Part Part { get; set; }
        /// <summary>
        /// Из чего состоит
        /// </summary>
        public ICollection<Part> PartsList { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public bool IsUsed { get; set; }
    }
}
