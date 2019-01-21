using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace Production.Models {
    public class DBContext : DbContext {
        public DBContext (DbContextOptions<DBContext> options)
            : base(options)
        {
        }

        public DbSet<Production.Models.Cart> Cart { get; set; }
        public DbSet<Production.Models.Manufacturer> Manufacturer { get; set; }
        public DbSet<Production.Models.Orders> Orders { get; set; }
        public DbSet<Production.Models.Product> Product { get; set; }
        public DbSet<Production.Models.Materials> Materials { get; set; }   
    }
}