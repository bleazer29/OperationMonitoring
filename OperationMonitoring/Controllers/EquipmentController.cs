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
        public ActionResult Index()
        {
            var equipment = db.Equipment.Include(x => x.Department)
                .Include(x => x.Category)
                .Include(x => x.Type)
                .Include(x => x.Status)
                .OrderBy(x => x.Status.Title)
                .ToList();
            ViewBag.Orders = db.Orders.Where(x => x.IsOpen == true)
                .Include(x => x.Well)
                .ThenInclude(c => c.Counterparty)
                .Include(x => x.Equipment)
                .Include(x => x.Agreement).ToList();
            ViewBag.StatusList = db.EquipmentStatuses.ToList();
            return View(equipment);
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
        public async Task<ActionResult> Preset(int id)
        {
            var nomenclature = db.Nomenclatures
                .Include(x => x.Specification)
                .ThenInclude(i => i.UsageType)
                .Include(x => x.Provider)
                .OrderBy(x => x.VendorCode)
                .ToList();

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
               .FirstOrDefault(x => x.Equipment.Id == id);
            if (preset == null)
            {
                preset = new Preset() { Equipment = equipment };
                db.Presets.Add(preset);
                await db.SaveChangesAsync();
            }
            ViewBag.Preset = preset;
            return View(nomenclature);
        }

        public async Task<ActionResult> AssemblePreset(int equipmentId, int? presetId)
        {
            return RedirectToAction("Index");
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> AssemblePreset(int id, string param)
        //{
        //    try
        //    {
        //        // get items from storage,
        //        // create assembly from storage items,
        //        return RedirectToAction("FinishCreate", new { id });
        //    }
        //    catch
        //    {
        //        return RedirectToAction("FinishCreate", new { id });
        //    }
        //}
        //public async Task<ActionResult> FinishCreate(int id)
        //{
        //    var equipment = db.Equipment
        //        .Include(x => x.Department)
        //        .Include(x => x.Category)
        //        .Include(x => x.Type)
        //        .Include(x => x.Status)
        //        .FirstOrDefault(x => x.Id == id);
        //    return View(equipment);
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> FinishCreate(int id, string operatingTime, string warningTime)
        //{
        //    try
        //    {
        //        var equipment = db.Equipment
        //        .Include(x => x.Department)
        //        .Include(x => x.Category)
        //        .Include(x => x.Type)
        //        .Include(x => x.Status)
        //        .FirstOrDefault(x => x.Id == id);
        //        equipment.Status = db.EquipmentStatuses.FirstOrDefault(x => x.Title == "RFU");
        //        equipment.OperatingTime = int.Parse(operatingTime) * 60;
        //        equipment.WarningTime = int.Parse(warningTime) * 60;
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return RedirectToAction("FinishCreate", new { id });
        //    }            
        //}

        // GET: EquipmentController/Details/5
        public ActionResult Details(int id)
        {
            var equipment = db.Equipment
                .Include(x => x.Department)
                .Include(x => x.Category)
                .Include(x => x.Type)
                .Include(x => x.Status)
                .FirstOrDefault(x => x.Id == id);
            ViewBag.CounterpartyList = db.Counterparties.OrderBy(x => x.Title).ToList();
            ViewBag.AgreementList = db.Agreements.OrderBy(x => x.Counterparty.Id).ToList();
            ViewBag.WellList = db.Wells.OrderBy(x => x.Counterparty.Id).ToList();
            switch (equipment.Status.Title)
            {
                case "NA":
                    break;
                case "RFU":
                    break;
                case "JF":
                    var order = db.Orders.Include(x => x.Agreement)
                            .ThenInclude(contract => contract.Counterparty)
                        .Include(x => x.Well)
                        .FirstOrDefault(x => x.Equipment.Id == equipment.Id && x.IsOpen == true);
                    ViewBag.Order = order;
                    break;
                case "WS":
                    break;
                case "SP":
                case "RP":
                    var maintenance = db.Maintenances.Include(x => x.Equipment)
                            .ThenInclude(equip => equip.Status)
                            .FirstOrDefault(x => x.Equipment.Id == equipment.Id
                            && x.IsOpened == true);
                    ViewBag.Maintenance = maintenance;
                    ViewBag.MaintenanceHistory = db.MaintenanceHistory.Include(x => x.Maintenance).Where(x => x.Id == id).ToList();
                    ViewBag.Scans = db.Docs.Where(x => x.Id == maintenance.Id).ToList();
                    //ViewBag.Scans = db.DocScans.Where(x => x.Maintenance.Id == maintenance.Id).ToList();
                    break;
                default:
                    break;
            }
            ViewBag.History = db.EquipmentHistory.Include(x => x.Status)
                .Where(x => x.Equipment == equipment).ToList();
            ViewBag.DepartmentList = db.Departments.ToList();
            ViewBag.CategoryList = db.EquipmentCategories.ToList();
            ViewBag.TypeList = db.EquipmentTypes.ToList();
            // also - 
            // assembly
            // order
            // maintenance
            return View(equipment);
        }
        // EDIT INFO
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditEquipment(int equipmentId,
                string editDepartment, string editCategory,
                string editType, string editTitle,
                string editSerialNum, string editInventoryNum, 
                string editDiameterOuter, string editDiameterInner, string editLength,
                string editOperatingTime, string editWarningTime)
        {
            try
            {
                var equipment = db.Equipment.Include(x => x.Status)
                    .FirstOrDefault(x => x.Id == equipmentId);
                equipment.Department = db.Departments.FirstOrDefault(x => x.Id == int.Parse(editDepartment));
                equipment.Category = db.EquipmentCategories.FirstOrDefault(x => x.Id == int.Parse(editCategory));
                equipment.Type = db.EquipmentTypes.FirstOrDefault(x => x.Id == int.Parse(editType));
                equipment.Title = editTitle;
                equipment.SerialNum = editSerialNum;
                equipment.InventoryNum = editInventoryNum;
                equipment.DiameterOuter = int.Parse(editDiameterOuter);
                equipment.DiameterInner = int.Parse(editDiameterInner);
                equipment.Length = int.Parse(editLength);
                equipment.OperatingTime = int.Parse(editOperatingTime) * 60;
                equipment.WarningTime = int.Parse(editWarningTime) * 60;

                EquipmentHistory history = new EquipmentHistory()
                {
                    Date = DateTime.Now,
                    Status = equipment.Status,
                    Equipment = equipment,
                    Message = "Properties edited"
                };
                db.EquipmentHistory.Add(history);

                db.SaveChanges();
                return RedirectToAction("Details", new { id = equipmentId });
            }
            catch
            {
                return RedirectToAction("Details", new { id = equipmentId });
            }
        }

        //// GET: EquipmentController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: EquipmentController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
