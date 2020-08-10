using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OperationMonitoring.Models
{
    public class DocType
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title must be specified")]
        public string Title { get; set; }
    }
}
