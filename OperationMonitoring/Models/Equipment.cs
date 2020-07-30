using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OperationMonitoring.Models
{
    public class Equipment
    {
        public int Id { get; set; }
        public virtual EquipmentStatus Status { get; set; }

        [Display(Name = "Department")]
        public virtual Department Department { get; set; }

        [Display(Name = "Category")]
        public virtual EquipmentCategory Category { get; set; }

        [Display(Name = "Type")]
        public virtual EquipmentType Type { get; set; }

        [Display(Name = "Title")]
        [Required]
        public string Title { get; set; }

        [Display(Name = "Serial Number")]
        [Required]
        public string SerialNum { get; set; }

        [Display(Name = "Inventory Number")]
        [Required]
        public string InventoryNum { get; set; }

        [Display(Name = "Diameter Outer")]
        [Required]
        public int DiameterOuter { get; set; }

        [Display(Name = "Diameter Inner")]
        [Required]
        public int DiameterInner { get; set; }

        [Display(Name = "Length")]
        [Required]
        public int Length { get; set; }

        [Display(Name = "Operating Time (hours)")]
        public int OperatingTime { get; set; }

        [Display(Name = "Alert for Operating Time (hours)")]
        public int WarningTime { get; set; }
    }
}
