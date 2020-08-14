﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OperationMonitoring.Models
{
    public class EditRoleViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Role Name is required")]
        public string RoleName { get; set; }
        public List<string> Users { get; set; }
        public List<string> Pages { get; set; }
        public EditRoleViewModel()  {  Users = new List<string>(); Pages = new List<string>(); }
    }



}
