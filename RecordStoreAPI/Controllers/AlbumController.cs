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
            return result != null && result.Count > 0 ? Ok(result) : NotFound("Cannot find albums.");
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetAlbumById(int id)
        {
            var result = _albumService.FindAlbumById(id);
            return result != null ? Ok(result) : BadRequest("No album was found with that Id.");
        }
    }
}
