using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IOrderRepository : IBaseRepository<Order>
    {

        Task<IEnumerable<Order>> GetOrdersByUserId(string userId);
        Task UpdateOrderStatus(string orderId, OrderStatus status);

        Task<Order> GetSingleWithPartitionKey(string orderId, string userId);

    }
}
