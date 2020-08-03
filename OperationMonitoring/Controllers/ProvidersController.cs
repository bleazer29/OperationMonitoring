using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OperationMonitoring.Data;
using OperationMonitoring.Models;
using X.PagedList;

namespace OperationMonitoring.Controllers
{
    public class ProvidersController : Controller
    {
        private readonly ApplicationContext db;
        private int pageSize = 10;
        private int pageSizeStocks = 5;
        public ProvidersController(ApplicationContext context)
        {
            db = context;
        }

        // SEARCH
        private List<Provider> Searching(List<Provider> providers, string searchField, string searchString)
        {
            switch (searchField)
            {
                case "Title":
                    providers = providers.Where(x => x.Title.ToLower().Contains(searchString.ToLower())).ToList();
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
                    providers = providers.OrderByDescending(s => s.Title).ToList();
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
                    providers = providers.OrderBy(s => s.Title).ToList();
                    break;
            }
            return providers;
        }
        private List<Stock> SortingStock(string sortOrder, List<Stock> stocks)
        {
            switch (sortOrder)
            {
                case "vendorCode_desc":
                    stocks = stocks.OrderByDescending(s => s.Nomenclature.VendorCode).ToList();
                    break;
                case "amount_desc":
                    stocks = stocks.OrderByDescending(s => s.Amount).ToList();
                    break;
                case "amount":
                    stocks = stocks.OrderBy(s => s.Amount).ToList();
                    break;
                case "storage_desc":
                    stocks = stocks.OrderByDescending(s => s.Storage.Title).ToList();
                    break;
                case "storage":
                    stocks = stocks.OrderBy(s => s.Storage.Title).ToList();
                    break;
                default:
                    stocks = stocks.OrderBy(s => s.Nomenclature.VendorCode).ToList();
                    break;
            }
            return stocks;
        }

        // GET: ProvidersController
        public ActionResult Index(string oldSortOrder, string newSortOrder, string searchString, string searchField, int? page)
        {
            ViewBag.CurrentFilter = searchString;
            ViewBag.SearchField = string.IsNullOrEmpty(searchField) ? "Title" : searchField;
          
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
 

        // CREATE
        public ActionResult Create()
        {
            ViewBag.Partial = false;
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

        // DETAILS 
        [HttpGet("/[controller]/[action]/{id}")]
        public ActionResult Details(int id, string oldSortOrder, string newSortOrder, int? page)
        {
            List<Stock> stocks = db.Stocks
                .Where(x => x.Amount > 0
                && x.Nomenclature != null 
                && x.Nomenclature.Provider != null 
                && x.Nomenclature.Provider.Id == id)
                .Include(x => x.Storage)
                    .ThenInclude(x => x.Parent)
                    .Include(x => x.Nomenclature)
                    .ThenInclude(x => x.Provider)
                .ToList();
            
            // SORTING
            if (string.IsNullOrEmpty(newSortOrder))
            {
                newSortOrder = "vendorCode";
            }
            else if (newSortOrder == oldSortOrder)
            {
                newSortOrder += "_desc";
            }
            ViewBag.CurrentSort = newSortOrder;
            stocks = SortingStock(newSortOrder, stocks);
            int pageNumber = (page ?? 1);
            ViewBag.Stocks = stocks.ToPagedList(pageNumber, pageSizeStocks);

            var provider = db.Providers.FirstOrDefault(x => x.Id == id);
            return View(provider);
        }

        // EDIT 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditProvider(int providerId, string oldSortOrder, string newSortOrder, string editName, string editAddress, string editEDRPOU,  int? page)
        {
            try
            {
                List<Stock> stocks = db.Stocks
                    .Where(x => x.Nomenclature != null && x.Nomenclature.Provider != null && x.Nomenclature.Provider.Id == providerId)
                    .Include(x => x.Storage)
                    .ThenInclude(x =>x.Parent)
                    .Include(x => x.Nomenclature)
                    .ThenInclude(x => x.Provider)
                    .ToList();

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
                stocks = SortingStock(newSortOrder, stocks);
                int pageNumber = (page ?? 1);
                ViewBag.Stocks = stocks;

                var provider = db.Providers.FirstOrDefault(x => x.Id == providerId);
                provider.Title = editName;
                provider.Address = editAddress;
                provider.EDRPOU = editEDRPOU;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = providerId, oldSortOrder, newSortOrder, page});
            }
            catch
            {
                return RedirectToAction("Details", new { id = providerId, oldSortOrder, newSortOrder, page });
            }
        }
    }
}
