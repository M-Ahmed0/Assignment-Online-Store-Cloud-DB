using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Online_Store_Cloud_DB.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseTestController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DatabaseTestController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("testConnection")]
        public async Task<IActionResult> TestDatabaseConnection()
        {
            try
            {
                // Try to fetch all orders. We just want to see if the operation succeeds.
                var orders = await _context.Orders.ToListAsync();

                // If the above line doesn't throw an exception, the connection is likely working.
                return Ok("Database connection is working.");
            }
            catch (Exception ex)
            {
                // If there's an exception, there's likely an issue with the connection or configuration.
                return BadRequest($"Failed to connect to the database. Error: {ex.Message}");
            }
        }
    }
}
