using System.ComponentModel.DataAnnotations;
namespace OperationMonitoring.ModelsIdentity
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
