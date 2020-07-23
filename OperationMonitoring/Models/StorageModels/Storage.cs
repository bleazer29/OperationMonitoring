using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OperationMonitoring.Models.StorageModels
{
    public class Storage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("Storage")]
        public int? ParentId { get; set; }
        public virtual Storage Parent { get; set; }
    }
}
