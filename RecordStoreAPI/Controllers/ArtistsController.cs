using Microsoft.AspNetCore.Mvc;
using RecordStoreAPI.Services;

namespace RecordStoreAPI.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class ArtistsController : ControllerBase
    {
        private readonly IArtistsService _artistService;
        public ArtistsController(IArtistsService artistsService)
        {
            _artistService = artistsService;
        }

        [HttpGet]
        public IActionResult GetAllArtists()
        {
            var result = _artistService.FindAllArtists();
            return result != null && result.Count > 0 ? Ok(result) : NotFound("Cannot find artists.");
        }

        [HttpGet("{id}")]
        public IActionResult GetAlbumsByArtistId(int id)
        {
            var result = _artistService.FindAlbumsByArtistId(id);
            return result != null ? Ok(result) : BadRequest("No artists were found with that Id.");
        }
    }
}
