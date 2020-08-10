using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OperationMonitoring.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [NotMapped]
        public string EncryptedId { get; set; }
        [Required(ErrorMessage = "FirstName must be specified")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "LastName must be specified")]
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string FullName { get { return LastName + " " + FirstName + " " + Patronymic; } }
        [DataType(DataType.Date), Required]
        public DateTime BirthDate { get; set; }
        [Required(ErrorMessage = "Position must be specified")]
        public virtual Position Position { get; set; }
        public virtual IdentityUser IdentityUser { get; set; }
    }
}
