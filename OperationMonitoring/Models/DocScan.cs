using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OperationMonitoring.Models
{
    public class DocScan
    {
        public int Id { get; set; }
        public string base64Image { get; set; }
        public string FilePath { get; set; }
    }
}
