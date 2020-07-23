using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OperationMonitoring.Models
{
   
    public class Purchase
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Amount { get; set; }
        [ForeignKey("Nomenclature")]
        public int NomenclatureId { get; set; }
        public virtual Nomenclature Nomenclature { get; set; }
    }
}
