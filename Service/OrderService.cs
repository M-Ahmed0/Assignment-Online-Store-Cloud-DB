using DAL.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class OrderService : IOrderService
    {
        private readonly IBaseRepository<Order> _orderRepository;

        public OrderService(IBaseRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            return await _orderRepository.GetAll();
        }

        public async Task<Order> GetOrderById(int orderId)
        {
            return await _orderRepository.GetSingle(orderId.ToString());
        }
    }
}
