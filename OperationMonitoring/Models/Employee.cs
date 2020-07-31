﻿using Microsoft.AspNetCore.Identity;
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

        public string FirstName { get; set; }
        public string LastName { get; set; }    
        public string Patronymic { get; set; }
        public string FullName { get { return LastName + " " + FirstName + " " + Patronymic; } }

        [DataType(DataType.Date),Required]
        public DateTime BirthDate { get; set; }
        public virtual Position Position { get; set; }
        public IdentityUser IdentityUser { get; set; }
    }
}
