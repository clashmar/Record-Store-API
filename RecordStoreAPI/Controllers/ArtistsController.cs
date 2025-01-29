using Microsoft.AspNetCore.Mvc;
using RecordStoreAPI.Services;
using RecordStoreFrontend.Client.Models;

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
        public IActionResult GetArtistById(int id)
        {
            var result = _artistService.FindArtistById(id);
            return result != null ? Ok(result) : BadRequest("No artists were found with that Id.");
        }

        [HttpPost]
        public IActionResult PostNewArtist(ArtistDetails artist)
        {
            if (artist == null || !ModelState.IsValid) return BadRequest("Invalid input.");
            var result = _artistService.AddNewArtist(artist);
            return result != null ? Ok(result) : NotFound("Could not process the request.");
        }

        [HttpPut("{id}")]
        public IActionResult PutAlbumById(int id, ArtistDetails artist)
        {
            if (artist == null || !ModelState.IsValid) return BadRequest("Invalid input.");
            var result = _artistService.UpdateArtist(id, artist);
            return result != null ? Ok(result) : NotFound("Could not process the request.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteArtistById(int id)
        {
            return _artistService.TryRemoveArtistById(id) ? Ok("Artist was removed.") : BadRequest("No artist was found with that Id.");
        }
    }
}
