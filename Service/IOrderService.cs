using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Service.DTOs;

namespace Service
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDTO>> GetAllOrders();
        Task<OrderDTO> CreateOrder(OrderCreationDTO orderCreationDto);
        Task UpdateOrderStatus(OrderDTO orderDto);

        Task AddOrder(OrderDTO orderDto);

        Task<OrderDTO> GetOrderById(string orderId);
        Task<OrderDTO> GetOrderByIds(string orderId, string userId);
        //Task<IEnumerable<Order>> GetOrdersByUser(User user);
        //Task AddOrder(Order order);
        //void UpdateOrder(Order order);
        //Task DeleteOrder(int orderId);
    }
}
