using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public class SeedData
    {
        private readonly ApplicationDbContext _context;

        public SeedData(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task InitializeAsync()
        {
            await SeedUsersAsync();
            await SeedProductsAsync();
            await SeedReviewsAsync();
            await SeedOrdersAsync();
        }

        private async Task SeedUsersAsync()
        {
            if (!(await _context.Users.ToListAsync()).Any())
            {
                var user = new User
                {
                    Id = Guid.NewGuid().ToString(),
                    Username = "sampleUser",
                    Email = "sample@example.com"
                };

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
            }
        }

        private async Task SeedProductsAsync()
        {
            if (!(await _context.Products.ToListAsync()).Any())
            {
                var products = new List<Product>
                {
                    new Product
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Sample Product 1",
                        Price = 100.50,
                        Stock = 10,
                        Description = "Description for product 1",
                        ImageUrl = "url/to/image1.jpg"
                    },
                    new Product
                    
                    {   Id = Guid.NewGuid().ToString(),
                        Name = "Sample Product 2",
                        Price = 200.25,
                        Stock = 5,
                        Description = "Description for product 2",
                        ImageUrl = "url/to/image2.jpg"
                    }
                };

                await _context.Products.AddRangeAsync(products);
                await _context.SaveChangesAsync();
            }
        }

        private async Task SeedReviewsAsync()
        {
            if (!(await _context.Reviews.ToListAsync()).Any())
            {
                var product = await _context.Products.FirstAsync();

                var review = new Review
                {Id = Guid.NewGuid().ToString(),
                    Message = "test product!",
                    Name = "John Doe",
                    Rating = 5,
                    CreatedAt = DateTime.Now,
                    ProductId = product.Id
                };

                await _context.Reviews.AddAsync(review);
                await _context.SaveChangesAsync();
            }
        }

        private async Task SeedOrdersAsync()
        {
            if (!(await _context.Orders.ToListAsync()).Any())
            {
                var user = await _context.Users.FirstAsync();
                var products = await _context.Products.Take(2).ToListAsync();

                var order = new Order
                {
                    Id = Guid.NewGuid().ToString(),
                    PartitionKey = "123PartitionKey",
                    OrderDate = DateTime.Now,
                    Status = OrderStatus.Placed,
                    UserId = user.Id,
                    ProductIds = products.Select(p => p.Id).ToList(),
                    ShippingAddress = new ShippingAddress
                    {
                        Street = "123 Main St",
                        City = "SampleCity"
                    }
                };

                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();
            }
        }
    }
}
