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
    public class ProvidersController : Controller
    {
        private readonly ApplicationContext db;
        private int pageSize = 10;
        public ProvidersController(ApplicationContext context)
        {
            db = context;
        }

        // SEARCH
        private List<Provider> Searching(List<Provider> providers, string searchField, string searchString)
        {
            switch (searchField)
            {
                case "Name":
                    providers = providers.Where(x => x.Name.ToLower().Contains(searchString.ToLower())).ToList();
                    break;
                case "Address":
                    providers = providers.Where(x => x.Address != null && x.Address.ToLower().Contains(searchString.ToLower())).ToList();
                    break;
                case "EDRPOU":
                    providers = providers.Where(x => x.EDRPOU != null && x.EDRPOU.ToLower().Contains(searchString.ToLower())).ToList();
                    break;
                default:
                    break;
            }          
            return providers;
        }

        // SORTING
        private List<Provider> Sorting(string sortOrder, List<Provider> providers)
        {
            switch (sortOrder)
            {
                case "name_desc":
                    providers = providers.OrderByDescending(s => s.Name).ToList();
                    break;
                case "address_desc":
                    providers = providers.OrderByDescending(s => s.Address).ToList();
                    break;
                case "address":
                    providers = providers.OrderBy(s => s.Address).ToList();
                    break;
                case "edrpou_desc":
                    providers = providers.OrderByDescending(s => s.EDRPOU).ToList();
                    break;
                case "edrpou":
                    providers = providers.OrderBy(s => s.EDRPOU).ToList();
                    break;
                default:
                    providers = providers.OrderBy(s => s.Name).ToList();
                    break;
            }
            return providers;
        }

        // GET: ProvidersController
        public ActionResult Index(string oldSortOrder, string newSortOrder, string searchString, string searchField, int? page)
        {
            ViewBag.CurrentFilter = searchString;
            ViewBag.SearchField = string.IsNullOrEmpty(searchField) ? "Name" : searchField;
          
            var providers = db.Providers.ToList();
            ViewBag.Providers = providers;

            // SEARCH
            providers = Searching(providers, searchField, searchString);

            // SORTING
            if (string.IsNullOrEmpty(newSortOrder))
            {
                newSortOrder = "name";
            }
            else if (newSortOrder == oldSortOrder)
            {
                newSortOrder += "_desc";
            }
            ViewBag.CurrentSort = newSortOrder;
            providers = Sorting(newSortOrder, providers);

            if (searchString != null)
            {
                page = 1;
            }          
            int pageNumber = (page ?? 1);


            return View(providers.ToPagedList(pageNumber, pageSize));
            
        }

        // DETAILS 
        [HttpGet("/[controller]/[action]/{id}")]
        public ActionResult Details(int id)
        {
            var provider = db.Providers.FirstOrDefault(x => x.Id == id);     
            return View(provider);
        }

        // CREATE
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Provider provider)
        {
            if (ModelState.IsValid)
            {
                db.Providers.Add(provider);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        // EDIT PROVIDER
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditProvider(int providerId, string editName, string editAddress, string editEDRPOU)
        {
            try
            {
                var provider = db.Providers.FirstOrDefault(x => x.Id == providerId);
                provider.Name = editName;
                provider.Address = editAddress;
                provider.EDRPOU = editEDRPOU;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = providerId });
            }
            catch
            {
                return RedirectToAction("Details", new { id = providerId });
            }
        }
    }
}
