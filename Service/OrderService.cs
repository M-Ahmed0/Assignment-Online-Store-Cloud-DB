using AutoMapper;
using DAL.Repositories;
using Domain.Entities;
using Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class OrderService : IOrderService
    {

        private readonly IOrderRepository _orderRepository;

        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<OrderDTO>> GetAllOrders()
        {
            var orders = await _orderRepository.GetAll();
            var orderDTOs = _mapper.Map<IEnumerable<OrderDTO>>(orders);
            return orderDTOs;
        }

        public async Task<OrderDTO> CreateOrder(OrderCreationDTO orderCreationDto)
        {
            // Map the OrderCreationDTO to an Order entity
            var order = _mapper.Map<Order>(orderCreationDto);

            // Seting the default values
            order.Id = Guid.NewGuid().ToString();
            order.PartitionKey = order.UserId;
            order.OrderDate = DateTime.Now;

            // Save the new order to the database using the repository
            await _orderRepository.Add(order);
            await _orderRepository.SaveChanges();

            // Map the Order entity back to OrderDTO for the response
            var createdOrderDto = _mapper.Map<OrderDTO>(order);

            return createdOrderDto;
        }

        public async Task UpdateOrderStatus(OrderDTO orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);
            await _orderRepository.Update(orderDto.Id, order);
        }

        public async Task<OrderDTO> GetOrderByIds(string orderId, string userId)
        {
            var order = await _orderRepository.GetSingleWithPartitionKey(orderId, userId);
            return _mapper.Map<OrderDTO>(order);
        }

        public async Task AddOrder(OrderDTO orderDto)
        {
            if (orderDto == null)
            {
                throw new ArgumentNullException(nameof(orderDto));
            }

            var order = _mapper.Map<Order>(orderDto);
            await _orderRepository.Add(order);
        }

        public async Task<OrderDTO> GetOrderById(string orderId)
        {
            var order = await _orderRepository.GetSingle(orderId);
            return _mapper.Map<OrderDTO>(order);
        }
    }
}
