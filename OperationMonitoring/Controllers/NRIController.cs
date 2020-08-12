using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OperationMonitoring.Data;
using OperationMonitoring.Models;

namespace OperationMonitoring.Controllers
{
    public class NRIController : Controller
    {
        private readonly ApplicationContext db;
        public NRIController(ApplicationContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Departments()
        {
            List<Department> departments = db.Departments.ToList();
            ViewBag.Departments = departments;
            return View();
        }

        public ActionResult CreateDepartment()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateDepartment(Department department)
        {
            if (ModelState.IsValid)
            {
                db.Departments.Add(department);
                await db.SaveChangesAsync();
                return RedirectToAction("Departments");
            }
            else
            {
                return View();
            }
        }
    }
}
