using Microsoft.EntityFrameworkCore;
using OperationMonitoring.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OperationMonitoring.Data
{
    public class HistoryContext : DbContext
    {

        public HistoryContext()
        {
            Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=IL634\\SQLEXPRESS;Database=HistoryDB;Trusted_Connection=True;");
        }

        public DbSet<History> History { get; set; }
    }
}
