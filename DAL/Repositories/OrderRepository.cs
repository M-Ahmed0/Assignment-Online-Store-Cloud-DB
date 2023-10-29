using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class OrderRepository : EntityBaseRepository<Order>, IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context) : base(context)
        {
            _context = context; 
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserId(string userId)
        {
            return await _context.Orders.Where(o => o.UserId == userId).ToListAsync();
        }

        public async Task<Order> GetSingleWithPartitionKey(string id, string userId)
        {
            return await _context.Orders.AsNoTracking().FirstOrDefaultAsync(o => o.Id == id && o.PartitionKey == userId);
        }

        public async Task UpdateOrderStatus(string orderId, OrderStatus status)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                order.Status = status;
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateOrderStatus(string orderId, string userId, OrderStatus status)
        {
            var order = await _context.Orders
                                      .Where(o => o.Id == orderId && o.PartitionKey == userId)
                                      .SingleOrDefaultAsync();

            if (order != null)
            {
                order.Status = status;
                await _context.SaveChangesAsync();
            }
        }

    }
}
