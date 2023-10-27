using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Service
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllOrders();
        Task<Order> GetOrderById(int orderId);
       //Task<IEnumerable<Order>> GetOrdersByUser(User user);
        //Task AddOrder(Order order);
        //void UpdateOrder(Order order);
        //Task DeleteOrder(int orderId);
    }
}
