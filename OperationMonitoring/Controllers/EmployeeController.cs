using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OperationMonitoring.Data;
using OperationMonitoring.Models;
using Microsoft.EntityFrameworkCore;
using OperationMonitoring.ModelsIdentity.Security;
using Microsoft.AspNetCore.DataProtection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace OperationMonitoring.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee

        private readonly ApplicationContext db;
        private readonly IDataProtector protector;
        private readonly UserManager<IdentityUser> userManager;
        public EmployeeController(ApplicationContext db, IDataProtectionProvider dataProtectionProvider,
            DataProtectionPurposeStrings dataProtectionPurposeStrings, UserManager<IdentityUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;
            protector = dataProtectionProvider.CreateProtector(dataProtectionPurposeStrings.EmployeeIdRouteValue);
        }

        public ActionResult Index()
        {
            return View(db.Employees.AsNoTracking().ToList().Select(e =>
            {
                e.EncryptedId = protector.Protect(e.Id.ToString());
                return e;
            }));
        }


        // GET: Employee/Details/5
        public ActionResult Details(string id)
        {
            return View(db.Employees.AsNoTracking().FirstOrDefault(x => x.Id == Convert.ToInt32(protector.Unprotect(id))));
        }

        // GET: Employee/Create
        public ActionResult Create() { return View();  }

        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee employees)
        {
            try
            {
                Employee employee = new Employee
                {
                    BirthDate = employees.BirthDate,
                    FirstName = employees.FirstName,
                    LastName = employees.LastName,
                    Patronymic = employees.Patronymic
                };
                db.Add(employee);
                db.SaveChanges();
                return RedirectToAction("AdminPanel","Admin");
            }
            catch {   return View();   }
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(string id)
        {
            return View(db.Employees.AsNoTracking().FirstOrDefault(x => x.Id == Convert.ToInt32(protector.Unprotect(id))));
        }

        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, Employee employees)
        {
            try
            {
                employees.EncryptedId = id;
                employees.Id = Convert.ToInt32(protector.Unprotect(id));
                db.Entry(employees).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AdminPanel", "Admin");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(string id)
        {
            return View(db.Employees.AsNoTracking().FirstOrDefault(x => x.Id == Convert.ToInt32(protector.Unprotect(id))));
        }

        // POST: Employee/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, IFormCollection collection)
        {
            try
            {
                var userId = userManager.GetUserId(HttpContext.User);
                var employee = db.Employees.AsNoTracking().Include(i=>i.IdentityUser).FirstOrDefault(x => x.Id == Convert.ToInt32(protector.Unprotect(id))) ;
               
                if (employee.IdentityUser ==null || employee.IdentityUser.Id != userId)
                {
                    db.Employees.Remove(db.Employees.FirstOrDefault(x => x.Id == Convert.ToInt32(protector.Unprotect(id))));
                    db.SaveChanges();
                    return RedirectToAction("AdminPanel", "Admin");
                }
                else
                {
                    return Content("Извините, но вы не можете удалить свои данные! Для редактирование перейдите во вкладу List Employee!");
                }
                
            }
            catch { return View(); }
        }
    }
}
