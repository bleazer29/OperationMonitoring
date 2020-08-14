using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace OperationMonitoring.Helpers
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            string userName = "Admin";
            string password = "Admin12345";
            if (await roleManager.FindByNameAsync("Admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if (await userManager.FindByNameAsync(userName) == null)
            {
                IdentityUser admin = new IdentityUser { UserName = userName, EmailConfirmed=true};
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }
        }
    }
}