using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OperationMonitoring.Models
{
    public class PresetItem
    {
        public int Id { get; set; }
        [Required]
        public virtual Preset Preset { get; set; }
        [Required]
        public virtual Nomenclature Nomenclature { get; set; }
        [Required]
        public double Amount { get; set; }
    }
}
