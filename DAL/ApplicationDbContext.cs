using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseCosmos(
                "https://localhost:8081",
                "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==",
                databaseName: "Widget-&-Co");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultContainer("Store");

            // Order Configuration
            modelBuilder.Entity<Order>()
                .ToContainer("Orders")
                .HasNoDiscriminator()
                .HasPartitionKey(o => o.PartitionKey)
                .UseETagConcurrency();

            modelBuilder.Entity<Order>()
                .OwnsOne(o => o.ShippingAddress, sa =>
                {
                    sa.ToJsonProperty("Address");
                    sa.Property(p => p.Street).ToJsonProperty("ShipsToStreet");
                    sa.Property(p => p.City).ToJsonProperty("ShipsToCity");
                });

            // Product Configuration
            modelBuilder.Entity<Product>()
                .ToContainer("Products")
                .HasNoDiscriminator()
                .HasPartitionKey(p => p.Id)
                .UseETagConcurrency();

            // User Configuration
            modelBuilder.Entity<User>()
                .ToContainer("Users")
                .HasNoDiscriminator()
                .HasPartitionKey(u => u.Id)
                .UseETagConcurrency();

            // Review Configuration (you can add further configurations for Review if needed)
            modelBuilder.Entity<Review>()
                .ToContainer("Reviews")
                .HasNoDiscriminator()
                .HasPartitionKey(r => r.Id)
                .UseETagConcurrency();
        }
    }
}
