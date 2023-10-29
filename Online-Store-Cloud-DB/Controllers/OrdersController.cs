using Microsoft.AspNetCore.Mvc;
using Service.DTOs;
using Service;
using Azure.Storage.Queues;
using System.Text.Json;

namespace Online_Store_Cloud_DB.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        private readonly QueueClient _queueClient;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IOrderService orderService, QueueClient queueClient, ILogger<OrdersController> logger)
        {
            _orderService = orderService;
            _queueClient = queueClient;
            _logger = logger;
        }
        // GET: api/orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrders();

            if (orders == null)
            {
                return NotFound();
            }

            return Ok(orders);
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrder(OrderCreationDTO orderCreationDto)
        {
            try
            {
                // Convert the orderCreationDto into a string message. 
                var orderMessage = JsonSerializer.Serialize(orderCreationDto);

                // Send the message to the Azure Queue
                await _queueClient.SendMessageAsync(orderMessage);

                // Return a response to the user indicating the order is being processed
                return Accepted($"Order for user {orderCreationDto.UserId} is being processed.");
            }
            catch (Exception ex)  // Catch general exceptions
            {
                // Log the error.
                _logger.LogError(ex, "An error occurred while processing the order.");

                // Return a general error message to the client.
                return StatusCode(500, "An error occurred while processing your request. Please try again later.");
            }
        }



        // more endpoints here as needed.
    }
}
