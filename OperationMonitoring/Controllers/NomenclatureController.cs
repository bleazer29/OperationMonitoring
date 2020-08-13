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
        public NomenclatureController(ApplicationContext context)
        {
            db = context;   
        }

        // GET: Controller
        public ActionResult Index()
        {
            var nomenclature = db.Nomenclatures
                .Include(x => x.Specification)
                .Include(x => x.Provider)
                .OrderBy(x => x.VendorCode)
                .ToList();
            return View(nomenclature);
        }

        // DETAILS 
        [HttpGet("/[controller]/[action]/{id}")]
        public ActionResult Details(int id)
        {
            var providers = db.Providers.ToList();
            ViewBag.Providers = providers;
            var nomenclature = db.Nomenclatures.Include(x => x.Specification).Include(x => x.Provider).FirstOrDefault(x => x.Id == id);
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
                if (nomenclature.Specification == null)
                {
                    Specification spec = new Specification();
                    if (editHeight != null) spec.Height = (double)editHeight;
                    if (editWeight != null) spec.Weight = (double)editWeight;
                    if (editOperatingTime != null) spec.OperatingTime = (int)editOperatingTime;
                    spec.Material = editMaterial;
                    await db.Specifications.AddAsync(spec);
                    nomenclature.Specification = spec;
                }
                else
                {
                    if (editHeight != null) nomenclature.Specification.Height = (double)editHeight;
                    if (editWeight != null) nomenclature.Specification.Weight = (double)editWeight;
                    if (editOperatingTime != null) nomenclature.Specification.OperatingTime = (int)editOperatingTime;
                    nomenclature.Specification.Material = editMaterial;
                }
                int providerId = int.Parse(provider);
                if (providerId == -1)
                {
                    Provider pr = new Provider() { Title = providerName, Address = providerAddress, EDRPOU = providerEDRPOU };
                    await db.Providers.AddAsync(pr);
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
