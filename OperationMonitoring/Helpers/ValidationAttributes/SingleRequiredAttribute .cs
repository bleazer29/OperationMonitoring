using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace OperationMonitoring.Helpers.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class SingleRequiredAttribute : ValidationAttribute
    {
        private string[] PropertyList { get; set; }

        public SingleRequiredAttribute(params string[] propertyList)
        {
            this.PropertyList = propertyList;
        }

        //See http://stackoverflow.com/a/1365669
        public override object TypeId
        {
            get
            {
                return this;
            }
        }

        public override bool IsValid(object value)
        {
            PropertyInfo propertyInfo;
            bool isOneNotNull = false;
            foreach (string propertyName in PropertyList)
            {
                propertyInfo = value.GetType().GetProperty(propertyName);

                if (propertyInfo != null && propertyInfo.GetValue(value, null) != null)
                {
                    if (isOneNotNull == true)
                    {
                        Console.WriteLine("hui tebe");
                        return false;
                    }
                    isOneNotNull = true;
                }
            }
            if (isOneNotNull == true)
            {
                return true;
            }
            else
            {
                Console.WriteLine("hui tebe");
                return false;
            }
        }
    }
}
