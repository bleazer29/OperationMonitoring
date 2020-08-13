using Castle.Core.Internal;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using OperationMonitoring.Data;
using OperationMonitoring.Models;
using OperationMonitoring.Models.Interfaces;
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

        /// <summary>
        /// COUNTERPARTIES
        /// </summary>
        public async Task SendCounterparties(string searchString, string sortField, bool isAscendingSort)
        {
            List<Counterparty> counterparties = db.Counterparties.ToList();
            if (!searchString.IsNullOrEmpty())
            {
                counterparties = await SearchCounterparty(searchString.ToLower(), counterparties);
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
            counterparties = await counterparties.Where(x => x.Title.ToLower().Contains(counterpartyName)).ToListAsync();
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
                            counterparties = await counterparties.OrderBy(x => x.Id).ToListAsync();
                            break;
                        case "Title":
                            counterparties = await counterparties.OrderBy(x => x.Title).ToListAsync();
                            break;
                        default:
                            break;
                    }
                    break;
                case false:
                    switch (sortField)
                    {
                        case "Id":
                            counterparties = await counterparties.OrderByDescending(x => x.Id).ToListAsync();
                            break;
                        case "Title":
                            counterparties = await counterparties.OrderByDescending(x => x.Title).ToListAsync();
                            break;
                        default:
                            break;
                    }
                    break;
                default:
            }
            return counterparties;
        }

        /// <summary>
        /// PROVIDERS
        /// </summary>
        public async Task SendProviders(string searchString, string searchField, bool isAscendingSort)
        {
            List<Provider> providers = db.Providers.ToList();
            if (!searchString.IsNullOrEmpty() && !searchField.IsNullOrEmpty())
            {
                providers = await SearchProvider(searchString.ToLower(), searchField, providers);
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
                    providers = await providers.Where(x => x.Title.ToLower().Contains(searchString)).ToListAsync();
                    break;
                case "Address":
                    providers = await providers.Where(x => x.Address.ToLower().Contains(searchString)).ToListAsync();
                    break;
                case "EDRPOU":
                    providers = await providers.Where(x => x.EDRPOU.ToLower().Contains(searchString)).ToListAsync();
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
                    providers = await providers.OrderBy(x => x.Title).ToListAsync();
                    break;
                case false:
                    providers = await providers.OrderByDescending(x => x.Title).ToListAsync();
                    break;
            }
            return providers;
        }

        /// <summary>
        /// NOMENCLATURE
        /// </summary>
        public async Task SendNomenclature(string searchString, string searchField, string sortField, bool isAscendingSort)
        {
            List<Nomenclature> nomenclature = db.Nomenclatures
                .Include(x => x.Provider)
                .Include(x => x.Specification)
                .ToList();
            if (!searchString.IsNullOrEmpty() && !searchField.IsNullOrEmpty())
            {
                nomenclature = await SearchNomenclature(searchString.ToLower(), searchField, nomenclature);
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
                    nomenclature = await nomenclature.Where(x => x.VendorCode.ToLower().Contains(searchString)).ToListAsync();
                    break;
                case "Title":
                    nomenclature = await nomenclature.Where(x => x.Title.ToLower().Contains(searchString)).ToListAsync();
                    break;
                case "Provider":
                    nomenclature = await nomenclature.Where(x => x.Provider.Title.ToLower().Contains(searchString)).ToListAsync();
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
                            nomenclature = await nomenclature.OrderBy(x => x.VendorCode).ToListAsync();
                            break;
                        case "Title":
                            nomenclature = await nomenclature.OrderBy(x => x.Title).ToListAsync();
                            break;
                        case "Provider":
                            nomenclature = await nomenclature.OrderBy(x => x.Provider.Title).ToListAsync();
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

        /// <summary>
        /// ORDERS
        /// </summary>
        public async Task SendOrders(string searchString, string searchField, bool searchOnlyActive, string sortField, bool isAscendingSort)
        {
            List<Order> orders = db.Orders.Include(x => x.Equipment)
                .Include(x => x.Well)
                .Include(x => x.Agreement).ThenInclude(x => x.Counterparty)
                .ToList();
            if (!searchString.IsNullOrEmpty() && !searchField.IsNullOrEmpty())
            {
                orders = await SearchOrders(searchString.ToLower(), searchField, searchOnlyActive, orders);
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
                    orders = await orders.Where(x => x.Agreement.Counterparty.Title.ToLower().Contains(searchString)).ToListAsync();
                    break;
                case "Agreement":
                    orders = orders.Where(x => x.Agreement.AgreementNumber.ToLower().Contains(searchString)).ToList();
                    break;
                case "Well":
                    orders = orders.Where(x => x.Well.Title.ToLower().Contains(searchString)).ToList();
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
                            orders = await orders.OrderBy(x => x.Id).ToListAsync();
                            break;
                        case "Counterparty":
                            orders = await orders.OrderBy(x => x.Agreement.Counterparty.Title).ToListAsync();
                            break;
                        case "Agreement":
                            orders = await orders.OrderBy(x => x.Agreement.AgreementNumber).ToListAsync();
                            break;
                        case "Well":
                            orders = await orders.OrderBy(x => x.Well.Title).ToListAsync();
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

        /// <summary>
        /// MAINTENANCE
        /// </summary>
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

        /// <summary>
        /// EQUIPMENT
        /// </summary>
        public async Task SendEquipment(string searchStatus, string searchString, string searchField, string sortField, bool isAscendingSort)
        {
            List<Equipment> equipment = db.Equipment
                .Include(x => x.Department)
                .Include(x => x.Status)
                .Include(x => x.Category)
                .Include(x => x.Type)
                .ToList();
            if (!searchString.IsNullOrEmpty() || !searchField.IsNullOrEmpty())
            {
                equipment = SearchEquipment(searchStatus, searchString, searchField, equipment).Result;
            }
            if (!sortField.IsNullOrEmpty())
            {
                equipment = SortEquipment(sortField, isAscendingSort, equipment).Result;
            }
            var json = JsonConvert.SerializeObject(equipment);
            await Clients.Caller.SendAsync("Receive", json);
        }
        public async Task<List<Equipment>> SearchEquipment(string searchStatus, string searchString, string searchField, List<Equipment> equipment)
        {
            int result;
            bool isInt = int.TryParse(searchString, out result);
            if (searchString.IsNullOrEmpty())
            {
                searchString = "";
            }
            searchString = searchString.ToLower();
            switch (searchField)
            {
                case "Department":
                    equipment = equipment.Where(x => x.Department.Title.ToLower().Contains(searchString)).ToList();
                    break;
                case "Category":
                    equipment = equipment.Where(x => x.Category.Title.ToLower().Contains(searchString)).ToList();
                    break;
                case "Type":
                    equipment = equipment.Where(x => x.Type.Title.ToLower().Contains(searchString)).ToList();
                    break;
                case "Title":
                    equipment = equipment.Where(x => x.Title.ToLower().Contains(searchString)).ToList();
                    break;
                case "SerialNumber":
                    equipment = equipment.Where(x => x.SerialNum.ToLower().Contains(searchString)).ToList();
                    break;
                case "InventoryNumber":
                    equipment = equipment.Where(x => x.InventoryNum.ToLower().Contains(searchString)).ToList();
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
        /// STOCKS
        /// </summary>
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
            if (searchString.IsNullOrEmpty() == false)
            {
                searchString = searchString.ToLower();
                switch (searchField)
                {
                    case "Title":
                        temp = temp.Where(x =>
                           x.Nomenclature.Title.ToLower().Contains(searchString)
                        || x.Equipment.Title.ToLower().Contains(searchString)
                        || x.Part.Title.ToLower().Contains(searchString)).ToList();
                        break;
                    default:
                        break;
                }
            }
            return temp;
        }

        /// <summary>
        /// DEPARTMENTS
        /// </summary>
        public async Task SendDepartments(string searchString, string searchField, bool isAscendedSort)
        {
            List<Department> departments = db.Departments.ToList();
            if (!string.IsNullOrEmpty(searchString))
            {
                departments = await SearchDepartments(searchString.ToLower(), searchField, departments);
            }
            departments = await SortDepartment(isAscendedSort, departments);
            var json = JsonConvert.SerializeObject(departments);
            await Clients.Caller.SendAsync("Receive", json);
        }

        public async Task<List<Department>> SearchDepartments(string searchString, string searchField, List<Department> departments)
        {
            switch (searchField)
            {
                case "Title":
                    departments = departments.Where(x => x.Title.ToLower().Contains(searchString)).ToList();
                    break;
                case "Address":
                    departments = departments.Where(x => x.Address.Contains(searchString)).ToList();
                    break;
            }
            return departments;
        }

        public async Task<List<Department>> SortDepartment(bool isAscendSort, List<Department> departments)
        {
            switch (isAscendSort)
            {
                case true:
                    departments = departments.OrderBy(x => x.Title).ToList();
                    break;
                case false:
                    departments = departments.OrderByDescending(x => x.Title).ToList();
                    break;
            }
            return departments;
        }

        /// <summary>
        /// NRI - GENERAL
        /// </summary>
        public async Task SendEquipmentTypes(string searchString, bool isAscendedSort)
        {
            var data = db.EquipmentTypes.ToList();
            var types = data.Cast<INri>().ToList();
            if (!searchString.IsNullOrEmpty())
            {
                types = await SearchNRI(searchString.ToLower(), types);
            }
            types = await SortNRI(isAscendedSort, types);
            var json = JsonConvert.SerializeObject(types);
            await Clients.Caller.SendAsync("Receive", json);
        }
        public async Task SendEquipmentCategories(string searchString, bool isAscendedSort)
        {
            var data = db.EquipmentCategories.ToList();
            var categories = data.Cast<INri>().ToList();
            if (!searchString.IsNullOrEmpty())
            {
                categories = await SearchNRI(searchString.ToLower(), categories);
            }
            categories = await SortNRI(isAscendedSort, categories);
            var json = JsonConvert.SerializeObject(categories);
            await Clients.Caller.SendAsync("Receive", json);
        }
        public async Task SendMaintenanceTypes(string searchString, bool isAscendedSort)
        {
            var data = db.MaintenanceTypes.ToList();
            var types = data.Cast<INri>().ToList();
            if (!searchString.IsNullOrEmpty())
            {
                types = await SearchNRI(searchString.ToLower(), types);
            }
            types = await SortNRI(isAscendedSort, types);
            var json = JsonConvert.SerializeObject(types);
            await Clients.Caller.SendAsync("Receive", json);
        }
        public async Task SendMaintenanceCategories(string searchString, bool isAscendedSort)
        {
            var data = db.MaintenanceCategories.ToList();
            var categories = data.Cast<INri>().ToList(); 
            if (!searchString.IsNullOrEmpty())
            {
                categories = await SearchNRI(searchString.ToLower(), categories);
            }
            categories = await SortNRI(isAscendedSort, categories);
            var json = JsonConvert.SerializeObject(categories);
            await Clients.Caller.SendAsync("Receive", json);
        }
        public async Task SendPosition(string searchString, bool isAscendedSort)
        {
            var data = db.Positions.ToList();
            var positions = data.Cast<INri>().ToList();
            if (!searchString.IsNullOrEmpty())
            {
                positions = await SearchNRI(searchString.ToLower(), positions);
            }
            positions = await SortNRI(isAscendedSort, positions);
            var json = JsonConvert.SerializeObject(positions);
            await Clients.Caller.SendAsync("Receive", json);
        }
        public async Task<List<INri>> SearchNRI(string searchString, List<INri> nri)
        {
            nri = await nri.Where(x => x.Title.ToLower().Contains(searchString)).ToListAsync();
            return nri;
        }
        public async Task<List<INri>> SortNRI(bool isAscendSort, List<INri> nri)
        {
            switch (isAscendSort)
            {
                case true:
                    nri = nri.OrderBy(x => x.Title).ToList();
                    break;
                case false:
                    nri = nri.OrderByDescending(x => x.Title).ToList();
                    break;
            }
            return nri;
        }

        /// <summary>
        /// EMPLOYEES
        /// </summary>
        public async Task SendEmployees(string searchString, string searchField, bool searchOnlyActive, string sortField, bool isAscendingSort)
        {
            List<Employee> employees = db.Employees.Include(x => x.Position)
                .ToList();
            if (!searchString.IsNullOrEmpty() && !searchField.IsNullOrEmpty())
            {
                employees = await SearchEmployees(searchString.ToLower(), searchField, employees);
            }
            if (!sortField.IsNullOrEmpty())
            {
                employees = await SortEmployees(sortField, isAscendingSort, employees);
            }
            var json = JsonConvert.SerializeObject(employees);
            await Clients.Caller.SendAsync("Receive", json);
        }

        public async Task<List<Employee>> SearchEmployees(string searchString, string searchField, List<Employee> employees)
        {
            switch (searchField)
            {
                case "Name":
                    employees = await employees.Where(x => x.FullName.ToLower().Contains(searchString)).ToListAsync();
                    break;
                case "Position":
                    employees = await employees.Where(x => x.Position.Title.ToLower().Contains(searchString)).ToListAsync();
                    break;
                default:
                    break;
            }
            return employees;
        }

        public async Task<List<Employee>> SortEmployees(string sortField, bool isAscending, List<Employee> employees)
        {
            switch (isAscending)
            {
                case true:
                    switch (sortField)
                    {
                        case "Id":
                            employees = await employees.OrderBy(x => x.Id).ToListAsync();
                            break;
                        case "Name":
                            employees = await employees.OrderBy(x => x.LastName).ToListAsync();
                            break;
                        case "Position":
                            employees = await employees.OrderBy(x => x.Position.Title).ToListAsync();
                            break;
                        default:
                            break;
                    }
                    break;
                case false:
                    switch (sortField)
                    {
                        case "Id":
                            employees = employees.OrderByDescending(x => x.Id).ToList();
                            break;
                        case "Name":
                            employees = employees.OrderByDescending(x => x.LastName).ToList();
                            break;
                        case "Position":
                            employees = employees.OrderByDescending(x => x.Position.Title).ToList();
                            break;
                        default:
                            break;
                    }
                    break;
                default:
            }
            return employees;
        }
    }
}
