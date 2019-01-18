using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using MySql.Data.Common;


namespace Production.Models {
    public class CartContext : DbContext {
        public CartContext (DbContextOptions<CartContext> options)
            : base(options)
        {
        }

        public DbSet<Production.Models.Cart> Cart { get; set; }
    }
}