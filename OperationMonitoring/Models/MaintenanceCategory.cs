﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OperationMonitoring.Models
{
    public class MaintenanceCategory
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
    }
}
