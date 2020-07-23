using Microsoft.EntityFrameworkCore;
using OperationMonitoring.EquipmentModels;
using OperationMonitoring.Models;
using OperationMonitoring.Models.EquipmentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OperationMonitoring.Data
{
    public class EquipmentContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Types> Types { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<EquipmentStatus> EquipmentStatuses { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<Parts> Parts { get; set; }
        public DbSet<Maintenance> Maintenances { get; set; }
        public DbSet<Operation> Operations { get; set; }

        public EquipmentContext(DbContextOptions<EquipmentContext> options) : base(options)
        {            
            Database.EnsureCreated();
            Database.Migrate();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb; Database=EquipmentDB; Trusted_Connection =True;")
                .UseLazyLoadingProxies();
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
        }
    }
}
