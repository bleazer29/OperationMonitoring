using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OperationMonitoring.Models.HistoryModels
{
    [Table("EquipmentHistory")]
    public class EquipmentHistory
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int EquipmentId { get; set; }
        public int HistoryTypeId { get; set; }
        public virtual HistoryType HistoryType { get; set; }
        /// <summary>
        /// Entity - Maintenance id or Order id, depends on history type
        /// </summary>
        public int? EntityId { get; set; } 
        public string Commentary { get; set; }
        public int? UserId { get; set; }
        public int EquipmentOperatingTime { get; set; }
    }
}
