using Microsoft.AspNetCore.Identity;
using OperationMonitoring.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OperationMonitoring.Models
{
    public class StorageHistory : IHistory
    {
        public int Id { get; set; }
        public virtual Stock Stock { get; set; }
        public double? Amount { get; set; }
        public virtual Storage StorageTo { get; set; }
        public virtual HistoryType HistoryType { get; set; }
        public DateTime Date { get; set; }
        public string Commentary { get; set; }
        public virtual Employee Author { get; set; }
        public string Message { get; set; }
    }
}
