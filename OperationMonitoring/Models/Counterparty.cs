using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OperationMonitoring.Models
{
    public class Counterparty
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [Required(ErrorMessage = "EDRPOU must be specified")]
        public string EDRPOU { get; set; }
        public string Description { get; set; }

    }
}
