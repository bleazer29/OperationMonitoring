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

        public async Task CounterpartySearch(string counterpartyName, int pageNumber, int pageSize)
        {
            if(pageNumber == 0 && pageSize == 0)
            {
                pageNumber = 1;
                pageSize = 5;
            }
            List<Counterparty> counterparties1 = db.Counterparties.ToList();
            IPagedList<Counterparty> counterparties = new PagedList<Counterparty>(counterparties1, pageNumber, pageSize);
            if (string.IsNullOrEmpty(counterpartyName) == false)
            {
                counterparties = counterparties1.Where(x => x.Title.Contains(counterpartyName)).ToPagedList(1, 5);
            }
            var jsonCounterparties = JsonConvert.SerializeObject(counterparties);
            await Clients.Caller.SendAsync("Receive", jsonCounterparties);
            
        }
    }
}
