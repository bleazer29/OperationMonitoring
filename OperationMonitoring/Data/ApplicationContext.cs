using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OperationMonitoring.Models;

namespace OperationMonitoring.Data
{
    public class ApplicationContext : IdentityDbContext
    {
        public DbSet<Nomenclature> Nomenclatures { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Storage> Storages { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Purchase> Purchases { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Types> Types { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<EquipmentStatus> EquipmentStatuses { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<Maintenance> Maintenances { get; set; }
        public DbSet<Operation> Operations { get; set; }

        public DbSet<Counterparty> Counterparties { get; set; }
        public DbSet<Agreement> Agreements { get; set; }
        public DbSet<Well> Wells { get; set; }


        public DbSet<HistoryType> HistoryTypes { get; set; }
        public DbSet<EquipmentHistory> EquipmentHistory { get; set; }
        public DbSet<PartHistory> PartsHistory { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies().UseSqlServer("Data Source = IL634\\SQLEXPRESS; Initial Catalog = OperationMonitorDB; Integrated Security = True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            EquipmentStatus[] statuses = {
                new EquipmentStatus() { Id = 1, Title = "RFU" },
                new EquipmentStatus() { Id = 2, Title = "JF" },
                new EquipmentStatus() { Id = 3, Title = "WS" },
                new EquipmentStatus() { Id = 4, Title = "SP" },
                new EquipmentStatus() { Id = 5, Title = "RP" },
                new EquipmentStatus() { Id = 6, Title = "Scrap" }};
            modelBuilder.Entity<EquipmentStatus>().HasData(statuses);
            modelBuilder.Entity<Operation>().Property(x => x.Id).ValueGeneratedNever();
            modelBuilder.Entity<Maintenance>().Property(x => x.Id).ValueGeneratedNever();

            modelBuilder.Entity<HistoryType>().HasData(
                new HistoryType[]
                {
                    new HistoryType {Id = 1, Name = "MaintenanceHistory"},
                    new HistoryType {Id = 2, Name = "OrderHistory"},
                    new HistoryType {Id = 3, Name = "General"}
                });

            Provider[] providers = {
                new Provider() { Id = 1, Name="Provider1", Address="Address1"},
                new Provider() { Id = 2, Name="New Provider", Address="8 Wang Hoi Road, Kowloon Bay, Hong Kong"},
                new Provider() { Id = 3, Name="AIP Company", Address="148 Wing Lok Street, Sheung Wan, Hong Kong"}};
            modelBuilder.Entity<Provider>().HasData(providers);

            Storage[] storages = {
                new Storage() { Id = 1, Name="Main Building" },
                new Storage() { Id = 2, Name="Side Building"},
                new Storage() { Id = 3, Name="Floor 1", ParentId=1},
                new Storage() { Id = 4, Name = "Floor 2", ParentId = 1 },
                new Storage() { Id = 5, Name = "Floor 3", ParentId = 2} ,
                new Storage() { Id = 6, Name="Room 4", ParentId=3},
                new Storage() { Id = 7, Name = "Room 5", ParentId = 3 },
                new Storage() { Id = 8, Name="Room 6", ParentId=4},
                new Storage() { Id = 9, Name = "Room 7", ParentId = 4 }};
            modelBuilder.Entity<Storage>().HasData(storages);

            Nomenclature[] nomenclatures = {
                new Nomenclature() { Id = 1, Name="Motor", Specifications="Small", ProviderId=1, Amount=15},
                new Nomenclature() { Id = 2, Name="Spacer", Specifications="Big",ProviderId=1, Amount=0},
                new Nomenclature() { Id = 3, Name="Ring", Specifications="Small",ProviderId=2,  Amount=16},
                new Nomenclature() { Id = 4, Name="Shaft", Specifications="Big",ProviderId=2, Amount=15},
                new Nomenclature() { Id = 5, Name="Ring", Specifications="Big",ProviderId=1,  Amount=5},
                new Nomenclature() { Id = 6, Name="Shaft", Specifications="Big",ProviderId=1, Amount=10},
            };
            modelBuilder.Entity<Nomenclature>().HasData(nomenclatures);

            Purchase[] purchases = {
                new Purchase() { Id = 1, Amount=5, Date=DateTime.Now, NomenclatureId=6},
                new Purchase() { Id = 2, Amount=10, Date=DateTime.Now.AddDays(-10), NomenclatureId=6},
                new Purchase() { Id = 3, Amount=5, Date=DateTime.Now.AddDays(-10), NomenclatureId=5},
                new Purchase() { Id = 4, Amount=15, Date=DateTime.Now, NomenclatureId=4},
                new Purchase() { Id = 5, Amount=15, Date=DateTime.Now.AddMonths(-3), NomenclatureId=3},
                new Purchase() { Id = 6, Amount=1, Date=DateTime.Now, NomenclatureId=3},
                new Purchase() { Id = 7, Amount=15, Date=DateTime.Now, NomenclatureId=1}
            };
            modelBuilder.Entity<Purchase>().HasData(purchases);

            Stock[] stocks = {
               new Stock() { Id = 1, Amount=5, NomenclatureId=6, StorageId=4},
               new Stock() { Id = 2, Amount=2, NomenclatureId=6, StorageId=6},
               new Stock() { Id = 3, Amount=5, NomenclatureId=1, StorageId=4},
               new Stock() { Id = 4, Amount=5, NomenclatureId=1, StorageId=5},
            };
            modelBuilder.Entity<Stock>().HasData(stocks);
        }
    }
}
