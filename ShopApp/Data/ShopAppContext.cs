using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GameStore_IL.Models;

namespace ShopApp.Data
{
    public class shoppingsiteContext : DbContext
    {
        public shoppingsiteContext (DbContextOptions<shoppingsiteContext> options)
            : base(options)
        {
        }

        public DbSet<GameStore_IL.Models.Cart> Cart { get; set; }

        public DbSet<GameStore_IL.Models.Category> Category { get; set; }

        public DbSet<GameStore_IL.Models.Product> Product { get; set; }

        public DbSet<GameStore_IL.Models.User> User { get; set; }
    }
}
