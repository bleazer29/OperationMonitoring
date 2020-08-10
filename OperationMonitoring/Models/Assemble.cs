using Microsoft.AspNetCore.Identity;
using OperationMonitoring.Helpers.ValidationAttributes;
using System;
using System.Collections.Generic;

namespace OperationMonitoring.Models
{
    [SingleRequired("Equipment", "Part", ErrorMessage = "Only Equipment or Part must be specified")]
    public class Assemble
    {
        public int Id { get; set; }
        public virtual Equipment Equipment { get; set; }
        public virtual Part Part { get; set; }
        public ICollection<Part> PartsList { get; set; }
        public DateTime Date { get; set; }
        public bool IsUsed { get; set; }    
    }
}
