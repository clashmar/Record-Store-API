using Microsoft.AspNetCore.Mvc;
using RecordStoreAPI.Services;

namespace RecordStoreAPI.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class HealthController : ControllerBase
    {
        private readonly IAlbumService _albumService;

        public HealthController(IAlbumService albumService)
        {
            _albumService = albumService;
        }

        [HttpGet]
        public IActionResult APICheck()
        {
            return Ok("API is Healthy.");
        }

        [HttpGet("Database")]
        public IActionResult DatabaseCheck()
        {
            if (_albumService.FindAllAlbums() == null) return NotFound("Database is Not Responding");
            return Ok("Controller is Healthy.");
        }
    }
}
