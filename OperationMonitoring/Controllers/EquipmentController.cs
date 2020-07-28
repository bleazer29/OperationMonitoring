using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OperationMonitoring.Data;
using OperationMonitoring.Models;
using X.PagedList;

namespace OperationMonitoring.Controllers
{
    public class EquipmentController : Controller
    {
        private readonly ApplicationContext db;
        public EquipmentController(ApplicationContext context)
        {
            db = context;
        }

        // GET: EquipmentController
        public ActionResult Index(int? page, string searchString)
        {
            var equipment = db.Equipment;
            if (searchString != null)
            {
                page = 1;
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(equipment.ToPagedList(pageNumber, pageSize));
        }

        // CREATE
        public ActionResult Create()
        {
            ViewBag.DepartmentList = db.Departments.ToList();
            ViewBag.CategoryList = db.EquipmentCategories.ToList();
            ViewBag.TypeList = db.EquipmentTypes.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Equipment equipment, string department, string category, string type)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    equipment.Status = db.EquipmentStatuses.FirstOrDefault(x => x.Title == "NA");
                    equipment.Department = db.Departments.FirstOrDefault(x => x.Id == int.Parse(department));
                    equipment.Category = db.EquipmentCategories.FirstOrDefault(x => x.Id == int.Parse(category));
                    equipment.Type = db.EquipmentTypes.FirstOrDefault(x => x.Id == int.Parse(type));
                    db.Equipment.Add(equipment);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Preset", new { equipment.Id });
                }
                else
                {
                    ViewBag.DepartmentList = db.Departments.ToList();
                    ViewBag.CategoryList = db.EquipmentCategories.ToList();
                    ViewBag.TypeList = db.EquipmentTypes.ToList();
                    return View();
                }                    
            }
            catch
            {
                ViewBag.DepartmentList = db.Departments.ToList();
                ViewBag.CategoryList = db.EquipmentCategories.ToList();
                ViewBag.TypeList = db.EquipmentTypes.ToList();
                return View();
            }
        }

        // PRESET
        public ActionResult Preset(int equipmentId)
        {
            var equipment = db.Equipment.FirstOrDefault(x => x.Id == equipmentId);
            return View(equipment);
        }


        // GET: EquipmentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        // GET: EquipmentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EquipmentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EquipmentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EquipmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
