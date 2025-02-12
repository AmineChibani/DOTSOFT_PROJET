using ClientService.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClientService.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseTestController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DatabaseTestController(AppDbContext context)
        {
            _context = context;
        }

        //Endpoint for test connection with Database
        [HttpGet("test-connection")]
        public async Task<IActionResult> TestConnection()
        {
            try
            {
                await _context.Database.OpenConnectionAsync();
                await _context.Database.CloseConnectionAsync();
                return Ok(" Database connection successful!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $" Database connection failed: {ex.Message}");
            }
        }
    }
}
