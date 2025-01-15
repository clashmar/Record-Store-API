using Microsoft.AspNetCore.Mvc;
using RecordStoreAPI.Data;

namespace RecordStoreAPI.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class HealthController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public HealthController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult APICheck()
        {
            return Ok("API is Healthy.");
        }

        [HttpGet("Database")]
        public IActionResult DatabaseCheck()
        {
            return _db.Albums.FirstOrDefault() != null 
                ? Ok("Database is Healthy.") 
                : NotFound("Database is not Healthy.");
        }
    }
}
