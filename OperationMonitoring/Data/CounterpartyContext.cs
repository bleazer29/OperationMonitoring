using Microsoft.EntityFrameworkCore;
using OperationMonitoring.Models;
using OperationMonitoring.Models.ClientsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OperationMonitoring.Data
{
    public class CounterpartyContext : DbContext
    {
        public DbSet<Counterparty> Counterparties { get; set; }
        public DbSet<Agreement> Agreements { get; set; }
        public DbSet<Well> Wells { get; set; }

        public CounterpartyContext(DbContextOptions<CounterpartyContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = IL707; Initial Catalog = CounterpartyDB; Integrated Security = True;")
                .UseLazyLoadingProxies();
        }

        

    }
}
