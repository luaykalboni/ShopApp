using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopApp.Models;

namespace ShopApp.Data
{
    public class shoppingsiteContext : DbContext
    {
        public shoppingsiteContext (DbContextOptions<shoppingsiteContext> options)
            : base(options)
        {
        }

        public DbSet<ShopApp.Models.Cart> Cart { get; set; }

        public DbSet<ShopApp.Models.Category> Category { get; set; }

        public DbSet<ShopApp.Models.Product> Product { get; set; }

        public DbSet<ShopApp.Models.User> User { get; set; }
    }
}
