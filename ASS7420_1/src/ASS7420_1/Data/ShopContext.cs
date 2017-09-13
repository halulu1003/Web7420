using Microsoft.EntityFrameworkCore;
using ASS7420_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASS7420_1.Data
{
    public class ShopContext: DbContext
    {

        public ShopContext(DbContextOptions<ShopContext> opyions) : base(opyions)
        { }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Orderitem> Orderitems { get; set; }
        public DbSet<Hat> Hats { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Category> Categorys { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<Orderitem>().ToTable("Orderitem");
            modelBuilder.Entity<Hat>().ToTable("Hat");
            modelBuilder.Entity<Supplier>().ToTable("Supplier");
            modelBuilder.Entity<Category>().ToTable("Category");
        }

    }
}
