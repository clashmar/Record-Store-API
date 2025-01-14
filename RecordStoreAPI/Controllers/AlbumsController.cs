using Microsoft.AspNetCore.Mvc;
using RecordStoreAPI.Services;

namespace RecordStoreAPI.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class AlbumsController : ControllerBase
    {
        private readonly IAlbumService _albumService;

        public AlbumsController(IAlbumService albumService)
        {
            _albumService = albumService;
        }

        [HttpGet]
        public IActionResult GetAllAlbums()
        {
            var result = _albumService.GetAllAlbums();
            if(result.Boolean) return Ok(result.Value);
            return BadRequest();
        }
    }
}
