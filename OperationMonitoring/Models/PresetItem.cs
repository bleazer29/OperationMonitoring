using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OperationMonitoring.Models
{
    public class PresetItem
    {
        public int Id { get; set; }
        public virtual Preset Preset { get; set; }
        public virtual Nomenclature Nomenclature { get; set; }
        public double Amount { get; set; }
    }
}
