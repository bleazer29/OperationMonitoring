using Microsoft.AspNetCore.Identity;
using OperationMonitoring.ModelsIdentity;

namespace OperationMonitoring.Models
{
    public class VisiblePageRole
    {
        public int Id { get; set; }
        public IdentityRole IdentityRole { get; set; }
        public RolePages RolePages { get; set; }
        public bool IsSelected { get; set; }
    }
}
