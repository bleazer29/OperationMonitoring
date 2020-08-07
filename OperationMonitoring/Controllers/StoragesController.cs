using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
            ViewBag.TreeViewStorages = treeViewStorages;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string JSONId)
        {
            try
            {
                return RedirectToAction("Transfer", new { st = JSONId });
            }
            catch
            {
                return RedirectToAction("Index");
            }
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

        public ActionResult Transfer(string st)
        {
            List<SelectedStock> selectedStocks = JsonConvert.DeserializeObject<List<SelectedStock>>(st);
            List<Stock> selected = new List<Stock>();

            List<Stock> stocks = db.Stocks.Include(x => x.Nomenclature).ThenInclude(x => x.Provider)
               .Include(x => x.Equipment).ThenInclude(x => x.Status)
               .Include(x => x.Part).ThenInclude(x => x.Status)
               .ToList();

            for (int i = 0; i < selectedStocks.Count; i++)
            {
                Stock stock = stocks.FirstOrDefault(x => x.Id == selectedStocks[i].StockId);
                stock.Amount = selectedStocks[i].Amount;
                if (stock != null) selected.Add(stock);
            }
            ViewBag.Stocks = selected;

            List<Storage> storages = db.Storages.Include(x => x.Parent).ThenInclude(x => x.Parent).ToList();
            List<TreeViewStorage> treeViewStorages = new List<TreeViewStorage>();
            foreach (Storage storage in storages)
            {
                treeViewStorages.Add(new TreeViewStorage(storage));
            }
            ViewBag.TreeViewStorages = treeViewStorages;
            return View();
        }

        private void WriteTransferHistory(Stock stock, Storage importStorage, string message)
        {
            StorageHistory newEntry = new StorageHistory()
            {
                HistoryType = db.HistoryTypes.FirstOrDefault(x => x.Title == "Transportation"),
                Amount = stock.Amount,
                Message = message,
                Stock = stock,
                StorageTo = importStorage,
                Date = DateTime.Now
            };
            db.StorageHistory.AddAsync(newEntry);
        }

        private async Task ImportStock(Stock stock, double amount, Storage importStorage, string stockType)
        {
            Stock importStock = null;
            switch (stockType)
            {
                case "Nomenclature":
                    importStock = await db.Stocks.FirstOrDefaultAsync(x => x.Nomenclature.Id == stock.Nomenclature.Id);
                    break;
                case "Part":
                    importStock = await db.Stocks.FirstOrDefaultAsync(x => x.Part.Id == stock.Part.Id);
                    break;
                case "Equipment":
                    importStock = await db.Stocks.FirstOrDefaultAsync(x => x.Equipment.Id == stock.Equipment.Id);
                    break;
            }
            if (importStock != null)
            {
                importStock.Amount += stock.Amount;
            }
            else
            {
                importStock = new Stock();
                switch (stockType)
                {
                    case "Nomenclature":
                        importStock.Nomenclature = stock.Nomenclature;
                        break;
                    case "Part":
                        importStock.Part = stock.Part;
                        break;
                    case "Equipment":
                        importStock.Equipment = stock.Equipment;
                        break;
                }
                importStock.Amount = stock.Amount;
                importStock.Storage = importStorage;
                db.Stocks.Add(importStock);
            }
            WriteTransferHistory(stock, importStorage, message: "Stock transfered");
            await db.SaveChangesAsync();
        }

        private async Task SubtractStock(int stockId, double amount, string message)
        {
            Stock dbStock = await db.Stocks.FirstOrDefaultAsync(x => x.Id == stockId);
            dbStock.Amount -= amount;
            WriteTransferHistory(dbStock, null, message);
        }

        private async Task TransferStock(int importStorageId, string jsonStocks)
        {
            List<SelectedStock> imports = JsonConvert.DeserializeObject<List<SelectedStock>>(jsonStocks);
            List<Stock> stocks = db.Stocks
                .Include(x => x.Equipment)
                .Include(x => x.Part)
                .Include(x => x.Nomenclature)
                .Where(x => x.Amount > 0)
                .ToList();
            Storage importStorage = await db.Storages.FirstOrDefaultAsync(x => x.Id == importStorageId);
            if (importStorage != null)
            {
                foreach (var import in imports)
                {
                    Stock currentStock = stocks.FirstOrDefault(x => x.Id == import.StockId);
                    await SubtractStock(import.StockId, import.Amount, "Stock was transfered to storage:" + importStorage.Title);

                    if (currentStock.Nomenclature != null)
                    {
                        await ImportStock(currentStock, import.Amount, importStorage, "Nomenclature");
                    }
                    else if (currentStock.Part != null)
                    {
                        await ImportStock(currentStock, import.Amount, importStorage, "Part");
                    }
                    else if (currentStock.Equipment != null)
                    {
                        await ImportStock(currentStock, import.Amount, importStorage, "Equipment");
                    }
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Transfer(int storageId, string stocksJSON)
        {
            try
            {
                await TransferStock(storageId, stocksJSON);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> WriteOff(int stockId, double amount)
        {
            try
            {
                await SubtractStock(stockId, amount, "Stock was written off");
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }


    }
}
