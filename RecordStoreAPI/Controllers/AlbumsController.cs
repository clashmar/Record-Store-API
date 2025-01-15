using Microsoft.AspNetCore.Mvc;
using RecordStoreAPI.Models;
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
            var result = _albumService.FindAllAlbums();
            return result != null && result.Count > 0 ? Ok(result) : NotFound("Cannot find albums.");
        }

        [HttpGet("{id}")]
        public IActionResult GetAlbumById(int id)
        {
            var result = _albumService.FindAlbumById(id);
            return result != null ? Ok(result) : BadRequest("No albums were found with that Id.");
        }

        [HttpPost]
        public IActionResult PostNewAlbum(AlbumPutDto album)
        {
            if (album == null || !ModelState.IsValid) return BadRequest("Invalid input.");
            var result = _albumService.AddNewAlbum(album);
            return result != null ? Ok(result) : NotFound("Could not process the request.");
        }

        [HttpPut("{id}")]
        public IActionResult PutAlbumById(int id, AlbumPutDto album)
        {
            if (album == null || !ModelState.IsValid) return BadRequest("Invalid input.");
            var result = _albumService.UpdateAlbum(id, album);
            return result != null ? Ok(result) : NotFound("Could not process the request.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAlbumById(int id)
        {
            return _albumService.TryRemoveAlbumById(id) ? Ok("Album was removed.") : BadRequest("No album was found with that Id.");
        }
    }
}
