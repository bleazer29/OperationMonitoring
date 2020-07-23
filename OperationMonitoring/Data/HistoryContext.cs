using Microsoft.EntityFrameworkCore;
using OperationMonitoring.Models;
using OperationMonitoring.Models.HistoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OperationMonitoring.Data
{
    public class HistoryContext : DbContext
    {

        public HistoryContext(DbContextOptions<HistoryContext> options)
        : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies().UseSqlServer("Data Source = IL707; Initial Catalog = HistoryDB; Integrated Security = True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HistoryType>().HasData(
                new HistoryType[]
                {
                    new HistoryType {Id = 1, Name = "MaintenanceHistory"},
                    new HistoryType {Id = 2, Name = "OrderHistory"},
                    new HistoryType {Id = 3, Name = "General"}
                });
        }

        public DbSet<HistoryType> HistoryTypes { get; set; }
        public DbSet<EquipmentHistory> EquipmentHistory { get; set; }
        public DbSet<PartHistory> PartsHistory { get; set; }

    }
}
