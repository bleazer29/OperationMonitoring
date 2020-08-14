using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        public AdminController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, ApplicationContext db,
            IDataProtectionProvider dataProtectionProvider, DataProtectionPurposeStrings dataProtectionPurposeStrings)
        {
            this.db = db;
            this.roleManager = roleManager;
            this.userManager = userManager;
            protector = dataProtectionProvider.CreateProtector(dataProtectionPurposeStrings.EmployeeIdRouteValue);
            
        }

        public IActionResult Index() { return View(); }

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
            catch { return View(); }
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
                    if (result.Succeeded) return RedirectToAction("AdminPanel");

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }
            }
            catch { return View(model); }
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
                    var userId = userManager.GetUserId(HttpContext.User);
                    if (db.Employees.Any(x => x.IdentityUser.Id.Equals(id)))
                    {
                        db.Employees.Remove(db.Employees.FirstOrDefault(x => x.IdentityUser.Id.Equals(id)));
                        await db.SaveChangesAsync();
                    }

                    if(user.Id != userId)
                    {
                        var result = await userManager.DeleteAsync(user);
                        if (result.Succeeded) return RedirectToAction(nameof(AdminPanel));
                        foreach (var error in result.Errors) { ModelState.AddModelError("", error.Description); }
                        return View(nameof(AdminPanel));
                    }
                    else
                    {
                        ViewBag.ErrorTitle = $"Вы не можете удалить самого себя!";
                        return View("Error");
                    }
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
        public IActionResult CreateRole() { return View(); }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IdentityRole identityRole = new IdentityRole { Name = model.RoleName };
                    IdentityResult result = await roleManager.CreateAsync(identityRole);
                    if (result.Succeeded) return RedirectToAction("AdminPanel", "Admin");
                    foreach (IdentityError error in result.Errors) { ModelState.AddModelError("", error.Description); }
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
                
                var rolePages = db.VisiblePageRoles.Include(x=>x.RolePages).Include(y=>y.IdentityRole).ToList();
                if (rolePages != null)
                {
                    for (int i = 0; i < rolePages.Count; i++)
                    {
                        if (rolePages[i].IdentityRole.Id == id)
                        {
                            if (rolePages[i].IsSelected)
                            {
                                model.Pages.Add(rolePages[i].RolePages.PageName);
                            }
                        }
                    }
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
                    foreach (var error in result.Errors) { ModelState.AddModelError("", error.Description); }
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
            catch { return View(); }
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
            catch { return View("NotFound"); }
        }



        [HttpGet]
        public async Task<IActionResult> EditPagesForRoles(string roleId)
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

                var model = new List<RolePageViewModel>();
                if (db.VisiblePageRoles.Include(y => y.RolePages).ToList() == null)
                {
                    foreach (var rolePages in db.RolePages.ToList())
                    {
                        var rolePageViewModel = new RolePageViewModel
                        {
                            Id = rolePages.Id,
                            RolePages = rolePages
                        };
                        model.Add(rolePageViewModel);
                    }
                }
                else
                {
                    foreach (var visiblePageRoles in db.VisiblePageRoles.Include(y => y.RolePages).ToList())
                    {
                        var rolePageViewModel = new RolePageViewModel
                        {
                            Id = visiblePageRoles.Id,
                            RolePages = visiblePageRoles.RolePages,
                            IdentityRole = role,
                            IsSelected = visiblePageRoles.IsSelected
                        };
                        model.Add(rolePageViewModel);
                    }
                }

                return View(model);
            }
            catch { return View(); }
        }


        [HttpPost]
        public async Task<IActionResult> EditPagesForRoles(List<RolePageViewModel> model, string roleId)
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
                    VisiblePageRole visiblePageRole = new VisiblePageRole();

                    visiblePageRole.Id = model[i].Id;   //Здесь с ID траблы для редактирования, и добавления было

                    visiblePageRole.IdentityRole = role;
                    visiblePageRole.RolePages = db.RolePages.Where(x => x.Id == model[i].RolePages.Id).FirstOrDefault();
                    if (model[i].IsSelected)
                    {
                        if (db.VisiblePageRoles.Any(x=>x.Id == model[i].Id))
                        {
                            visiblePageRole.IsSelected = true;
                            db.Entry(visiblePageRole).State = EntityState.Modified;
                        }
                        else
                        {
                            visiblePageRole.IsSelected = model[i].IsSelected;
                            db.VisiblePageRoles.Add(visiblePageRole);
                        }
                     
                    }
                    else
                    {
                        visiblePageRole.IsSelected = false;
                        db.Entry(visiblePageRole).State = EntityState.Modified;
                    }
                    await db.SaveChangesAsync();

                    if (db.SaveChangesAsync().IsCompletedSuccessfully)
                    {
                        if (i < (model.Count - 1)) continue;
                        else return RedirectToAction("EditRole", new { Id = roleId });
                    }
                }
                return RedirectToAction("EditRole", new { Id = roleId });
            }
            catch { return View("NotFound"); }
        }
        
        [HttpGet]
        public async Task<IActionResult> AdminPanel()
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var listEmployees = await db.Employees.ToListAsync();
            for (var i = 0; i < listEmployees.Count; i++)
            {
                listEmployees[i].EncryptedId = protector.Protect(listEmployees[i].Id.ToString());
            }

            ViewBag.IdentityRoles = await roleManager.Roles.AsNoTracking().ToListAsync() as IEnumerable<IdentityRole>;
            ViewBag.IdentityUsers = await userManager.Users.AsNoTracking().ToListAsync() as IEnumerable<IdentityUser>;
            ViewBag.Employee = listEmployees as IEnumerable<Employee>;

            stopWatch.Stop();
            ViewBag.TotalTime = stopWatch.Elapsed;
            return View();
        }




    }
}