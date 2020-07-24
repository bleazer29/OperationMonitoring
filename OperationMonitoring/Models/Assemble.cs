using Microsoft.AspNetCore.Identity;
using OperationMonitoring.Models.Interfaces;
using System;
using System.Collections.Generic;
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
        public DateTime Date { get; set; }
        public bool IsUsed { get; set; }
    }
}
