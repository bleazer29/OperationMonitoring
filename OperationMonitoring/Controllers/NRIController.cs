using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OperationMonitoring.Data;
using OperationMonitoring.Models;
using OperationMonitoring.Models.Interfaces;

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

        // DEPARTMENT
        public IActionResult Departments()
        {
            List<Department> departments = db.Departments.ToList();
            ViewBag.Departments = departments;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateDepartment(Department department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Departments.Add(department);
                    await db.SaveChangesAsync();
                }
                return RedirectToAction("Departments");
            }
            catch
            {
                return RedirectToAction("Departments");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditDepartment(int editId, string editTitle, string editAddress)
        {
            try
            {
                var department = db.Departments.FirstOrDefault(x => x.Id == editId);
                department.Title = editTitle;
                department.Address = editAddress;
                await db.SaveChangesAsync();
                return RedirectToAction("Departments");
            }
            catch
            {
                return RedirectToAction("Departments");
            }
        }

        // EQUIPMENT TYPE
        public IActionResult EquipmentTypes()
        {
            var data = db.EquipmentTypes.ToList();
            var types = data.Cast<INri>().ToList();
            ViewBag.Types = types;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateType(EquipmentType type)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.EquipmentTypes.Add(type);
                    await db.SaveChangesAsync();
                }
                return RedirectToAction("EquipmentTypes");
            }
            catch
            {
                return RedirectToAction("EquipmentTypes");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditType(int editId, string editTitle)
        {
            try
            {
                var type = db.EquipmentTypes.FirstOrDefault(x => x.Id == editId);
                type.Title = editTitle;
                await db.SaveChangesAsync();
                return RedirectToAction("EquipmentTypes");
            }
            catch
            {
                return RedirectToAction("EquipmentTypes");
            }
        }

        // EQUIPMENT CATEGORY
        public IActionResult EquipmentCategories()
        {
            var data = db.EquipmentCategories.ToList();
            var categories = data.Cast<INri>().ToList();
            ViewBag.Categories = categories;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateCategory(EquipmentCategory category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.EquipmentCategories.Add(category);
                    await db.SaveChangesAsync();
                }
                return RedirectToAction("EquipmentCategories");
            }
            catch
            {
                return RedirectToAction("EquipmentCategories");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditCategory(int editId, string editTitle)
        {
            try
            {
                var category = db.EquipmentCategories.FirstOrDefault(x => x.Id == editId);
                category.Title = editTitle;
                await db.SaveChangesAsync();
                return RedirectToAction("EquipmentCategories");
            }
            catch
            {
                return RedirectToAction("EquipmentCategories");
            }
        }

        // MAINTENANCE TYPE
        public IActionResult MaintenanceTypes()
        {
            var data = db.MaintenanceTypes.ToList();
            var types = data.Cast<INri>().ToList();
            ViewBag.Types = types;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateTypeMaintenance(MaintenanceType type)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.MaintenanceTypes.Add(type);
                    await db.SaveChangesAsync();
                }
                return RedirectToAction("MaintenanceTypes");
            }
            catch
            {
                return RedirectToAction("MaintenanceTypes");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditTypeMaintenance(int editId, string editTitle)
        {
            try
            {
                var type = db.MaintenanceTypes.FirstOrDefault(x => x.Id == editId);
                type.Title = editTitle;
                await db.SaveChangesAsync();
                return RedirectToAction("MaintenanceTypes");
            }
            catch
            {
                return RedirectToAction("MaintenanceTypes");
            }
        }

        // MAINTENANCE CATEGORY
        public IActionResult MaintenanceCategories()
        {
            var data = db.MaintenanceCategories.ToList();
            var categories = data.Cast<INri>().ToList();
            ViewBag.Categories = categories;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateCategoryMaintenance(MaintenanceCategory category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.MaintenanceCategories.Add(category);
                    await db.SaveChangesAsync();
                }
                return RedirectToAction("MaintenanceCategories");
            }
            catch
            {
                return RedirectToAction("MaintenanceCategories");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditCategoryMaintenance(int editId, string editTitle)
        {
            try
            {
                var category = db.MaintenanceCategories.FirstOrDefault(x => x.Id == editId);
                category.Title = editTitle;
                await db.SaveChangesAsync();
                return RedirectToAction("MaintenanceCategories");
            }
            catch
            {
                return RedirectToAction("MaintenanceCategories");
            }
        }

        // PROVIDERS


        // NOMENCLATURE


        // COUNTERPARTIES 


        // STORAGES


        // EMPLOYEES


        // POSITIONS
        public IActionResult Positions()
        {
            var data = db.Positions.ToList();
            var positions = data.Cast<INri>().ToList();
            ViewBag.Positions = positions;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreatePosition(Position position)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Positions.Add(position);
                    await db.SaveChangesAsync();
                }
                return RedirectToAction("Positions");
            }
            catch
            {
                return RedirectToAction("Positions");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditPosition(int editId, string editTitle)
        {
            try
            {
                var position = db.Positions.FirstOrDefault(x => x.Id == editId);
                position.Title = editTitle;
                await db.SaveChangesAsync();
                return RedirectToAction("Positions");
            }
            catch
            {
                return RedirectToAction("Positions");
            }
        }
    }
}
