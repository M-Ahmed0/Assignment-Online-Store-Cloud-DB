using System;
using System.Net;
using System.Text.Json;
using Domain.Entities;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Service;
using Service.DTOs;

namespace MarkOrderAsShipped
{
    public class Function1
    {
        private readonly ILogger _logger;
        private readonly IOrderService _orderService;

        public Function1(ILogger<Function1> logger, IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }

        [Function("MarkOrderAsShipped")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "put")] HttpRequestData req)
        {
            _logger.LogInformation("Processing request to mark order as shipped.");

            string requestBody = await req.ReadAsStringAsync();
            var orderDto = JsonSerializer.Deserialize<OrderDTO>(requestBody);

            if (orderDto == null || string.IsNullOrEmpty(orderDto.Id) || string.IsNullOrEmpty(orderDto.UserId))
            {
                var errorResponse = req.CreateResponse(HttpStatusCode.BadRequest);
                errorResponse.WriteString("Invalid input. Please provide a valid order ID and UserId.");
                _logger.LogWarning("Invalid input received.");

                return errorResponse;
            }

            try
            {
                var existingOrder = await _orderService.GetOrderByIds(orderDto.Id, orderDto.UserId);
                if (existingOrder != null)
                {
                    existingOrder.Status = OrderStatus.Shipped;
                    existingOrder.ShippingDate = DateTime.UtcNow;
                    await _orderService.UpdateOrderStatus(existingOrder);

                    var successResponse = req.CreateResponse(HttpStatusCode.OK);
                    successResponse.WriteString($"Order {orderDto.Id} marked as shipped successfully.");
                    return successResponse;
                }
                else
                {
                    var notFoundResponse = req.CreateResponse(HttpStatusCode.NotFound);
                    notFoundResponse.WriteString($"Order {orderDto.Id} not found.");
                    return notFoundResponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error marking order as shipped: {ex.Message}");
                var errorResponse = req.CreateResponse(HttpStatusCode.InternalServerError);
                errorResponse.WriteString("An error occurred while processing the request.");
                return errorResponse;
            }
        }
    }
}
