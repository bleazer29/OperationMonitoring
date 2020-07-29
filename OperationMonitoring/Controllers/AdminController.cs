using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OperationMonitoring.Models;

namespace OperationMonitoring.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;

        public AdminController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ListUsers()
        {
            return View(userManager.Users);
        }

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
                    if (result.Succeeded)  return RedirectToAction("ListUsers");
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
            try
            {
                var user = await userManager.FindByIdAsync(id);
                if (user == null)
                {
                    ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                    return View("NotFound");
                }
                else
                {
                    var result = await userManager.DeleteAsync(user);
                    if (result.Succeeded) return RedirectToAction(nameof(ListUsers));
                    foreach (var error in result.Errors)  {  ModelState.AddModelError("", error.Description);  }
                    return View(nameof(ListUsers));
                }
            }
            catch { return View(); }
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IdentityRole identityRole = new IdentityRole {  Name = model.RoleName  };
                    IdentityResult result = await roleManager.CreateAsync(identityRole);
                    if (result.Succeeded) return RedirectToAction("listroles", "admin");
                    foreach (IdentityError error in result.Errors) { ModelState.AddModelError("", error.Description);}
                }
                return View(model);
            }
            catch
            {
                return View(model);
            }
            
        }

        [HttpGet]
        public IActionResult ListRoles()
        {
            return View(roleManager.Roles);
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
            catch
            {
                return View();
            }
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
                    if (result.Succeeded) return RedirectToAction("ListRoles");
                    foreach (var error in result.Errors)  {  ModelState.AddModelError("", error.Description); }
                    return View(model);
                }
            }
            catch { return View(model); }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            try
            {
                var role = await roleManager.FindByIdAsync(id);
                if (role == null)
                {
                    ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                    return View("NotFound");
                }
                else
                {
                    var result = await roleManager.DeleteAsync(role);
                    if (result.Succeeded) return RedirectToAction("ListRoles");
                    foreach (var error in result.Errors) { ModelState.AddModelError("", error.Description); }
                    return View("ListRoles");
                }
            }
            catch {  return View(); }
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

    }
}