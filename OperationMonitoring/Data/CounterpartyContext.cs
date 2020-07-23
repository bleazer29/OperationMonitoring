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
            Database.Migrate();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb; Database=CounterpartyDb; Trusted_Connection =True;")
                .UseLazyLoadingProxies();
        }

        

    }
}
