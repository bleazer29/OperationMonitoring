using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OperationMonitoring.Models
{   
    public class Provider
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        [Required]
        public string EDRPOU { get; set; }
    }
}
