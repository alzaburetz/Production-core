using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using MySql.Data.Common;


namespace Production.Models {
    public class ManufacturerContext : DbContext {
        public ManufacturerContext (DbContextOptions<ManufacturerContext> options)
            : base(options)
        {
        }

        public DbSet<Production.Models.Manufacturer> Manufacturer { get; set; }
    }
}