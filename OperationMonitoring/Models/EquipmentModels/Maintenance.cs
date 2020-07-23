using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OperationMonitoring.Models.EquipmentModels
{
    public class Maintenance
    {
        public int Id { get; set; }
        public virtual Equipment Equipment { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateDue { get; set; }
        public DateTime DateFinish { get; set; }
        public bool IsOpen { get; set; }
        //public string Title {
        //    get { return Title; }
        //    set { Title = $"WO#{Id}-{DateStart.Year}"; } 
        //}

    }
}
