using System.ComponentModel.DataAnnotations;

namespace OperationMonitoring.Models
{
    public class CreateRoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}
