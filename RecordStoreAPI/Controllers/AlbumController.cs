using Microsoft.AspNetCore.Mvc;
using RecordStoreAPI.Services;

namespace RecordStoreAPI.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class AlbumController : ControllerBase
    {
        private readonly IAlbumService _albumService;

        public AlbumController(IAlbumService albumService)
        {
            _albumService = albumService;
        }

        

        [HttpGet]
        public IActionResult GetAllAlbums()
        {
            var result = _albumService.FindAllAlbums();
            if (result != null && result.Count > 0) return Ok(result);
            return BadRequest("There are no albums.");
        }
    }
}
