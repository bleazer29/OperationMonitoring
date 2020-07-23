﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OperationMonitoring.Models.HistoryModels
{
    [Table("HistoryTypes")]
    public class HistoryType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
