﻿using Microsoft.AspNetCore.Identity;
using OperationMonitoring.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OperationMonitoring.Models
{
    public class StorageHistory : IHistory
    {
        public int Id { get; set; }
        public virtual Part Part { get; set; }
        public virtual Storage StorageFrom { get; set; }
        public virtual Storage StorageTo { get; set; }
        public virtual HistoryType HistoryType { get; set; }
        public DateTime Date { get; set; }
        public string Commentary { get; set; }
        public virtual Employee Author { get; set; }
    }
}
