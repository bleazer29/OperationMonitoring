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
        public DataFilterHub(ApplicationContext context)
        {
            db = context;
        }

        public async Task SendCounterparties(string searchString, string sortField, bool isAscendingSort)
        {
            List<Counterparty> counterparties = db.Counterparties.ToList();
            if (!searchString.IsNullOrEmpty())
            {
                counterparties = SearchCounterparty(searchString, counterparties).Result;
            }
            if(!sortField.IsNullOrEmpty())
            {
                counterparties = SortCounterparties(sortField, isAscendingSort, counterparties).Result;
            }
            var json= JsonConvert.SerializeObject(counterparties);
            await Clients.Caller.SendAsync("Receive", json);
        }

        /// <summary>
        /// Counterparty search
        /// </summary>
        /// <param name="counterpartyName"></param>
        /// <returns></returns>
        public async Task<List<Counterparty>> SearchCounterparty(string counterpartyName, List<Counterparty> counterparties)
        {
            counterparties = counterparties.Where(x => x.Title.ToLower().Contains(counterpartyName.ToLower())).ToList();
            return counterparties;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sortField">1 - Id, 2 - Title</param>
        /// <param name="isAscending">ascending or descending sort</param>
        /// <param name="counterparties">list to sort</param>
        /// <returns></returns>
        public async Task<List<Counterparty>> SortCounterparties(string sortField, bool isAscending,  List<Counterparty> counterparties)
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

        public async Task SendProviders(string searchString, string searchField, string sortField, bool isAscendingSort)
        {
            List<Provider> providers = db.Providers.ToList();
            if (!searchString.IsNullOrEmpty() && !searchField.IsNullOrEmpty())
            {
                providers = SearchProvider(searchString, searchField, providers).Result;
            }
            if (!sortField.IsNullOrEmpty())
            {
                providers = SortProviders(sortField, isAscendingSort, providers).Result;
            }
            var json = JsonConvert.SerializeObject(providers);
            await Clients.Caller.SendAsync("Receive", json);
        }

        /// <summary>
        /// Provider search on index page
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="searchField">1 - Search by Name, 2 - Address, 3 - EDRPOU</param>
        /// <returns></returns>
        public async Task<List<Provider>> SearchProvider(string searchString, string searchField, List<Provider> providers)
        {
            switch (searchField)
            {
                case "Name":
                    providers = providers.Where(x => x.Name.ToLower().Contains(searchString.ToLower())).ToList();
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sortField">1 - Name, 2 - Address</param>
        /// <param name="isAscending">ascending or descending sort</param>
        /// <param name="counterparties">list to sort</param>
        /// <returns></returns>
        public async Task<List<Provider>> SortProviders(string sortField, bool isAscending, List<Provider> providers)
        {
            switch (isAscending)
            {
                case true:
                    switch (sortField)
                    {
                        case "Name":
                            providers = providers.OrderBy(x => x.Name).ToList();
                            break;
                        case "Address":
                            providers = providers.OrderBy(x => x.Address).ToList();
                            break;
                        default:
                            break;
                    }
                    break;
                case false:
                    switch (sortField)
                    {
                        case "Name":
                            providers = providers.OrderByDescending(x => x.Name).ToList();
                            break;
                        case "Address":
                            providers = providers.OrderByDescending(x => x.Address).ToList();
                            break;
                        default:
                            break;
                    }
                    break;
                default:
            }
            return providers;
        }

        public async Task SendNomenclature(string searchString, string searchField, string sortField, bool isAscendingSort)
        {
            List<Nomenclature> nomenclature = db.Nomenclatures.ToList();
            if (!searchString.IsNullOrEmpty() && !searchField.IsNullOrEmpty())
            {
                nomenclature = SearchNomenclature(searchString, searchField, nomenclature).Result;
            }
            if (!sortField.IsNullOrEmpty())
            {
                nomenclature = SortNomenclature(sortField, isAscendingSort, nomenclature).Result;
            }
            var json = JsonConvert.SerializeObject(nomenclature);
            await Clients.Caller.SendAsync("Receive", json);
        }

        /// <summary>
        /// Nomenclature search on index page
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="searchField">1 - Search by Vendor code, 2 - Title, 3 - Provider name</param>
        /// <returns></returns>
        public async Task<List<Nomenclature>> SearchNomenclature(string searchString, string searchField, List<Nomenclature> nomenclature)
        {
            switch (searchField)
            {
                case "VendorCode":
                    nomenclature = nomenclature.Where(x => x.VendorCode.ToLower().Contains(searchString.ToLower())).ToList();
                    break;
                case "Name":
                    nomenclature = nomenclature.Where(x => x.Name.ToLower().Contains(searchString.ToLower())).ToList();
                    break;
                case "Provider":
                    nomenclature = nomenclature.Where(x => x.Provider.Name.ToLower().Contains(searchString.ToLower())).ToList();
                    break;
                default:
                    break;
            }
            return nomenclature;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sortField">1 - Vndor code, 2 - Name, 3 - Provider name</param>
        /// <param name="isAscending">ascending or descending sort</param>
        /// <param name="counterparties">list to sort</param>
        /// <returns></returns>
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
                        case "Name":
                            nomenclature = nomenclature.OrderBy(x => x.Name).ToList();
                            break;
                        case "Provider":
                            nomenclature = nomenclature.OrderBy(x => x.Provider.Name).ToList();
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
                        case "Name":
                            nomenclature = nomenclature.OrderByDescending(x => x.Name).ToList();
                            break;
                        case "Provider":
                            break;
                        default:
                            nomenclature = nomenclature.OrderByDescending(x => x.Provider.Name).ToList();
                            break;
                    }
                    break;
                default:
            }
            return nomenclature;
        }

        public async Task SendOrders(string searchString, string searchField, bool searchOnlyActive, string sortField, bool isAscendingSort)
        {
            List<Order> orders = db.Orders.ToList();
            if (!searchString.IsNullOrEmpty() && !searchField.IsNullOrEmpty())
            {
                orders = SearchOrders(searchString, searchField, searchOnlyActive, orders).Result;
            }
            if (!sortField.IsNullOrEmpty())
            {
                orders = SortOrders(sortField, isAscendingSort, orders).Result;
            }
            var json = JsonConvert.SerializeObject(orders);
            await Clients.Caller.SendAsync("Receive", json);
        }

        /// <summary>
        ///  Order search on index page
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="searchField">1 - Search by counterparty name, 2 - agreement number, 3 - well name</param>
        /// <param name="searchOnlyActive">True - search only active orders, False - only inactive</param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sortField">1 - Id, 2 - Counterparty name, 3 - Agreement number, 4 - Well name</param>
        /// <param name="isAscending">ascending or descending sort</param>
        /// <param name="counterparties">list to sort</param>
        /// <returns></returns>
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
            List<Maintenance> maintenances = db.Maintenances.ToList();
            if (!sortField.IsNullOrEmpty())
            {
                maintenances = SortMaintenances(sortField, isAscendingSort, maintenances).Result;
            }
            var json = JsonConvert.SerializeObject(maintenances);
            await Clients.Caller.SendAsync("Receive", json);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sortField">1 - Id, 2 - Status, 3 - Start date, 4 - Finish date</param>
        /// <param name="isAscending">ascending or descending sort</param>
        /// <param name="counterparties">list to sort</param>
        /// <returns></returns>
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
            List<Equipment> equipment = db.Equipment.ToList();
            if (!searchString.IsNullOrEmpty() && !searchField.IsNullOrEmpty())
            {
                equipment = SearchEquipment(searchStatus, searchString, searchField, equipment).Result;
            }
            //if (!sortField.IsNullOrEmpty())
            //{
            //    maintenances = SortMaintenances(sortField, isAscendingSort, maintenances).Result;
            //}
            var json = JsonConvert.SerializeObject(equipment);
            await Clients.Caller.SendAsync("Receive", json);
        }
        /// <summary>
        ///  Equipment search on index page
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="searchField">1 - Search by department, 2 - category, 3 - type, 4 - Name, 5 - SerialNum
        /// 6 - Outer diameter, 7 - Inner diameter, 8 - Length
        /// </param>
        /// <param name="searchOnlyActive">True - search equipment that is currently in use, False - only inactive</param>
        /// <returns></returns>
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
                        equipment = equipment.Where(x => x.Id == null).ToList();
                    break;
                case "DiameterInner":
                    if (isInt)
                        equipment = equipment.Where(x => x.DiameterInner == result).ToList();
                    else
                        equipment = equipment.Where(x => x.Id == null).ToList();
                        break;
                case "Length":
                    if (isInt)
                        equipment = equipment.Where(x => x.Length == result).ToList();
                    else
                        equipment = equipment.Where(x => x.Id == null).ToList();
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
                equipment = equipment.Where(x => x.Status.Id == status).ToList();
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
    }
}
