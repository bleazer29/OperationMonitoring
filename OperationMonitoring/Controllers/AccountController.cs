using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OperationMonitoring.Data;
using OperationMonitoring.Models;
using OperationMonitoring.ModelsIdentity;
using OperationMonitoring.ModelsIdentity.Security;

namespace OperationMonitoring.Controllers
{
    public class AccountController : Controller
    {

        private readonly ApplicationContext db;
        private readonly UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager { get; set; }
        private readonly IDataProtector protector;

        public AccountController(UserManager<IdentityUser> userManager, 
                                    SignInManager<IdentityUser> signInManager, 
                                    ApplicationContext db,
                                    IDataProtectionProvider dataProtectionProvider, 
                                    DataProtectionPurposeStrings dataProtectionPurposeStrings)
        {
            this.db = db;
            this.userManager = userManager;
            this.signInManager = signInManager;
            protector = dataProtectionProvider.CreateProtector(dataProtectionPurposeStrings.EmployeeIdRouteValue);
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterEmployeePageViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new IdentityUser
                    {
                        UserName = model.RegisterViewModel.Email,
                        Email = model.RegisterViewModel.Email
                    };

                    // добавляем пользователя
                    var result = await userManager.CreateAsync(user, model.RegisterViewModel.Password);
                   
                    /////////////////////////////////////////////////////////////

                    //Добавление первой роли для администратора
                    //IdentityRole identityRole = new IdentityRole 
                    //{
                    //    Name = "Admin"
                    //};


                    //IdentityResult rolesResult = await this.roleManager.CreateAsync(identityRole);

                    //var firstRoles = new IdentityUserRole<string>
                    //{
                    //    RoleId = identityRole.Id,
                    //    UserId = user.Id
                    //};

                    //db.UserRoles.Add(firstRoles);
                    //db.SaveChanges();


                    /////////////////////////////////////////////////////////////////
                    if (result.Succeeded)
                    {
                        Employee employee = new Employee
                        {
                            IdentityUser = user
                        };
                        db.Employees.Add(employee);
                        db.SaveChanges();
                   
                        var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
                        //var callbackUrl = Url.Action("ConfirmEmail", "Account",new { userId = user.Id, code = code },protocol: HttpContext.Request.Scheme);  //подтверждение почты на gmail
                        //EmailServices emailService = new EmailServices();
                        //await emailService.SendEmailAsync(model.RegisterViewModel.Email, "Confirm your account", $"Подтвердите регистрацию, перейдя по ссылке: <a href='{callbackUrl}'>link</a>");
                        var result1 = await userManager.ConfirmEmailAsync(user, code);  //первое подтверждение по умолчанию
                        if (result1.Succeeded)
                        {
                            return View();
                        }
                        if (signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
                        {
                            return RedirectToAction("ListUsers", "Admin");
                        }
                        //return Content("Для завершения регистрации проверьте электронную почту и перейдите по ссылке, указанной в письме");
                       
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                return View(model);
            } 
            catch (Exception ex) { return Content(ex.Message); }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            try
            {
                if(userId==null && code == null) return View("Error"); 
                var user = await userManager.FindByIdAsync(userId);
                if (user == null)  {  return View("Error"); }
                var result = await userManager.ConfirmEmailAsync(user, code);
                if (result.Succeeded) { return RedirectToAction("Index", "Home"); }
                else { return View("Error");  }
            }
            catch { return View("Error"); }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
           await signInManager.SignOutAsync();
           return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel, string returnUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await userManager.FindByNameAsync(loginViewModel.Email);
                    if (user != null)
                    {
                        if (!await userManager.IsEmailConfirmedAsync(user))
                        {
                            ModelState.AddModelError(string.Empty, "Вы не подтвердили свой email");
                            return View(loginViewModel);
                        }
                    }

                    var result = await signInManager.PasswordSignInAsync(loginViewModel.Email, loginViewModel.Password, loginViewModel.RememberMe, true);
                    if (result.Succeeded)
                    {
                        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl)) return Redirect(returnUrl);
                        else return RedirectToAction("Index", "Home");
                    }
                    else { ModelState.AddModelError("", "Неправильный логин и (или) пароль"); }
                    if (result.IsLockedOut)  return View("AccountLocked");
                }
                return View(loginViewModel);
            }
            catch { return View(loginViewModel); }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await userManager.GetUserAsync(User);
                    if (user == null)  return RedirectToAction("Login");

                    var result = await userManager.ChangePasswordAsync(user,  model.CurrentPassword, model.NewPassword);
                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                        return View();
                    }
                    await signInManager.RefreshSignInAsync(user);
                    return View("ChangePasswordConfirmation");
                }
                return View(model);
            }
            catch {  return View(model);  }
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await userManager.FindByEmailAsync(model.Email);
                    if (user == null || !await userManager.IsEmailConfirmedAsync(user)) return View("ForgotPasswordConfirmation");
                    var token = await userManager.GeneratePasswordResetTokenAsync(user);
                    var callbackUrl = Url.Action("ResetPassword", "Account", new { token = token, email = model.Email }, Request.Scheme);
                    EmailServices emailService = new EmailServices();
                    await emailService.SendEmailAsync(model.Email, "Reset Password", $"Для сброса пароля пройдите по ссылке: <a href='{callbackUrl}'>link</a>");
                    return View("ForgotPasswordConfirmation");
                }
                return View(model);
            }
            catch {  return View(model); }
            
        }

        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            if (token == null || email == null)  ModelState.AddModelError("", "Invalid password reset token");
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await userManager.FindByEmailAsync(model.Email);

                    if (user != null)
                    {
                        var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);
                        if (result.Succeeded)
                        {
                            if (await userManager.IsLockedOutAsync(user)) await userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow);
                            return View("ResetPasswordConfirmation");
                        }
                        foreach (var error in result.Errors) { ModelState.AddModelError("", error.Description); }
                        return View(model);
                    }
                    return View("ResetPasswordConfirmation");
                }
                return View(model);
            }
            catch  { return View(model);  }
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditProfile() 
        {
            try
            {
                var userId = this.userManager.GetUserId(HttpContext.User);
                if (userId == null) return View();
                else return View(await db.Employees.FirstOrDefaultAsync(x => x.IdentityUser.Id.Equals(userId)));
            }
            catch { return View(); }
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(int id, Employee employees)
        {
            try
            {
                this.db.Entry(employees).State = EntityState.Modified;
                await this.db.SaveChangesAsync();
                return RedirectToAction(nameof(EditProfile));
            }
            catch { return View(); }
        }


    }
}
