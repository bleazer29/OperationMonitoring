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

        public async Task SendProviders(string searchString, int searchField, int sortField, bool isAscendingSort)
        {
            List<Provider> providers = db.Providers.ToList();
            if (string.IsNullOrEmpty(searchString) == false)
            {
                providers = SearchProvider(searchString, searchField, providers).Result;
            }
            if (sortField != 0)
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
        public async Task<List<Provider>> SearchProvider(string searchString, int searchField, List<Provider> providers)
        {
            if(string.IsNullOrEmpty(searchString) == false)
            {
                if (searchField == 1) providers = providers.Where(x => x.Name.Contains(searchString)).ToList();
                else if (searchField == 2) providers = providers.Where(x => x.Address.Contains(searchString)).ToList();
                else if (searchField == 3) providers = providers.Where(x => x.EDRPOU.Contains(searchString)).ToList();
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
        public async Task<List<Provider>> SortProviders(int sortField, bool isAscending, List<Provider> providers)
        {
            if (sortField == 1)
            {
                if (isAscending) providers = providers.OrderBy(x => x.Name).ToList();
                else providers = providers.OrderByDescending(x => x.Name).ToList();
            }
            else if (sortField == 2)
            {
                if (isAscending) providers = providers.OrderBy(x => x.Address).ToList();
                else providers = providers.OrderByDescending(x => x.Address).ToList();
            }
            return providers;
        }

        public async Task SendNomenclature(string searchString, string searchField, string sortField, bool isAscendingSort)
        {
            List<Nomenclature> nomenclature = db.Nomenclatures.ToList();
            if (string.IsNullOrEmpty(searchString) == false)
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
            //if (string.IsNullOrEmpty(searchString) == false)
            //{
            //    if (searchField == 1) nomenclature = nomenclature.Where(x => x.VendorCode.Contains(searchString)).ToList();
            //    else if (searchField == 2) nomenclature = nomenclature.Where(x => x.Name.Contains(searchString)).ToList();
            //    else if (searchField == 3) nomenclature = nomenclature.Where(x => x.Provider.Name.Contains(searchString)).ToList();
            //}
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
            switch (sortField)
            {
                case "VendorCode":
                    break;
                case "Name":
                    break;
                case "Provider":
                    break;
                default:
                    break;
            }
            //if (sortField == 1)
            //{
            //    if (isAscending) nomenclature = nomenclature.OrderBy(x => x.VendorCode).ToList();
            //    else nomenclature = nomenclature.OrderByDescending(x => x.VendorCode).ToList();
            //}
            //else if (sortField == 2)
            //{
            //    if (isAscending) nomenclature = nomenclature.OrderBy(x => x.Name).ToList();
            //    else nomenclature = nomenclature.OrderByDescending(x => x.Name).ToList();
            //}
            //else if (sortField == 3)
            //{
            //    if (isAscending) nomenclature = nomenclature.OrderBy(x => x.Provider.Name).ToList();
            //    else nomenclature = nomenclature.OrderByDescending(x => x.Provider.Name).ToList();
            //}
            return nomenclature;
        }

        public async Task SendOrders(string searchString, int searchField, bool searchOnlyActive, int sortField, bool isAscendingSort)
        {
            List<Order> orders = db.Orders.ToList();
            if (string.IsNullOrEmpty(searchString) == false)
            {
                orders = SearchOrders(searchString, searchField, searchOnlyActive, orders).Result;
            }
            if (sortField != 0)
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
        public async Task<List<Order>> SearchOrders(string searchString, int searchField, bool searchOnlyActive, List<Order> orders)
        {
            if (string.IsNullOrEmpty(searchString) == false)
            {
                if (searchField == 1) orders = orders.Where(x => x.Agreement.Counterparty.Title.Contains(searchString)).ToList();
                else if (searchField == 2) orders = orders.Where(x => x.Agreement.AgreementNumber.Contains(searchString)).ToList();
                else if (searchField == 3) orders = orders.Where(x => x.Well.Title.Contains(searchString)).ToList();
            }
            if (searchOnlyActive == true) orders = orders.Where(x => x.IsOpen == true).ToList();
            else orders = orders.Where(x => x.IsOpen == false).ToList();
            return orders;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sortField">1 - Id, 2 - Counterparty name, 3 - Agreement number, 4 - Well name</param>
        /// <param name="isAscending">ascending or descending sort</param>
        /// <param name="counterparties">list to sort</param>
        /// <returns></returns>
        public async Task<List<Order>> SortOrders(int sortField, bool isAscending, List<Order> orders)
        {
            if (sortField == 1)
            {
                if (isAscending) orders = orders.OrderBy(x => x.Id).ToList();
                else orders = orders.OrderByDescending(x => x.Id).ToList();
            }
            else if (sortField == 2)
            {
                if (isAscending) orders = orders.OrderBy(x => x.Agreement.Counterparty.Title).ToList();
                else orders = orders.OrderByDescending(x => x.Agreement.Counterparty.Title).ToList();
            }
            else if (sortField == 3)
            {
                if (isAscending) orders = orders.OrderBy(x => x.Agreement.AgreementNumber).ToList();
                else orders = orders.OrderByDescending(x => x.Agreement.AgreementNumber).ToList();
            }
            else if (sortField == 4)
            {
                if (isAscending) orders = orders.OrderBy(x => x.Well.Title).ToList();
                else orders = orders.OrderByDescending(x => x.Well.Title).ToList();
            }
            return orders;
        }

        public async Task SendMaintenances(int sortField, bool isAscendingSort)
        {
            List<Maintenance> maintenances = db.Maintenances.ToList();
            if (sortField != 0)
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
        public async Task<List<Maintenance>> SortMaintenances(int sortField, bool isAscending, List<Maintenance> maintenances)
        {
            if (sortField == 1)
            {
                if (isAscending) maintenances = maintenances.OrderBy(x => x.Id).ToList();
                else maintenances = maintenances.OrderByDescending(x => x.Id).ToList();
            }
            else if (sortField == 2)
            {
                if (isAscending) maintenances = maintenances.OrderBy(x => x.IsOpened).ToList();
                else maintenances = maintenances.OrderByDescending(x => x.IsOpened).ToList();
            }
            else if (sortField == 3)
            {
                if (isAscending) maintenances = maintenances.OrderBy(x => x.StartDate).ToList();
                else maintenances = maintenances.OrderByDescending(x => x.FinishDate).ToList();
            }
            return maintenances;
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
        public async Task EquipmentSearch(string searchString, int searchField, bool searchOnlyActive)
        {
            List<Equipment> equipment = db.Equipment.ToList();
            if (string.IsNullOrEmpty(searchString) == false)
            {
                if (searchField == 1) equipment = equipment.Where(x => x.Department.Title.Contains(searchString)).ToList();
                else if (searchField == 2) equipment = equipment.Where(x => x.Category.Title.Contains(searchString)).ToList();
                else if (searchField == 3) equipment = equipment.Where(x => x.Type.Title.Contains(searchString)).ToList();
                if (searchField == 4) equipment = equipment.Where(x => x.Title.Contains(searchString)).ToList();
                else if (searchField == 5) equipment = equipment.Where(x => x.SerialNum.Contains(searchString)).ToList();
                else if (searchField == 6) equipment = equipment.Where(x => x.DiameterOuter == Int32.Parse(searchString)).ToList();
                else if (searchField == 7) equipment = equipment.Where(x => x.DiameterInner == Int32.Parse(searchString)).ToList();
                else if (searchField == 8) equipment = equipment.Where(x => x.Length == Int32.Parse(searchString)).ToList();
            }
            if (searchOnlyActive == true) equipment = equipment.Where(x => x.Status.Title == "JF").ToList();
            else equipment = equipment.Where(x => x.Status.Title != "JF").ToList();
            var json = JsonConvert.SerializeObject(equipment);
            await Clients.Caller.SendAsync("Receive", json);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sortField">1 - Id, 2 - Counterparty name, 3 - Agreement number, 4 - Well name</param>
        /// <param name="isAscending">ascending or descending sort</param>
        /// <param name="counterparties">list to sort</param>
        /// <returns></returns>
        public async Task<List<Equipment>> SortEquipment(int sortField, bool isAscending, List<Equipment> equipment)
        {
            if (sortField == 1)
            {
                if (isAscending) equipment = equipment.OrderBy(x => x.Id).ToList();
                else equipment = equipment.OrderByDescending(x => x.Id).ToList();
            }
            else if (sortField == 2)
            {
                if (isAscending) equipment = equipment.OrderBy(x => x.Status.Title).ToList();
                else equipment = equipment.OrderByDescending(x => x.Status.Title).ToList();
            }
            else if (sortField == 3)
            {
                if (isAscending) equipment = equipment.OrderBy(x => x.Department.Title).ToList();
                else equipment = equipment.OrderByDescending(x => x.Department.Title).ToList();
            }
            else if (sortField == 4)
            {
                if (isAscending) equipment = equipment.OrderBy(x => x.Category.Title).ToList();
                else equipment = equipment.OrderByDescending(x => x.Category.Title).ToList();
            }
            else if (sortField == 5)
            {
                if (isAscending) equipment = equipment.OrderBy(x => x.Type.Title).ToList();
                else equipment = equipment.OrderByDescending(x => x.Type.Title).ToList();
            }
            else if (sortField == 6)
            {
                if (isAscending) equipment = equipment.OrderBy(x => x.Title).ToList();
                else equipment = equipment.OrderByDescending(x => x.Title).ToList();
            }
            else if (sortField == 7)
            {
                if (isAscending) equipment = equipment.OrderBy(x => x.DiameterInner).ToList();
                else equipment = equipment.OrderByDescending(x => x.DiameterOuter).ToList();
            }
            return equipment;
        }
    }
}
