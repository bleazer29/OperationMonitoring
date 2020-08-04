﻿using Castle.Core.Internal;
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
            if (!sortField.IsNullOrEmpty())
            {
                counterparties = SortCounterparties(sortField, isAscendingSort, counterparties).Result;
            }
            var json = JsonConvert.SerializeObject(counterparties);
            await Clients.Caller.SendAsync("Receive", json);
        }

        public async Task<List<Counterparty>> SearchCounterparty(string counterpartyName, List<Counterparty> counterparties)
        {
            counterparties = counterparties.Where(x => x.Title.ToLower().Contains(counterpartyName.ToLower())).ToList();
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

        public async Task<List<Provider>> SortProviders(string sortField, bool isAscending, List<Provider> providers)
        {
            switch (isAscending)
            {
                case true:
                    switch (sortField)
                    {
                        case "Title":
                            providers = providers.OrderBy(x => x.Title).ToList();
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
                        case "Title":
                            providers = providers.OrderByDescending(x => x.Title).ToList();
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
            List<Nomenclature> nomenclature = db.Nomenclatures
                .Include(x => x.Provider)
                .Include(x => x.Specification)
                .ToList();
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

        public async Task<List<Nomenclature>> SearchNomenclature(string searchString, string searchField, List<Nomenclature> nomenclature)
        {
            switch (searchField)
            {
                case "VendorCode":
                    nomenclature = nomenclature.Where(x => x.VendorCode.ToLower().Contains(searchString.ToLower())).ToList();
                    break;
                case "Title":
                    nomenclature = nomenclature.Where(x => x.Title.ToLower().Contains(searchString.ToLower())).ToList();
                    break;
                case "Provider":
                    nomenclature = nomenclature.Where(x => x.Provider.Title.ToLower().Contains(searchString.ToLower())).ToList();
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
                orders = SearchOrders(searchString, searchField, searchOnlyActive, orders).Result;
            }
            if (!sortField.IsNullOrEmpty())
            {
                orders = SortOrders(sortField, isAscendingSort, orders).Result;
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
                maintenances = SortMaintenances(sortField, isAscendingSort, maintenances).Result;
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

        public async Task SendStocks(string searchString, int storageId, string searchField)
        {
            List<Stock> stocks = new List<Stock>();
            stocks = db.Stocks.Include(x => x.Storage)
                .Include(x => x.Nomenclature)
                .ThenInclude(x => x.Provider)
                .Include(x => x.Part)
                .ThenInclude(x => x.Status)
                .Include(x => x.Equipment)
                .ThenInclude(x => x.Status)
                .ToList();
            stocks = SearchStocks(searchString, storageId, searchField, stocks).Result;
            var json = JsonConvert.SerializeObject(stocks);
            await Clients.All.SendAsync("Receive", json);
        }

        private List<Storage> childrenStorages = new List<Storage>();

        public List<Storage> FindStorageChildren(int storageId)
        {
            List<Storage> Storages = db.Storages.Include(x => x.Parent).ThenInclude(x => x.Parent).ToList();
            foreach (var storage in Storages)
            {
                if (storage.Parent != null)
                {
                    if(storage.Parent.Id == storageId)
                    {
                        childrenStorages.Add(storage);
                        FindStorageChildren(storage.Id);
                    }
                }
            }
            return childrenStorages;
        }

        public async Task<List<Stock>> SearchStocks(string searchString, int storageId, string searchField, List<Stock> stocks)
        {
            List<Stock> temp = new List<Stock>();
            if (storageId != 0)
            {
                childrenStorages = new List<Storage>();
                childrenStorages.Add(db.Storages.FirstOrDefault(x => x.Id == storageId));
                childrenStorages = FindStorageChildren(storageId);
                foreach(var child in childrenStorages)
                {
                    temp.AddRange(stocks.Where(x => x.Storage.Id == child.Id && (x.Amount > 0 || x.Part.Amount > 0)).ToList());
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

    }
}
