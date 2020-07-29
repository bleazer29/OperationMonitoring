using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OperationMonitoring.Data;
using OperationMonitoring.Models;
using X.PagedList;

namespace OperationMonitoring.Controllers
{
    public class MaintenanceController : Controller
    {
        private readonly ApplicationContext db;
        public MaintenanceController(ApplicationContext context)
        {
            db = context;
        }
        // SORTING
        private List<Maintenance> Sorting(List<Maintenance> maintenance, string sortOrder)
        {
            switch (sortOrder)
            {
                case "status_desc":
                    maintenance = maintenance.OrderBy(x => x.IsOpened).ToList();
                    break;
                case "status":
                    maintenance = maintenance.OrderByDescending(x => x.IsOpened).ToList();
                    break;
                case "datestart_desc":
                    maintenance = maintenance.OrderByDescending(x => x.StartDate).ToList();
                    break;
                case "datestart":
                    maintenance = maintenance.OrderBy(x => x.StartDate).ToList();
                    break;
                case "datefinish_desc":
                    maintenance = maintenance.OrderByDescending(x => x.FinishDate).ToList();
                    break;
                case "datefinish":
                    maintenance = maintenance.OrderBy(x => x.FinishDate).ToList();
                    break;
                case "id_desc":
                    maintenance = maintenance.OrderByDescending(x => x.Id).ToList();
                    break;
                default:
                    maintenance = maintenance.OrderBy(x => x.Id).ToList();
                    break;
            }
            return maintenance;
        }
        // LIST
        public ActionResult Index(string oldSortOrder, string newSortOrder, int? page)
        {
            var maintenance = db.Maintenances.Include(x => x.Equipment).ToList();

            // SORTING           
            if (string.IsNullOrEmpty(newSortOrder))
            {
                newSortOrder = "id";
            }
            else if (newSortOrder == oldSortOrder)
            {
                newSortOrder += "_desc";
            }
            ViewBag.CurrentSort = newSortOrder;
            maintenance = Sorting(maintenance, newSortOrder);

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(maintenance.ToPagedList(pageNumber, pageSize));
        }

        // GET: MaintenanceController/Details/5
        public ActionResult Details(int id)
        {
            var maintenance = db.Maintenances
                .Include(x => x.Equipment)
                .Include(a => a.MaintenanceCategory)
                .Include(a => a.MaintenanceType)
                .Include(x => x.Responsible)
                .FirstOrDefault(x => x.Id == id);
            ViewBag.History = db.MaintenanceHistory.Include(x => x.Maintenance).Where(x => x.Id == id).ToList();
            ViewData["Scans"] = db.Docs.Where(x => x.Id == maintenance.Id).ToList();
            return View(maintenance);
        }

        //// GET: MaintenanceController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: MaintenanceController/Delete/5
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
