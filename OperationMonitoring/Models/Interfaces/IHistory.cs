using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OperationMonitoring.Models.Interfaces
{
    interface IHistory
    {
        public DateTime Date { get; set; }
        public string Commentary { get; set; }
        public Employee Author { get; set; } //кто коммент написал
    }
}
