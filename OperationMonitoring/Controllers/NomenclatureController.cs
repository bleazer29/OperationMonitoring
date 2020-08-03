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
                case "Title":
                    nomenclature = nomenclature.Where(x => x.Title != null && x.Title.ToLower().Contains(searchString.ToLower())).ToList();
                    break;
                case "VendorCode":
                    nomenclature = nomenclature.Where(x => x.VendorCode != null && x.VendorCode.ToLower().Contains(searchString.ToLower())).ToList();
                    break;
                case "Provider":
                    nomenclature = nomenclature.Where(x => x.Provider != null && x.Provider.Title.ToLower().Contains(searchString.ToLower())).ToList();
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
                    nomenclature = nomenclature.OrderByDescending(s => s.Title).ToList();
                    break;
                case "vendorCode_desc":
                    nomenclature = nomenclature.OrderByDescending(s => s.VendorCode).ToList();
                    break;
                case "vendorCode":
                    nomenclature = nomenclature.OrderBy(s => s.VendorCode).ToList();
                    break;
                case "providerName_desc":
                    nomenclature = nomenclature.OrderByDescending(s => s.Provider.Title).ToList();
                    break;
                case "providerName":
                    nomenclature = nomenclature.OrderBy(s => s.Provider.Title).ToList();
                    break;
                default:
                    nomenclature = nomenclature.OrderBy(s => s.Title).ToList();
                    break;
            }
            return nomenclature;
        }

        // GET: Controller
        public ActionResult Index(string oldSortOrder, string newSortOrder, string searchString, string searchField, int? page)
        {
            ViewBag.CurrentFilter = searchString;
            ViewBag.SearchField = string.IsNullOrEmpty(searchField) ? "Title" : searchField;

            var nomenclature = db.Nomenclatures.Include(x => x.Specification).Include(x => x.Provider).ToList();
            ViewBag.Nomenclature = nomenclature;
            var providers = db.Providers.ToList();
            ViewBag.Providers = providers;
            
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
            var providers = db.Providers.ToList();
            ViewBag.Providers = providers;
            var nomenclature =db.Nomenclatures.Include(x => x.Specification).Include(x => x.Provider).FirstOrDefault(x => x.Id == id);
            if (nomenclature.Provider != null) ViewBag.ProviderId = nomenclature.Provider.Id;
            else ViewBag.ProviderId = -1;
            
            return View(nomenclature);
        }

        // CREATE
        public ActionResult Create()
        {
            var providers = db.Providers.ToList();
            ViewBag.Providers = providers;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Nomenclature nomenclature,
            string provider, string providerName, string providerEDRPOU, string providerAddress)
        {
            
            if (ModelState.IsValid)
            {
                int providerId = int.Parse(provider);
                if (providerId == -1)
                {
                    Provider pr = new Provider() { Title = providerName, Address = providerAddress, EDRPOU = providerEDRPOU };
                    db.Providers.Add(pr);
                    nomenclature.Provider = pr;
                }
                else if (providerId != 0)
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
        public async Task<ActionResult> EditNomenclature(int nomenclatureId, string editName, string editVendorCode, string editMaterial, 
            double? editHeight, double? editWeight, int? editOperatingTime, string provider, string providerName, string providerEDRPOU, string providerAddress)
        {
            try
            {
                var nomenclature = db.Nomenclatures.Include(x => x.Specification).Include(x => x.Provider).FirstOrDefault(x => x.Id == nomenclatureId);
                nomenclature.Title = editName;
                nomenclature.VendorCode = editVendorCode;
                if (editHeight !=null) nomenclature.Specification.Height = (double)editHeight;
                if (editWeight != null) nomenclature.Specification.Weight = (double)editWeight;
                if (editOperatingTime != null) nomenclature.Specification.OperatingTime = (int)editOperatingTime;
                nomenclature.Specification.Material = editMaterial;
                int providerId = int.Parse(provider);
                if (providerId == -1)
                {
                    Provider pr = new Provider() { Title = providerName, Address = providerAddress, EDRPOU = providerEDRPOU };
                    db.Providers.Add(pr);
                    nomenclature.Provider = pr;
                }
                else if (providerId != 0) 
                    nomenclature.Provider = db.Providers.FirstOrDefault(x => x.Id == providerId);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = nomenclatureId });
            }
            catch
            {
                return RedirectToAction("Details", new { id = nomenclatureId });
            }
        }
    }
}
