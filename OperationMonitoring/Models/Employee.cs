using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OperationMonitoring.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }    
        public string Patronymic { get { return LastName + " " + FirstName + " " + Patronymic; } }
        public DateTime BirthDate { get; set; }
        public virtual Position Position { get; set; }
        public string Email { get; set; }
    }
}
