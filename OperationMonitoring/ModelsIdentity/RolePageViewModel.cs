using Microsoft.AspNetCore.Identity;

namespace OperationMonitoring.ModelsIdentity
{
    public class RolePageViewModel
    {
        public int Id { get; set; }
        public IdentityRole IdentityRole { get; set; }
        public RolePages RolePages { get; set; }
        public bool IsSelected { get; set; }
    }
}
