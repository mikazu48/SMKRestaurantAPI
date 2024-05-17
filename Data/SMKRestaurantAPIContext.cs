using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SMKRestaurantAPI.Models;

namespace SMKRestaurantAPI.Data
{
    public class SMKRestaurantAPIContext : DbContext
    {
        public SMKRestaurantAPIContext (DbContextOptions<SMKRestaurantAPIContext> options)
            : base(options)
        {
        }

        public DbSet<SMKRestaurantAPI.Models.Customer> Customer { get; set; }

        public DbSet<SMKRestaurantAPI.Models.Food> Food { get; set; }

        public DbSet<SMKRestaurantAPI.Models.Order> Order { get; set; }
    }
}
