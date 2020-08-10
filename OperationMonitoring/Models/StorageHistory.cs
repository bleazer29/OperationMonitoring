using OperationMonitoring.Models.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace OperationMonitoring.Models
{
    public class StorageHistory : IHistory
    {
        public int Id { get; set; }
        [Required]
        public virtual Stock Stock { get; set; }
        public double? Amount { get; set; }
        [Required]
        public virtual Storage StorageTo { get; set; }
        [Required]
        public virtual HistoryType HistoryType { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public string Commentary { get; set; }
        public virtual Employee Author { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
