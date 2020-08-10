using Newtonsoft.Json;
using OperationMonitoring.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OperationMonitoring.Helpers
{
    public static class JSConverter
    {
        public static string SerializeObject(object value)
        {           
            return JsonConvert.SerializeObject(value);
        }

    }
}
