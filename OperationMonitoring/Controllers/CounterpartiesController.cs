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
    public class CounterpartiesController : Controller
    {
        private readonly ApplicationContext db;
        public CounterpartiesController(ApplicationContext context)
        {
            db = context;
        }

        // SORTING
        private List<Counterparty> Sorting(string sortOrder, List<Counterparty> Counterparties)
        {
            switch (sortOrder)
            {
                case "id_desc":
                    Counterparties = Counterparties.OrderByDescending(s => s.Id).ToList();
                    break;
                case "id":
                    Counterparties = Counterparties.OrderBy(s => s.Id).ToList();
                    break;
                case "name_desc":
                    Counterparties = Counterparties.OrderByDescending(s => s.Title).ToList();
                    break;
                default:
                    Counterparties = Counterparties.OrderBy(s => s.Title).ToList();
                    break;
            }
            return Counterparties;
        }

        // LIST
        public ActionResult Index(string oldSortOrder, string newSortOrder, string searchString, int? page, string currentFilter)
        {
            var Counterparties = db.Counterparties.ToList();

            if (!string.IsNullOrEmpty(searchString))
            {
                Counterparties = Counterparties.Where(s => s.Title.ToLower().Contains(searchString.ToLower())).ToList();
            }

            if (string.IsNullOrEmpty(newSortOrder))
            {
                newSortOrder = "name";
            }
            else if (newSortOrder == oldSortOrder)
            {
                newSortOrder += "_desc";
            }
            ViewBag.CurrentSort = newSortOrder;
            Counterparties = Sorting(newSortOrder, Counterparties);

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            ViewBag.CounterpartyContracts = db.Agreements.ToList();
            ViewBag.CounterpartyWells = db.Wells.ToList();
            return View(Counterparties.ToPagedList(pageNumber, pageSize));
        }

        // DETAILS 
        [HttpGet("/[controller]/[action]/{id}")]
        public ActionResult Details(int id)
        {
            var Counterparty = db.Counterparties.FirstOrDefault(x => x.Id == id);
            ViewBag.CounterpartyContracts = db.Agreements.Where(x => x.Counterparty.Id == Counterparty.Id).ToList();
            ViewBag.CounterpartyWells = db.Wells.Where(x => x.Counterparty.Id == Counterparty.Id).ToList();
            return View(Counterparty);
        }

        // CREATE Counterparty
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Counterparty Counterparty)
        {
            if (ModelState.IsValid)
            {
                db.Counterparties.Add(Counterparty);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        // ADD CONTRACT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddContract(int CounterpartyId, string contractNum, DateTime contractDateStart, DateTime contractDateDue)
        {
            try
            {
                var Counterparty = db.Counterparties.FirstOrDefault(x => x.Id == CounterpartyId);
                db.Agreements.Add(new Agreement() { AgreementNumber = contractNum, Counterparty = Counterparty, DateStart = contractDateStart, DateDue = contractDateDue });
                await db.SaveChangesAsync();
                return RedirectToAction("Details", new { id = CounterpartyId });
            }
            catch
            {
                return RedirectToAction("Details", new { id = CounterpartyId });
            }
        }

        // ADD WELL
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddWell(int CounterpartyId, string title, string location)
        {
            try
            {
                var Counterparty = db.Counterparties.FirstOrDefault(x => x.Id == CounterpartyId);
                db.Wells.Add(new Well() { Title = title, Location = location, Counterparty = Counterparty });
                await db.SaveChangesAsync();
                return RedirectToAction("Details", new { id = CounterpartyId });
            }
            catch
            {
                return RedirectToAction("Details", new { id = CounterpartyId });
            }
        }

        // EDIT CONTRACT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditContract(int CounterpartyId, int contractId, string editContractNum, DateTime editDateStart, DateTime editDateDue)
        {
            try
            {
                var contract = db.Agreements.FirstOrDefault(x => x.Id == contractId);
                contract.AgreementNumber = editContractNum;
                contract.DateStart = editDateStart;
                contract.DateDue = editDateDue;
                await db.SaveChangesAsync();
                return RedirectToAction("Details", new { id = CounterpartyId });
            }
            catch
            {
                return RedirectToAction("Details", new { id = CounterpartyId });
            }
        }

        // EDIT WELL
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditWell(int CounterpartyId, int wellId, string editTitle, string editLocation)
        {
            try
            {
                var well = db.Wells.FirstOrDefault(x => x.Id == wellId);
                well.Title = editTitle;
                well.Location = editLocation;
                await db.SaveChangesAsync();
                return RedirectToAction("Details", new { id = CounterpartyId });
            }
            catch
            {
                return RedirectToAction("Details", new { id = CounterpartyId });
            }
        }

        // EDIT Counterparty
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditCounterparty(int CounterpartyId, string editName)
        {
            try
            {
                var Counterparty = db.Counterparties.FirstOrDefault(x => x.Id == CounterpartyId);
                Counterparty.Title = editName;
                await db.SaveChangesAsync();
                return RedirectToAction("Details", new { id = CounterpartyId });
            }
            catch
            {
                return RedirectToAction("Details", new { id = CounterpartyId });
            }
        }
    }
}
