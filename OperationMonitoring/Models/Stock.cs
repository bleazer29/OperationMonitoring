using System.ComponentModel.DataAnnotations;

namespace OperationMonitoring.Models
{
    public class Stock
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        //[StockItemRequired]
        public virtual Equipment Equipment { get; set; }
        public virtual Part Part { get; set; }
        public virtual Nomenclature Nomenclature { get; set; }
        [Required]
        public virtual Storage Storage { get; set; }        
    }
}
