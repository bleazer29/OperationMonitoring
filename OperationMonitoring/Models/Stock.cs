using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OperationMonitoring.Models
{
    public class Stock
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public int NomenclatureId { get; set; }
        public virtual Nomenclature Nomenclature { get; set; }
        public int StorageId { get; set; }
        public virtual Storage Storage { get; set; }        
    }
}
