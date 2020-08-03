using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Castle.Core.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        // SEARCHING
        private List<Nomenclature> Searching(List<Nomenclature> nomenclature,  string searchField, string searchString)
        {
            switch (searchField)
            {
                case "Title":
                    nomenclature = nomenclature.Where(x => x.Name.ToLower()
                        .Contains(searchString.ToLower())).ToList();
                    break;
                case "Provider":
                    nomenclature = nomenclature.Where(x => x.Provider.Name.ToLower()
                        .Contains(searchString.ToLower())).ToList();
                    break;
                case "Code":
                    nomenclature = nomenclature.Where(x => x.VendorCode.ToLower()
                        .Contains(searchString.ToLower())).ToList();
                    break;
                default:
                    break;
            }

            return nomenclature;
        }

        // PRESET
        public async Task<ActionResult> Preset(int id, int? presetId, int? page, string searchString, string searchField, string currentSearch, string presetParameters)
        {
            if (searchField.IsNullOrEmpty())
            {
                searchField = "Title";
            }
            ViewBag.SearchField = searchField;

            var nomenclature = db.Nomenclatures
                .Include(x => x.Specification)
                .ThenInclude(i => i.UsageType)
                .Include(x => x.Provider).ToList();

             var equipment = db.Equipment
                .Include(x => x.Department)
                .Include(x => x.Category)
                .Include(x => x.Type)
                .Include(x => x.Status)
                .FirstOrDefault(x => x.Id == id);
            ViewBag.Equipment = equipment;

            var preset = db.Presets
               .Include(x => x.PresetItems)
                   .ThenInclude(i => i.Nomenclature)
                       .ThenInclude(ii => ii.Provider)
               .Include(x => x.PresetItems)
                   .ThenInclude(i => i.Nomenclature)
                       .ThenInclude(ii => ii.Specification)
               .FirstOrDefault(x => x.Id == presetId);
            if (preset == null)
            {
                preset = new Preset() { Equipment = equipment };
                db.Presets.Add(preset);
                await db.SaveChangesAsync();
            }
            ViewBag.Preset = preset;

            if (searchString.IsNullOrEmpty())
            {
                if (!currentSearch.IsNullOrEmpty())
                {
                    nomenclature = Searching(nomenclature, searchField, currentSearch);
                }               
            }
            else
            {
                nomenclature = Searching(nomenclature, searchField, searchString);
                currentSearch = searchString;
                page = 1;
            }
            ViewBag.CurrentFilter = currentSearch;
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(nomenclature.ToPagedList(pageNumber, pageSize));
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
