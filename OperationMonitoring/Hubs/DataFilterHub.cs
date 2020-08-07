using Castle.Core.Internal;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using OperationMonitoring.Data;
using OperationMonitoring.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace OperationMonitoring.Hubs
{
    public class DataFilterHub : Hub
    {
        ApplicationContext db;
        private List<Storage> childrenStorages = new List<Storage>();
        private List<Stock> SelectedStocks = new List<Stock>();

        public DataFilterHub(ApplicationContext context)
        {
            Console.WriteLine("Hub created");
            db = context;
        }

        public async Task SendCounterparties(string searchString, string sortField, bool isAscendingSort)
        {
            List<Counterparty> counterparties = db.Counterparties.ToList();
            if (!searchString.IsNullOrEmpty())
            {
                counterparties = await SearchCounterparty(searchString, counterparties);
            }
            if (!sortField.IsNullOrEmpty())
            {
                counterparties = await SortCounterparties(sortField, isAscendingSort, counterparties);
            }
            var json = JsonConvert.SerializeObject(counterparties);
            await Clients.Caller.SendAsync("Receive", json);
        }

        public async Task<List<Counterparty>> SearchCounterparty(string counterpartyName, List<Counterparty> counterparties)
        {
            counterparties = await counterparties.Where(x => x.Title.ToLower().Contains(counterpartyName.ToLower())).ToListAsync();
            return counterparties;
        }

        public async Task<List<Counterparty>> SortCounterparties(string sortField, bool isAscending, List<Counterparty> counterparties)
        {
            switch (isAscending)
            {
                case true:
                    switch (sortField)
                    {
                        case "Id":
                            counterparties = counterparties.OrderBy(x => x.Id).ToList();
                            break;
                        case "Title":
                            counterparties = counterparties.OrderBy(x => x.Title).ToList();
                            break;
                        default:
                            break;
                    }
                    break;
                case false:
                    switch (sortField)
                    {
                        case "Id":
                            counterparties = counterparties.OrderByDescending(x => x.Id).ToList();
                            break;
                        case "Title":
                            counterparties = counterparties.OrderByDescending(x => x.Title).ToList();
                            break;
                        default:
                            break;
                    }
                    break;
                default:
            }
            return counterparties;
        }

        public async Task SendProviders(string searchString, string searchField, bool isAscendingSort)
        {
            List<Provider> providers = db.Providers.ToList();
            if (!searchString.IsNullOrEmpty() && !searchField.IsNullOrEmpty())
            {
                providers = await SearchProvider(searchString, searchField, providers);
            }
                providers = await SortProviders(isAscendingSort, providers);
            var json = JsonConvert.SerializeObject(providers);
            await Clients.Caller.SendAsync("Receive", json);
        }

        public async Task<List<Provider>> SearchProvider(string searchString, string searchField, List<Provider> providers)
        {
            switch (searchField)
            {
                case "Title":
                    providers = providers.Where(x => x.Title.ToLower().Contains(searchString.ToLower())).ToList();
                    break;
                case "Address":
                    providers = providers.Where(x => x.Address.ToLower().Contains(searchString.ToLower())).ToList();
                    break;
                case "EDRPOU":
                    providers = providers.Where(x => x.EDRPOU.ToLower().Contains(searchString.ToLower())).ToList();
                    break;
                default:
                    break;
            }
            return providers;
        }

        public async Task<List<Provider>> SortProviders(bool isAscending, List<Provider> providers)
        {
            switch (isAscending)
            {
                case true:
                    providers = providers.OrderBy(x => x.Title).ToList();
                    break;
                case false:
                    providers = providers.OrderByDescending(x => x.Title).ToList();
                    break;
            }
            return providers;
        }

        public async Task SendNomenclature(string searchString, string searchField, string sortField, bool isAscendingSort)
        {
            List<Nomenclature> nomenclature = db.Nomenclatures
                .Include(x => x.Provider)
                .Include(x => x.Specification)
                .ToList();
            if (!searchString.IsNullOrEmpty() && !searchField.IsNullOrEmpty())
            {
                nomenclature = await SearchNomenclature(searchString, searchField, nomenclature);
            }
            if (!sortField.IsNullOrEmpty())
            {
                nomenclature = await SortNomenclature(sortField, isAscendingSort, nomenclature);
            }
            var json = JsonConvert.SerializeObject(nomenclature);
            await Clients.Caller.SendAsync("Receive", json);
        }

        public async Task<List<Nomenclature>> SearchNomenclature(string searchString, string searchField, List<Nomenclature> nomenclature)
        {
            switch (searchField)
            {
                case "VendorCode":
                    nomenclature = await nomenclature.Where(x => x.VendorCode.ToLower().Contains(searchString.ToLower())).ToListAsync();
                    break;
                case "Title":
                    nomenclature = await nomenclature.Where(x => x.Title.ToLower().Contains(searchString.ToLower())).ToListAsync();
                    break;
                case "Provider":
                    nomenclature = await nomenclature.Where(x => x.Provider.Title.ToLower().Contains(searchString.ToLower())).ToListAsync();
                    break;
                default:
                    break;
            }
            return nomenclature;
        }

        public async Task<List<Nomenclature>> SortNomenclature(string sortField, bool isAscending, List<Nomenclature> nomenclature)
        {
            switch (isAscending)
            {
                case true:
                    switch (sortField)
                    {
                        case "VendorCode":
                            nomenclature = nomenclature.OrderBy(x => x.VendorCode).ToList();
                            break;
                        case "Title":
                            nomenclature = nomenclature.OrderBy(x => x.Title).ToList();
                            break;
                        case "Provider":
                            nomenclature = nomenclature.OrderBy(x => x.Provider.Title).ToList();
                            break;
                        default:
                            break;
                    }
                    break;
                case false:
                    switch (sortField)
                    {
                        case "VendorCode":
                            nomenclature = nomenclature.OrderByDescending(x => x.VendorCode).ToList();
                            break;
                        case "Title":
                            nomenclature = nomenclature.OrderByDescending(x => x.Title).ToList();
                            break;
                        case "Provider":
                            break;
                        default:
                            nomenclature = nomenclature.OrderByDescending(x => x.Provider.Title).ToList();
                            break;
                    }
                    break;
                default:
            }
            return nomenclature;
        }

        public async Task SendOrders(string searchString, string searchField, bool searchOnlyActive, string sortField, bool isAscendingSort)
        {
            List<Order> orders = db.Orders.Include(x => x.Equipment)
                .Include(x => x.Well)
                .Include(x => x.Agreement).ThenInclude(x => x.Counterparty)
                .ToList();
            if (!searchString.IsNullOrEmpty() && !searchField.IsNullOrEmpty())
            {
                orders = await SearchOrders(searchString, searchField, searchOnlyActive, orders);
            }
            if (!sortField.IsNullOrEmpty())
            {
                orders = await SortOrders(sortField, isAscendingSort, orders);
            }
            var json = JsonConvert.SerializeObject(orders);
            await Clients.Caller.SendAsync("Receive", json);
        }

        public async Task<List<Order>> SearchOrders(string searchString, string searchField, bool searchOnlyActive, List<Order> orders)
        {
            switch (searchField)
            {
                case "Counterparty":
                    orders = orders.Where(x => x.Agreement.Counterparty.Title.ToLower().Contains(searchString.ToLower())).ToList();
                    break;
                case "Agreement":
                    orders = orders.Where(x => x.Agreement.AgreementNumber.ToLower().Contains(searchString.ToLower())).ToList();
                    break;
                case "Well":
                    orders = orders.Where(x => x.Well.Title.ToLower().Contains(searchString.ToLower())).ToList();
                    break;
                default:
                    break;
            }
            if (searchOnlyActive == true)
            {
                orders = orders.Where(x => x.IsOpen == true).ToList();
            }
            else
            {
                orders = orders.Where(x => x.IsOpen == false).ToList();
            }
            return orders;
        }

        public async Task<List<Order>> SortOrders(string sortField, bool isAscending, List<Order> orders)
        {
            switch (isAscending)
            {
                case true:
                    switch (sortField)
                    {
                        case "Id":
                            orders = orders.OrderBy(x => x.Id).ToList();
                            break;
                        case "Counterparty":
                            orders = orders.OrderBy(x => x.Agreement.Counterparty.Title).ToList();
                            break;
                        case "Agreement":
                            orders = orders.OrderBy(x => x.Agreement.AgreementNumber).ToList();
                            break;
                        case "Well":
                            orders = orders.OrderBy(x => x.Well.Title).ToList();
                            break;
                        default:
                            break;
                    }
                    break;
                case false:
                    switch (sortField)
                    {
                        case "Id":
                            orders = orders.OrderByDescending(x => x.Id).ToList();
                            break;
                        case "Counterparty":
                            orders = orders.OrderByDescending(x => x.Agreement.Counterparty.Title).ToList();
                            break;
                        case "Agreement":
                            orders = orders.OrderByDescending(x => x.Agreement.AgreementNumber).ToList();
                            break;
                        case "Well":
                            orders = orders.OrderByDescending(x => x.Well.Title).ToList();
                            break;
                        default:
                            break;
                    }
                    break;
                default:
            }
            return orders;
        }

        public async Task SendMaintenances(string sortField, bool isAscendingSort)
        {
            List<Maintenance> maintenances = db.Maintenances
                .Include(x => x.MaintenanceCategory)
                .Include(x => x.MaintenanceType)
                .Include(x => x.Responsible)
                .Include(x => x.ReturnStorage)
                .Include(x => x.Counterparty)
                .Include(x => x.Equipment)
                .ToList();
            if (!sortField.IsNullOrEmpty())
            {
                maintenances = await SortMaintenances(sortField, isAscendingSort, maintenances);
            }
            var json = JsonConvert.SerializeObject(maintenances);
            await Clients.Caller.SendAsync("Receive", json);
        }

        public async Task<List<Maintenance>> SortMaintenances(string sortField, bool isAscending, List<Maintenance> maintenances)
        {
            switch (isAscending)
            {
                case true:
                    switch (sortField)
                    {
                        case "Id":
                            maintenances = maintenances.OrderBy(x => x.Id).ToList();
                            break;
                        case "Status":
                            maintenances = maintenances.OrderBy(x => x.IsOpened).ToList();
                            break;
                        case "DateStart":
                            maintenances = maintenances.OrderBy(x => x.StartDate).ToList();
                            break;
                        case "DateFinish":
                            maintenances = maintenances.OrderBy(x => x.FinishDate).ToList();
                            break;
                        default:
                            break;
                    }
                    break;
                case false:
                    switch (sortField)
                    {
                        case "Id":
                            maintenances = maintenances.OrderByDescending(x => x.Id).ToList();
                            break;
                        case "Status":
                            maintenances = maintenances.OrderByDescending(x => x.IsOpened).ToList();
                            break;
                        case "DateStart":
                            maintenances = maintenances.OrderByDescending(x => x.StartDate).ToList();
                            break;
                        case "DateFinish":
                            maintenances = maintenances.OrderByDescending(x => x.FinishDate).ToList();
                            break;
                        default:
                            break;
                    }
                    break;
                default:
            }
            return maintenances;
        }

        public async Task SendEquipment(string searchStatus, string searchString, string searchField, string sortField, bool isAscendingSort)
        {
            List<Equipment> equipment = db.Equipment
                .Include(x => x.Status)
                .Include(x => x.Category)
                .Include(x => x.Type)
                .ToList();
            if (!searchString.IsNullOrEmpty() && !searchField.IsNullOrEmpty())
            {
                equipment = SearchEquipment(searchStatus, searchString, searchField, equipment).Result;
            }
            var json = JsonConvert.SerializeObject(equipment);
            await Clients.Caller.SendAsync("Receive", json);
        }

        public async Task<List<Equipment>> SearchEquipment(string searchStatus, string searchString, string searchField, List<Equipment> equipment)
        {
            int result;
            bool isInt = int.TryParse(searchString, out result);
            switch (searchField)
            {
                case "Department":
                    equipment = equipment.Where(x => x.Department.Title.Contains(searchString)).ToList();
                    break;
                case "Category":
                    equipment = equipment.Where(x => x.Category.Title.Contains(searchString)).ToList();
                    break;
                case "Type":
                    equipment = equipment.Where(x => x.Type.Title.Contains(searchString)).ToList();
                    break;
                case "Title":
                    equipment = equipment.Where(x => x.Title.Contains(searchString)).ToList();
                    break;
                case "SerialNum":
                    equipment = equipment.Where(x => x.SerialNum.Contains(searchString)).ToList();
                    break;
                case "DiameterOuter":
                    if (isInt)
                        equipment = equipment.Where(x => x.DiameterOuter == result).ToList();
                    else
                        equipment = new List<Equipment>();
                    break;
                case "DiameterInner":
                    if (isInt)
                        equipment = equipment.Where(x => x.DiameterInner == result).ToList();
                    else
                        equipment = new List<Equipment>();
                    break;
                case "Length":
                    if (isInt)
                        equipment = equipment.Where(x => x.Length == result).ToList();
                    else
                        equipment = new List<Equipment>();
                    break;
                default:
                    break;
            }

            int status = -1;
            if (!string.IsNullOrEmpty(searchStatus))
            {
                if (!int.TryParse(searchStatus, out status))
                {
                    status = -1;
                }
            }
            if (status != -1)
            {
                equipment = await equipment.Where(x => x.Status.Id == status).ToListAsync();
            }
            return equipment;
        }

        public async Task<List<Equipment>> SortEquipment(string sortField, bool isAscending, List<Equipment> equipment)
        {
            switch (isAscending)
            {
                case true:
                    switch (sortField)
                    {
                        case "Id":
                            equipment = equipment.OrderBy(x => x.Id).ToList();
                            break;
                        case "Status":
                            equipment = equipment.OrderBy(x => x.Status.Title).ToList();
                            break;
                        case "Department":
                            equipment = equipment.OrderBy(x => x.Department.Title).ToList();
                            break;
                        case "Category":
                            equipment = equipment.OrderBy(x => x.Category.Title).ToList();
                            break;
                        case "Type":
                            equipment = equipment.OrderBy(x => x.Type.Title).ToList();
                            break;
                        case "Title":
                            equipment = equipment.OrderBy(x => x.Title).ToList();
                             break;
                        case "DiameterOuter":
                            equipment = equipment.OrderBy(x => x.DiameterOuter).ToList();
                            break;
                        case "DiameterInner":
                            equipment = equipment.OrderBy(x => x.DiameterInner).ToList();
                            break;
                        case "Length":
                            equipment = equipment.OrderBy(x => x.Length).ToList();
                            break;
                        default:
                            break;
                    }
                    break;
                case false:
                    switch (sortField)
                    {
                        case "Id":
                            equipment = equipment.OrderByDescending(x => x.Id).ToList();
                            break;
                        case "Status":
                            equipment = equipment.OrderByDescending(x => x.Status.Title).ToList();
                            break;
                        case "Department":
                            equipment = equipment.OrderByDescending(x => x.Department.Title).ToList();
                            break;
                        case "Category":
                            equipment = equipment.OrderByDescending(x => x.Category.Title).ToList();
                            break;
                        case "Type":
                            equipment = equipment.OrderByDescending(x => x.Type.Title).ToList();
                            break;
                        case "Title":
                            equipment = equipment.OrderByDescending(x => x.Title).ToList();
                            break;
                        case "DiameterOuter":
                            equipment = equipment.OrderByDescending(x => x.DiameterOuter).ToList();
                            break;
                        case "DiameterInner":
                            equipment = equipment.OrderByDescending(x => x.DiameterInner).ToList();
                            break;
                        case "Length":
                            equipment = equipment.OrderByDescending(x => x.Length).ToList();
                            break;
                        default:
                            break;
                    }
                    break;
                default:
            }
            return equipment;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="storageId"></param>
        /// <param name="searchField"></param>
        /// <param name="searchedObjType">Номенклатура, оборудование или же детали оборудования</param>
        /// <returns></returns>
        public async Task SendStocks(string searchString, int storageId, string searchField, string searchedObjType)
        {
            List<Stock> stocks = new List<Stock>();
            stocks = db.Stocks
                .Include(x => x.Storage)
                .Include(x => x.Nomenclature).ThenInclude(x => x.Provider)
                .Include(x => x.Part).ThenInclude(x => x.Status)
                .Include(x => x.Equipment).ThenInclude(x => x.Status)
                .Where(x => x.Amount > 0)
                .ToList();
            stocks = SearchStocks(searchString, storageId, searchField, searchedObjType, stocks).Result;
            var json = JsonConvert.SerializeObject(stocks);
            await Clients.All.SendAsync("Receive", json);
        }

        public async Task SendSelectedStocks()
        {
            var json = JsonConvert.SerializeObject(Context.Items["SelectedStocks"]);
            await Clients.Caller.SendAsync("RecieveSelectedStocks", json);
        }

        public Stock GetStock(int id)
        {
            Stock stock = db.Stocks
                .Include(x => x.Storage)
                .Include(x => x.Nomenclature).ThenInclude(x => x.Provider)
                .Include(x => x.Part).ThenInclude(x => x.Status)
                .Include(x => x.Equipment).ThenInclude(x => x.Status)
                .FirstOrDefault(x => x.Id == id);
            return stock;
        }

        public async Task AddSelectedStock(int stockId)
        {
            var stock = GetStock(stockId);
            if (Context.Items["SelectedStocks"] != null)
            {
                var temp = Context.Items["SelectedStocks"] as List<Stock>;
                SelectedStocks = temp;
                Context.Items.Remove("SelectedStocks");
            }
            SelectedStocks.Add(stock);
            Context.Items.Add("SelectedStocks", SelectedStocks);
            await SendSelectedStocks();
        }

        public async Task RemoveSelectedStock(int stockId)
        {
           
            if (Context.Items["SelectedStocks"] != null)
            {
                var temp = Context.Items["SelectedStocks"] as List<Stock>;
                SelectedStocks = temp;
                Context.Items.Remove("SelectedStocks");
            }
            Stock removeStock = SelectedStocks.FirstOrDefault(x => x.Id == stockId);
            if(removeStock!=null) SelectedStocks.Remove(removeStock);
            Context.Items.Add("SelectedStocks", SelectedStocks);
            await SendSelectedStocks();
        }

        public async Task<List<Storage>> FindStorageChildren(int storageId)
        {
            List<Storage> Storages = db.Storages
                .Include(x => x.Parent).ThenInclude(x => x.Parent)
                .ToList();
            foreach (var storage in Storages)
            {
                if (storage.Parent != null)
                {
                    if (storage.Parent.Id == storageId)
                    {
                        childrenStorages.Add(storage);
                        await FindStorageChildren(storage.Id);
                    }
                }
            }
            return childrenStorages;
        }

        public async Task<List<Stock>> SearchStocks(string searchString, int storageId, string searchField, string searchedObjType, List<Stock> stocks)
        {
            List<Stock> temp = new List<Stock>();
            if (storageId != 0)
            {
                childrenStorages = new List<Storage>();
                childrenStorages.Add(db.Storages.FirstOrDefault(x => x.Id == storageId));
                childrenStorages = await FindStorageChildren(storageId);
                foreach(var child in childrenStorages)
                {
                    temp.AddRange(stocks.Where(x => x.Storage.Id == child.Id && (x.Amount > 0 || x.Part.Amount > 0)).ToList());
                }
            }
            else
            {
                temp = stocks;
            }
            if(searchedObjType != "All")
            {
                switch (searchedObjType)
                {
                    case "Equipment":
                        temp = temp.Where(x => x.Equipment != null).ToList();
                        break;
                    case "Parts":
                        temp = temp.Where(x => x.Part != null).ToList();
                        break;
                    case "Nomenclature":
                        temp = temp.Where(x => x.Nomenclature != null).ToList();
                        break;
                    default:
                        break;
                }
            }
            if (string.IsNullOrEmpty(searchString) == false)
            {
                switch (searchField)
                {
                    case "Title":
                        temp = temp.Where(x =>
                           x.Nomenclature.Title.Contains(searchString)
                        || x.Equipment.Title.Contains(searchString)
                        || x.Part.Title.Contains(searchString)).ToList();
                        break;
                    default:
                        break;
                }
            }
            return temp;
        }

        public void WriteTransferHistory(Stock stock, Storage importStorage, string message)
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

        public async Task ImportStock(Stock stock, Storage importStorage, string stockType)
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

        public async Task WriteOffStock(Stock stock, string message)
        {
            Stock dbStock = await db.Stocks.FirstOrDefaultAsync(x => x.Id == stock.Id);
            dbStock.Amount -= stock.Amount;
            WriteTransferHistory(stock, null, message);
        }

        public async Task TranserStock(int importStorageId, string jsonStocks)
        {
            List<Stock> stocks = JsonConvert.DeserializeObject<List<Stock>>(jsonStocks);
            Storage importStorage = await db.Storages.FirstOrDefaultAsync(x => x.Id == importStorageId);
            if(importStorage != null)
            {
                foreach (var stock in stocks)
                {
                    await WriteOffStock(stock, "Stock was written off");
                    if (stock.Nomenclature != null)
                    {
                        await ImportStock(stock, importStorage, "Nomenclature");
                    }
                    else if(stock.Part != null)
                    {
                        await ImportStock(stock, importStorage, "Part");
                    }
                    else if (stock.Equipment != null)
                    {
                        await ImportStock(stock, importStorage, "Equipment");
                    }
                }
            }
        }

        public async Task AssembleEquipment(string jsonStocks, int assemblyId)
        {
            List<Stock> stocks = JsonConvert.DeserializeObject<List<Stock>>(jsonStocks);
            foreach(var stock in stocks)
            {
                var message = "Stock was sended on assembly #" + assemblyId.ToString("D8");
                await WriteOffStock(stock, message);
            }
        }

    }
}
