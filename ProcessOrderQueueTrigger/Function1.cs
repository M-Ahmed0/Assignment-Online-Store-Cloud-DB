using System;
using System.Text.Json;
using Azure.Storage.Queues.Models;
using Domain.Entities;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Service;
using Service.DTOs;

namespace ProcessOrderQueueTrigger
{
    public class Function1
    {
        private readonly ILogger<Function1> _logger;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;

        public Function1(ILogger<Function1> logger, IProductService productService, IOrderService orderService)
        {
            _logger = logger;
            _productService = productService;
            _orderService = orderService;
            
        }

        [Function(nameof(Function1))]
        public async Task Run([QueueTrigger("order-queue-items", Connection = "AzureWebJobsStorage")] QueueMessage message)
        {
            try
            {
                _logger.LogInformation($"C# Queue trigger function processed: {message.MessageText}");

                // Deserialize the message into an OrderDTO object
                OrderDTO orderDto = JsonSerializer.Deserialize<OrderDTO>(message.MessageText);

                if (orderDto == null)
                {
                    _logger.LogError("Failed to deserialize order from queue message.");
                    return;
                }

                // Check if UserId is provided
                if (string.IsNullOrEmpty(orderDto.UserId))
                {
                    _logger.LogError("UserId is not provided in the order.");
                    return;
                }

                // Set the PartitionKey to the provided UserId
                orderDto.PartitionKey = orderDto.UserId;

                // Generate a new unique ID for the order if it doesn't have one
                if (string.IsNullOrEmpty(orderDto.Id))
                {
                    orderDto.Id = Guid.NewGuid().ToString();
                }

                // Fetch stock levels for all products in the order
                var productStocks = await _productService.GetProductStocks(orderDto.ProductIds);

                // Check if all products in the order are in stock
                bool allProductsInStock = orderDto.ProductIds.All(pid => productStocks.ContainsKey(pid) && productStocks[pid] > 0);

                if (allProductsInStock)
                {
                    // Add the order to the database
                    await _orderService.AddOrder(orderDto);
                    orderDto.Status = OrderStatus.Placed;
                    await _orderService.UpdateOrderStatus(orderDto);
                    _logger.LogInformation($"Order with ID {orderDto.Id} has been added to the database.");
                }
                else
                {
                    // Products are out of stock, so we mark the order as Failed
                    orderDto.Status = OrderStatus.Failed;
                    await _orderService.UpdateOrderStatus(orderDto);
                    _logger.LogWarning($"Order with ID {orderDto.Id} failed due to out-of-stock products.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error processing order: {ex.Message}");
            }
        }


        //[Function(nameof(Function1))]
        //public async Task Run([QueueTrigger("order-queue-items", Connection = "AzureWebJobsStorage")] QueueMessage message)
        //{
        //    try
        //    {
        //        _logger.LogInformation($"C# Queue trigger function processed: {message.MessageText}");

        //        // Deserialize the message into an OrderDTO object
        //        OrderDTO orderDto = JsonSerializer.Deserialize<OrderDTO>(message.MessageText);

        //        if (orderDto == null)
        //        {
        //            _logger.LogError("Failed to deserialize order from queue message.");
        //            return;
        //        }

        //        // Generate a new unique ID for the order if it doesn't have one
        //        if (string.IsNullOrEmpty(orderDto.Id))
        //        {
        //            orderDto.Id = Guid.NewGuid().ToString();
        //        }

        //        // Fetch stock levels for all products in the order
        //        var productStocks = await _productService.GetProductStocks(orderDto.ProductIds);

        //        // Check if all products in the order are in stock
        //        bool allProductsInStock = orderDto.ProductIds.All(pid => productStocks.ContainsKey(pid) && productStocks[pid] > 0);

        //        if (allProductsInStock)
        //        {
        //            // Add the order to the database
        //            await _orderService.AddOrder(orderDto);
        //            orderDto.Status = OrderStatus.Placed;
        //            await _orderService.UpdateOrderStatus(orderDto);
        //            _logger.LogInformation($"Order with ID {orderDto.Id} has been added to the database.");
        //        }
        //        else
        //        {
        //            // Products are out of stock, so we mark the order as Failed
        //            orderDto.Status = OrderStatus.Failed;
        //            await _orderService.UpdateOrderStatus(orderDto);
        //            _logger.LogWarning($"Order with ID {orderDto.Id} failed due to out-of-stock products.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Error processing order: {ex.Message}");
        //    }
        //}
    }
}
