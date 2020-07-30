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
        public ActionResult Index(string oldSortOrder, string newSortOrder, string searchString, string searchField, int? page)
        {
            ViewBag.CurrentFilter = searchString;
            ViewBag.SearchField = string.IsNullOrEmpty(searchField) ? "Name" : searchField;

            List<Storage> storages = db.Storages.Include(x => x.Parent).ThenInclude(x => x.Parent).ToList();
            List<TreeViewStorage> treeViewStorages = new List<TreeViewStorage>();
            foreach(Storage storage in storages)
            {
                treeViewStorages.Add(new TreeViewStorage(storage));
            }
            //ViewBag.Providers = providers;

            //// SEARCH
            //providers = Searching(providers, searchField, searchString);

            //// SORTING
            //if (string.IsNullOrEmpty(newSortOrder))
            //{
            //    newSortOrder = "name";
            //}
            //else if (newSortOrder == oldSortOrder)
            //{
            //    newSortOrder += "_desc";
            //}
            //ViewBag.CurrentSort = newSortOrder;
            //providers = Sorting(newSortOrder, providers);

            //if (searchString != null)
            //{
            //    page = 1;
            //}
            //int pageNumber = (page ?? 1);


            return View(treeViewStorages);

        }

        // GET: StoragesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StoragesController/Create
        public ActionResult Create()
        {
            var storages = db.Storages.ToList();
            ViewBag.Storages = storages;
            ViewBag.StoragesJS = JSConverter.SerializeObject(storages);
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
                    // Logic to add the book to DB
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
