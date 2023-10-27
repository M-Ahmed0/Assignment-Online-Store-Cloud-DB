using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ApplicationDbContext : DbContext
    {
        //public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        //{
        //}



        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }


        //DONT FORGET TO NUGET INSTALL: Install-Package Microsoft.EntityFrameworkCore.Cosmos -Version 5.0.10 for DbContextbuilder Use Cosmos
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseCosmos(
                "https://localhost:8081",
                "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==",
                databaseName: "test123");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultContainer("Store");

            modelBuilder.Entity<Order>()
                .ToContainer("Orders");

            modelBuilder.Entity<Order>()
                .HasNoDiscriminator();

            modelBuilder.Entity<Order>()
                .HasPartitionKey(o => o.PartitionKey);

            modelBuilder.Entity<Order>()
                .UseETagConcurrency();

            modelBuilder.Entity<Order>().OwnsOne(
                o => o.ShippingAddress,
                sa =>
                {
                    sa.ToJsonProperty("Address");
                    sa.Property(p => p.Street).ToJsonProperty("ShipsToStreet");
                    sa.Property(p => p.City).ToJsonProperty("ShipsToCity");
                });

        }


    }
}
