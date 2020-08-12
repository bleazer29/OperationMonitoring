using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OperationMonitoring.Models
{
    public class Doc
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Path must be specified")]
        public string FilePath { get; set; }
        public DocType Type { get; set; }
        public int RelatedEntityId { get; set; }
    }
}
