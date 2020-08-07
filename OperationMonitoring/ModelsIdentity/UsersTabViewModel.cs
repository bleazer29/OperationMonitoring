using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OperationMonitoring.ModelsIdentity
{
    public class UsersTabViewModel
    {
        public Tab ActiveTab { get; set; }
    }
    public enum Tab
    {
        ListRoles,
        ListUsers,
        ListEmployee
    }
}
