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
        public DbSet<Specification> Specifications { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Storage> Storages { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<EquipmentCategory> EquipmentCategories { get; set; }
        public DbSet<EquipmentType> EquipmentTypes { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<EquipmentStatus> EquipmentStatuses { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<Counterparty> Counterparties { get; set; }
        public DbSet<Agreement> Agreements { get; set; }
        public DbSet<Maintenance> Maintenances { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Well> Wells { get; set; }
        public DbSet<HistoryType> HistoryTypes { get; set; }
        public DbSet<EquipmentHistory> EquipmentHistory { get; set; }
        public DbSet<Assemble> Assemblies { get; set; }
        public DbSet<PartHistory> PartHistory { get; set; }
        public DbSet<StorageHistory> StorageHistory { get; set; }
        public DbSet<MaintenanceHistory> MaintenanceHistory { get; set; }
        public DbSet<MaintenanceType> MaintenanceTypes { get; set; }
        public DbSet<MaintenanceCategory> MaintenanceCategories { get; set; }
        public DbSet<OrderHistory> OrderHistory { get; set; }
        public DbSet<Doc> Docs { get; set; }
        public DbSet<DocType> DocTypes { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<UsageType> UsageTypes { get; set; }
        public DbSet<Preset> Presets { get; set; }
        public DbSet<PresetItem> PresetItems { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies().UseSqlServer("Data Source = (localdb)\\mssqllocaldb; Initial Catalog = OperationMonitorDB; Integrated Security = True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            EquipmentStatus[] statuses = {
                new EquipmentStatus() { Id = 1, Title = "NA" },
                new EquipmentStatus() { Id = 2, Title = "RFU" },
                new EquipmentStatus() { Id = 3, Title = "JF" },
                new EquipmentStatus() { Id = 4, Title = "WS" },
                new EquipmentStatus() { Id = 5, Title = "SP" },
                new EquipmentStatus() { Id = 6, Title = "RP" },
                new EquipmentStatus() { Id = 7, Title = "Scrap" }};
            modelBuilder.Entity<EquipmentStatus>().HasData(statuses);

            modelBuilder.Entity<HistoryType>().HasData(
                new HistoryType[]
                {
                    new HistoryType {Id = 1, Name = "Write-off"},
                    new HistoryType {Id = 2, Name = "Transportation"},
                    new HistoryType {Id = 3, Name = "Supply"}
                });

            modelBuilder.Entity<MaintenanceType>().HasData(
                new MaintenanceType[]
                {
                    new MaintenanceType {Id = 1, Name = "Common maintenance"},
                    new MaintenanceType {Id = 2, Name = "Outer meintenance"},
                    new MaintenanceType {Id = 3, Name = "Additional meintenance"}
                });

            modelBuilder.Entity<UsageType>().HasData(
                new UsageType[]
                {
                    new UsageType {Id = 1, Title = "General"},
                    new UsageType {Id = 2, Title = "Special"}
                });

            Provider[] providers = {
                new Provider() { Id = 1, Name="Provider1", Address="Address1", EDRPOU="32855961"},
                new Provider() { Id = 2, Name="New Provider", Address="8 Wang Hoi Road, Kowloon Bay, Hong Kong", EDRPOU="38377143"},
                new Provider() { Id = 3, Name="AIP Company", Address="148 Wing Lok Street, Sheung Wan, Hong Kong", EDRPOU="47855961"}};
            modelBuilder.Entity<Provider>().HasData(providers);


            Storage st1 = new Storage() { Id = 1, Name = "Main Building", Location="Kyiv, Ukraine" };
            Storage st2 = new Storage() { Id = 2, Name = "Side Building", Location = "Kyiv, Ukraine" };
            Storage st3 = new Storage() { Id = 3, Name = "Floor 1", ParentId = 1 };
            Storage st4 = new Storage() { Id = 4, Name = "Floor 2", ParentId = 2 };
            Storage st5 = new Storage() { Id = 5, Name = "Floor 3", ParentId = 1 };
            Storage st6 = new Storage() { Id = 6, Name = "Room 4", ParentId = 3 };
            Storage st7 = new Storage() { Id = 7, Name = "Room 5", ParentId = 4 };
            Storage st8 = new Storage() { Id = 8, Name = "Room 6", ParentId = 5 };
            Storage st9 = new Storage() { Id = 9, Name = "Room 7", ParentId = 4 };

            Storage[] storages = { st1, st2, st3, st4, st5, st6, st7, st8, st9};
            modelBuilder.Entity<Storage>().HasData(storages);

            Nomenclature[] nomenclatures = {
                new Nomenclature() { Id = 1, Name="Motor"},
                new Nomenclature() { Id = 2, Name="Spacer"},
                new Nomenclature() { Id = 3, Name="Ring"},
                new Nomenclature() { Id = 4, Name="Shaft"},
                new Nomenclature() { Id = 5, Name="Ring"},
                new Nomenclature() { Id = 6, Name="Shaft"},
            };
            modelBuilder.Entity<Nomenclature>().HasData(nomenclatures);
            Specification[] specifications =
            {
                new Specification() { Id=1, Weight=3000, OperatingTime=12000  },
                new Specification() { Id=2, Weight=30, Height=30, OperatingTime=9000  },
                new Specification() { Id=3, Height=20, OperatingTime=5000  },
                new Specification() { Id=4, Weight=200, OperatingTime=12000  },
                new Specification() { Id=5, Height=40, OperatingTime=12000  },
                new Specification() { Id=6, Weight=100, OperatingTime=10000  },
            };
            modelBuilder.Entity<Specification>().HasData(specifications);

            Stock[] stocks = {
               new Stock() { Id = 1, Amount=5},
               new Stock() { Id = 2, Amount=2},
               new Stock() { Id = 3, Amount=5},
               new Stock() { Id = 4, Amount=5}
            };
            modelBuilder.Entity<Stock>().HasData(stocks);

            Department[] departments =
            {
                new Department() { Id=1, Title="Department1" },
                new Department() { Id=2, Title="Department2" },
                new Department() { Id=3, Title="Department3" }
            };
            modelBuilder.Entity<Department>().HasData(departments);

            EquipmentCategory[] categories =
            {
                new EquipmentCategory() { Id=1, Title="Category1" },
                new EquipmentCategory() { Id=2, Title="Category2" },
                new EquipmentCategory() { Id=3, Title="Category3" }
            };
            modelBuilder.Entity<EquipmentCategory>().HasData(categories);

            EquipmentType[] types =
            {
                new EquipmentType() { Id=1, Title="Type1" },
                new EquipmentType() { Id=2, Title="Type2" },
                new EquipmentType() { Id=3, Title="Type3" }
            };
            modelBuilder.Entity<EquipmentType>().HasData(types);


        }
    }
}
