using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OperationMonitoring.Models.StorageModels
{
    public class Nomenclature
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Specifications { get; set; }      
        public int Amount { get; set; }
        public int ProviderId { get; set; }
        public virtual Provider Provider { get; set; }
    }
}
