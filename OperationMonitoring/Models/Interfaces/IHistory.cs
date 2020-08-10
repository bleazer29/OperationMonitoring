using System;

namespace OperationMonitoring.Models.Interfaces
{
    public interface IHistory
    {
        public DateTime Date { get; set; }
        public string Commentary { get; set; }
        public string Message { get; set; }
        public Employee Author { get; set; } //кто коммент написал
    }
}
