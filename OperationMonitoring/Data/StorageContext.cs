using Microsoft.EntityFrameworkCore;
using OperationMonitoring.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OperationMonitoring.Data
{
    public class StorageContext : DbContext
    {
        public DbSet<Nomenclature> Nomenclatures { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Storage> Storages { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Purchase> Purchases { get; set; }

        public StorageContext()
        {
            Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=IL634\\SQLEXPRESS;Database=StorageDB;Trusted_Connection=True;");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Provider[] providers = {
            //    new Provider() { Id = 1, Name="Provider1", Address="Address1"},
            //    new Provider() { Id = 2, Name="New Provider", Address="8 Wang Hoi Road, Kowloon Bay, Hong Kong"},
            //    new Provider() { Id = 3, Name="AIP Company", Address="148 Wing Lok Street, Sheung Wan, Hong Kong"}};
            //modelBuilder.Entity<Provider>().HasData(providers);

            //Storage[] storages = {
            //    new Storage() { Id = 1, Name="Main Building" },
            //    new Storage() { Id = 2, Name="Side Building"},
            //    new Storage() { Id = 3, Name="Floor 1", ParentId=1},
            //    new Storage() { Id = 4, Name = "Floor 2", ParentId = 1 },
            //    new Storage() { Id = 5, Name = "Floor 3", ParentId = 2} , 
            //    new Storage() { Id = 6, Name="Room 4", ParentId=3},
            //    new Storage() { Id = 7, Name = "Room 5", ParentId = 3 },
            //    new Storage() { Id = 8, Name="Room 6", ParentId=4},
            //    new Storage() { Id = 9, Name = "Room 7", ParentId = 4 }};
            //modelBuilder.Entity<Storage>().HasData(storages);

            //Nomenclature[] nomenclatures = {
            //    new Nomenclature() { Id = 1, Name="Motor", Specifications="Small", Amount=15},
            //    new Nomenclature() { Id = 2, Name="Spacer", Specifications="Big", Amount=0},
            //    new Nomenclature() { Id = 3, Name="Ring", Specifications="Small",  Amount=16},
            //    new Nomenclature() { Id = 4, Name="Shaft", Specifications="Big", Amount=15},
            //    new Nomenclature() { Id = 5, Name="Ring", Specifications="Big",  Amount=5},
            //    new Nomenclature() { Id = 6, Name="Shaft", Specifications="Big", Amount=10},
            //};
            //modelBuilder.Entity<Nomenclature>().HasData(nomenclatures);

            //Purchase[] purchases = {
            //    new Purchase() { Id = 1, Amount=5, Date=DateTime.Now, NomenclatureId=6},
            //    new Purchase() { Id = 2, Amount=10, Date=DateTime.Now.AddDays(-10), NomenclatureId=6},
            //    new Purchase() { Id = 3, Amount=5, Date=DateTime.Now.AddDays(-10), NomenclatureId=5},
            //    new Purchase() { Id = 4, Amount=15, Date=DateTime.Now, NomenclatureId=4},
            //    new Purchase() { Id = 5, Amount=15, Date=DateTime.Now.AddMonths(-3), NomenclatureId=3},
            //    new Purchase() { Id = 6, Amount=1, Date=DateTime.Now, NomenclatureId=3},
            //    new Purchase() { Id = 7, Amount=15, Date=DateTime.Now, NomenclatureId=1}
            //};
            //modelBuilder.Entity<Purchase>().HasData(purchases);

            //Stock[] stocks = {
            //   new Stock() { Id = 1, Amount=5, NomenclatureId=6, StorageId=4},
            //   new Stock() { Id = 2, Amount=2, NomenclatureId=6, StorageId=6},
            //   new Stock() { Id = 3, Amount=5, NomenclatureId=1, StorageId=4},
            //   new Stock() { Id = 4, Amount=5, NomenclatureId=1, StorageId=5},
            //};
            //modelBuilder.Entity<Stock>().HasData(stocks);

            
           
        }

        

    }
}
