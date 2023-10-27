using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public class SeedOrder
    {
        private readonly ApplicationDbContext _context;

        public SeedOrder(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task InitializeAsync()
        {
            // Fetch all orders from the database
            var orders = await _context.Orders.ToListAsync();

            // Check if the list has any items
            if (!orders.Any())
            {
                // For simplicity, get the first user and some products
                var user = _context.Users.FirstOrDefault();
                var products = _context.Products.Take(2).ToList();

                // Create a new order
                var order = new Order
                {
                    OrderDate = DateTime.Now,
                    Status = OrderStatus.Placed,  // Assuming you have an 'Placed' enum value in OrderStatus
                    User = user,
                    Products = products,
                    ShippingAddress = new ShippingAddress
                    {
                        // Sample address details. Adjust as necessary.
                        Street = "123 Main St",
                        City = "SampleCity",
                       
                    },
                    PartitionKey = "SamplePartitionKey" // Set a meaningful partition key value based on your data modeling strategy.
                };

                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();
            }
        }
    }
}
