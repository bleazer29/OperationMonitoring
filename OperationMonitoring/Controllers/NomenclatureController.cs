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
    public class NomenclatureController : Controller
    {
        private readonly ApplicationContext db;
        private int pageSize = 10;
        public NomenclatureController(ApplicationContext context)
        {
            db = context;
        }

        // SEARCH
        private List<Nomenclature> Searching(List<Nomenclature> nomenclature, string searchField, string searchString)
        {
            switch (searchField)
            {
                case "Name":
                    nomenclature = nomenclature.Where(x => x.Name != null && x.Name.ToLower().Contains(searchString.ToLower())).ToList();
                    break;
                case "VendorCode":
                    nomenclature = nomenclature.Where(x => x.VendorCode != null && x.VendorCode.ToLower().Contains(searchString.ToLower())).ToList();
                    break;
                case "ProviderName":
                    nomenclature = nomenclature.Where(x => x.Provider != null && x.Provider.Name.ToLower().Contains(searchString.ToLower())).ToList();
                    break;
                default:
                    break;
            }
            return nomenclature;
        }

        // SORTING
        private List<Nomenclature> Sorting(string sortOrder, List<Nomenclature> nomenclature)
        {
            switch (sortOrder)
            {
                case "name_desc":
                    nomenclature = nomenclature.OrderByDescending(s => s.Name).ToList();
                    break;
                case "vendorCode_desc":
                    nomenclature = nomenclature.OrderByDescending(s => s.VendorCode).ToList();
                    break;
                case "vendorCode":
                    nomenclature = nomenclature.OrderBy(s => s.VendorCode).ToList();
                    break;
                case "providerName_desc":
                    nomenclature = nomenclature.OrderByDescending(s => s.Provider.Name).ToList();
                    break;
                case "providerName":
                    nomenclature = nomenclature.OrderBy(s => s.Provider.Name).ToList();
                    break;
                default:
                    nomenclature = nomenclature.OrderBy(s => s.Name).ToList();
                    break;
            }
            return nomenclature;
        }

        // GET: Controller
        public ActionResult Index(string oldSortOrder, string newSortOrder, string searchString, string searchField, int? page)
        {
            ViewBag.CurrentFilter = searchString;
            ViewBag.SearchField = string.IsNullOrEmpty(searchField) ? "Name" : searchField;

            var nomenclature = db.Nomenclatures.Include(x => x.Provider).Include(x => x.Specification).ToList();
            ViewBag.Nomenclature = nomenclature;
            ViewBag.Providers = db.Providers.ToList();
            
            // SEARCH
            nomenclature = Searching(nomenclature, searchField, searchString);

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
            nomenclature = Sorting(newSortOrder, nomenclature);

            if (searchString != null)
            {
                page = 1;
            }
            int pageNumber = (page ?? 1);


            return View(nomenclature.ToPagedList(pageNumber, pageSize));

        }

        // DETAILS 
        [HttpGet("/[controller]/[action]/{id}")]
        public ActionResult Details(int id)
        {
            var nomenclature = db.Nomenclatures.Include(x => x.Provider).Include(x => x.Specification).FirstOrDefault(x => x.Id == id);
            return View(nomenclature);
        }

        // CREATE
        public ActionResult Create()
        {
            ViewBag.Providers = db.Providers.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Nomenclature nomenclature, string provider)
        {
            
            if (ModelState.IsValid)
            {
                int providerId = int.Parse(provider);
                nomenclature.Provider = db.Providers.FirstOrDefault(x => x.Id == providerId);
                db.Nomenclatures.Add(nomenclature);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        // EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditNomenclature(int providerId, string editName, string editAddress, string editEDRPOU)
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
