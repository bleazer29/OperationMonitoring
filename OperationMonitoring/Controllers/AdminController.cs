using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using OperationMonitoring.Data;
using OperationMonitoring.Models;
using OperationMonitoring.ModelsIdentity;
using OperationMonitoring.ModelsIdentity.Security;

namespace OperationMonitoring.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IDataProtector protector;
        private readonly ApplicationContext db;
        private readonly IMemoryCache memoryCache;

        public AdminController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, ApplicationContext db,
            IDataProtectionProvider dataProtectionProvider, DataProtectionPurposeStrings dataProtectionPurposeStrings, IMemoryCache memoryCache)
        {
            this.db = db;
            this.roleManager = roleManager;
            this.userManager = userManager;
            protector = dataProtectionProvider.CreateProtector(dataProtectionPurposeStrings.EmployeeIdRouteValue);
            this.memoryCache = memoryCache;
        }  


        public IActionResult Index() {  return View();  }
       
        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            try
            {
                var user = await userManager.FindByIdAsync(id);
                if (user == null)
                {
                    ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                    return View("NotFound");
                }
                var userClaims = await userManager.GetClaimsAsync(user);
                var userRoles = await userManager.GetRolesAsync(user);
                var model = new EditUserViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    UserName = user.UserName,
                    Claims = userClaims.Select(c => c.Value).ToList(),
                    Roles = userRoles
                };
                return View(model);
            }
            catch  { return View(); }
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            try
            {
                var user = await userManager.FindByIdAsync(model.Id);
                if (user == null)
                {
                    ViewBag.ErrorMessage = $"User with Id = {model.Id} cannot be found";
                    return View("NotFound");
                }
                else
                {
                    user.Email = model.Email;
                    user.UserName = model.UserName;
                    var result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)  return RedirectToAction("AdminPanel");
                    
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }
            }
            catch  {  return View(model); }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                return View("NotFound");
            }
            else
            {
                try
                {
                    if (db.Employees.Any(x => x.IdentityUser.Id.Equals(id)))
                    {
                        db.Employees.Remove(db.Employees.FirstOrDefault(x => x.IdentityUser.Id.Equals(id)));
                        db.SaveChanges();
                    }
                    var result = await userManager.DeleteAsync(user);
                    if (result.Succeeded) return RedirectToAction(nameof(AdminPanel));
                    foreach (var error in result.Errors) { ModelState.AddModelError("", error.Description); }
                    return View(nameof(AdminPanel));
                }
                catch
                {
                    ViewBag.ErrorTitle = $"Почта привязана к роли!";
                    ViewBag.ErrorMessage = $"Она не может быть удалена, обратитесь к сисадмину за помощью!";
                    return View("Error");
                }
            }
        }

        [HttpGet]
        public IActionResult CreateRole() {return View(); }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IdentityRole identityRole = new IdentityRole {  Name = model.RoleName  };
                    IdentityResult result = await roleManager.CreateAsync(identityRole);
                    if (result.Succeeded) return RedirectToAction("AdminPanel", "Admin");
                    foreach (IdentityError error in result.Errors) { ModelState.AddModelError("", error.Description);}
                }
                return View(model);
            }
            catch { return View(model); }
            
        }


        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            try
            {
                var role = await roleManager.FindByIdAsync(id);
                if (role == null)
                {
                    ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                    return View("NotFound");
                }
                var model = new EditRoleViewModel
                {
                    Id = role.Id,
                    RoleName = role.Name
                };
                foreach (var user in userManager.Users) 
                {
                    if (await userManager.IsInRoleAsync(user, role.Name)) model.Users.Add(user.UserName);
                }
                return View(model);
            }
            catch { return View(); }
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            try
            {
                var role = await roleManager.FindByIdAsync(model.Id);
                if (role == null)
                {
                    ViewBag.ErrorMessage = $"Role with Id = {model.Id} cannot be found";
                    return View("NotFound");
                }
                else
                {
                    role.Name = model.RoleName;
                    var result = await roleManager.UpdateAsync(role);
                    if (result.Succeeded) return RedirectToAction("AdminPanel");
                    foreach (var error in result.Errors)  {  ModelState.AddModelError("", error.Description); }
                    return View(model);
                }
            }
            catch { return View(model); }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("NotFound");
            }
            else
            {
                try
                {
                    var result = await roleManager.DeleteAsync(role);
                    if (result.Succeeded) return RedirectToAction("AdminPanel");
                    foreach (var error in result.Errors) { ModelState.AddModelError("", error.Description); }
                    return View("AdminPanel");
                }
                catch
                {
                    ViewBag.ErrorTitle = $"{role.Name} role is in use";
                    ViewBag.ErrorMessage = $"{role.Name} role cannot be deleted as there are users in this role. If you want to delete this role, please remove the users from the role and then try to delete";
                    return View("Error");
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            try
            {
                ViewBag.roleId = roleId;
                var role = await roleManager.FindByIdAsync(roleId);
                if (role == null)
                {
                    ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                    return View("NotFound");
                }

                var model = new List<UserRoleViewModel>();
                foreach (var user in userManager.Users)
                {
                    var userRoleViewModel = new UserRoleViewModel
                    {
                        UserId = user.Id,
                        UserName = user.UserName
                    };

                    if (await userManager.IsInRoleAsync(user, role.Name)) userRoleViewModel.IsSelected = true;
                    else userRoleViewModel.IsSelected = false;
                    model.Add(userRoleViewModel);
                }
                return View(model);
            }
            catch  {  return View();  }
        }

        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string roleId)
        {
            try
            {
                var role = await roleManager.FindByIdAsync(roleId);
                if (role == null)
                {
                    ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                    return View("NotFound");
                }

                for (int i = 0; i < model.Count; i++)
                {
                    var user = await userManager.FindByIdAsync(model[i].UserId);
                    IdentityResult result;
                    if (model[i].IsSelected && !await userManager.IsInRoleAsync(user, role.Name))
                    {
                        result = await userManager.AddToRoleAsync(user, role.Name);
                    }
                    else if (!model[i].IsSelected && await userManager.IsInRoleAsync(user, role.Name))
                    {
                        result = await userManager.RemoveFromRoleAsync(user, role.Name);
                    }
                    else continue;

                    if (result.Succeeded)
                    {
                        if (i < (model.Count - 1)) continue;
                        else return RedirectToAction("EditRole", new { Id = roleId });
                    }
                }
                return RedirectToAction("EditRole", new { Id = roleId });
            }
            catch  { return View("NotFound"); }
        }

        [HttpGet]
        public async Task<IActionResult> AdminPanel()
        {
            //var stopWatch = new Stopwatch();
            //stopWatch.Start();
            List<IdentityRole> rolesIdentity;
            List<IdentityUser> usersIdentity;
            List<Employee> employees;
            if (!memoryCache.TryGetValue("rolesIdentity", out rolesIdentity))
            {
                memoryCache.Set("rolesIdentity", await roleManager.Roles.AsNoTracking().ToListAsync(), new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(12)));
            }
            if (!memoryCache.TryGetValue("usersIdentity", out usersIdentity))
            {
                memoryCache.Set("usersIdentity", await userManager.Users.AsNoTracking().ToListAsync(), new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(12)));
            }
            if (!memoryCache.TryGetValue("Employees", out employees))
            {
                var listEmployees = await db.Employees.AsNoTracking().ToListAsync();
                listEmployees.Select(e =>
                {
                    e.EncryptedId = protector.Protect(e.Id.ToString());
                    return e;
                });
                memoryCache.Set("Employees", listEmployees, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(12)));
            }

            rolesIdentity = memoryCache.Get("rolesIdentity") as List<IdentityRole>;
            usersIdentity = memoryCache.Get("usersIdentity") as List<IdentityUser>;
            employees = memoryCache.Get("Employees") as List<Employee>;

            ViewBag.IdentityRoles = rolesIdentity as IEnumerable<IdentityRole>;
            ViewBag.IdentityUsers = usersIdentity as IEnumerable<IdentityUser>;
            ViewBag.Employee = employees as IEnumerable<Employee>;

            //stopWatch.Stop();
            //ViewBag.TotalTime = stopWatch.Elapsed;
            return View();
        }



    }
}