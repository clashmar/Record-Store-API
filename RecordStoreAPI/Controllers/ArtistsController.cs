using Microsoft.AspNetCore.Mvc;
using RecordStoreAPI.Services;

namespace RecordStoreAPI.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class ArtistsController : ControllerBase
    {
        private readonly IArtistService _artistService;
        public ArtistsController(IArtistService artistService)
        {
            _artistService = artistService;
        }

        [HttpGet]
        public IActionResult GetAllArtists()
        {
            var result = _artistService.FindAllArtists();
            return result != null && result.Count > 0 ? Ok(result) : NotFound("Cannot find artists.");
        }
    }
}
