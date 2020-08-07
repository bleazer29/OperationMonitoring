using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OperationMonitoring.Models
{
    public class Preset
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual ICollection<PresetItem> PresetItems { get; set; }
    }
}
