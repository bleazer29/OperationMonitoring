using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OperationMonitoring.Data;
using OperationMonitoring.Helpers;
using OperationMonitoring.Models;
using X.PagedList;

namespace OperationMonitoring.Controllers
{
    public class StoragesController : Controller
    {
        private readonly ApplicationContext db;
        private int pageSize = 10;
        public StoragesController(ApplicationContext context)
        {
            db = context;
        }

        // GET: StoragesController
        public ActionResult Index()
        {
            //ViewBag.CurrentFilter = searchString;
            //ViewBag.SearchField = string.IsNullOrEmpty(searchField) ? "Title" : searchField;
            List<Stock> stocks = db.Stocks.Include(x => x.Nomenclature).ThenInclude(x=> x.Provider)
                .Include(x => x.Equipment).ThenInclude(x => x.Status)
                .Include(x => x.Part).ThenInclude(x => x.Status)
                .ToList();
            ViewBag.Stocks = stocks;
            List<Storage> storages = db.Storages.Include(x => x.Parent).ThenInclude(x => x.Parent).ToList();
            List<TreeViewStorage> treeViewStorages = new List<TreeViewStorage>();
            foreach(Storage storage in storages)
            {
                treeViewStorages.Add(new TreeViewStorage(storage));
            }
            return View(treeViewStorages);
        }

        // GET: StoragesController/Details/5
        public ActionResult Details(int id)
        {
            var storages = db.Storages.Include(x => x.Parent).ThenInclude(x => x.Parent).ToList();
            ViewBag.Storages = storages;

            var storage = storages.FirstOrDefault(i => i.Id == id);

            List<Storage> storageParents = new List<Storage>();
            var p = storage.ParentId;
            while (p != null)
            {
                var item = storages.FirstOrDefault(x => x.Id == p);
                storageParents.Add(item);
                p = item.ParentId;
            }
            ViewBag.StorageParents = storageParents;
            return View(storage);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditStorage(int storageId, string editName, string editAddress, int? parentId)
        {
            try
            {
                var storage = db.Storages.FirstOrDefault(x => x.Id == storageId);
                storage.Title = editName;
                storage.Location = editAddress;
                storage.ParentId = parentId;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = storageId });
            }
            catch
            {
                return RedirectToAction("Details", new { id = storageId });
            }           
        }

        // GET: StoragesController/Create
        public ActionResult Create()
        {
            var storages = db.Storages.ToList();
            ViewBag.Storages = storages;
            return View();
        }

        // POST: StoragesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Storage storage)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Storages.Add(storage);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: StoragesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: StoragesController/Edit/5
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

        // GET: StoragesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StoragesController/Delete/5
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
