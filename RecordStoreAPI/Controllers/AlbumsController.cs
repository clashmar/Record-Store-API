using Microsoft.AspNetCore.Mvc;
using RecordStoreAPI.Entities;
using RecordStoreFrontend.Client.Models;
using RecordStoreAPI.Services;

namespace RecordStoreAPI.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class AlbumsController : ControllerBase
    {
        private readonly IAlbumsService _albumsService;

        public AlbumsController(IAlbumsService albumsService)
        {
            _albumsService = albumsService;
        }

        [HttpGet]
        public IActionResult GetAllAlbums()
        {
            var result = _albumsService.FindAllAlbums();
            return result != null && result.Count > 0 ? Ok(result) : NotFound("Could not access the database.");
        }

        [HttpGet("{id}")]
        public IActionResult GetAlbumById(int id)
        {
            var result = _albumsService.FindAlbumById(id);
            return result != null ? Ok(result) : BadRequest("No albums were found with that Id.");
        }

        [HttpPost]
        public IActionResult PostNewAlbum(AlbumDetails album)
        {
            if (album == null || !ModelState.IsValid) return BadRequest("Invalid input.");
            var result = _albumsService.AddNewAlbum(album);
            return result != null ? Ok(result) : NotFound("Could not process the request.");
        }

        [HttpPut("{id}")]
        public IActionResult PutAlbumById(int id, AlbumDetails album)
        {
            if (album == null || !ModelState.IsValid) return BadRequest("Invalid input.");
            var result = _albumsService.UpdateAlbum(id, album);
            return result != null ? Ok(result) : NotFound("Could not process the request.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAlbumById(int id)
        {
            return _albumsService.TryRemoveAlbumById(id) ? Ok("Album was removed.") : BadRequest("No album was found with that Id.");
        }

        [HttpGet("Year/{releaseYear}")]
        public IActionResult GetAlbumsByReleaseYear(int releaseYear)
        {
            var result = _albumsService.FindAlbumsByReleaseYear(releaseYear);
            return result != null && result.Count > 0 ? Ok(result) : NotFound($"We don't have any albums from {releaseYear}.");
        }

        [HttpGet("Genre/{genre}")]
        public IActionResult GetAlbumsByGenre(GenreEnum genre)
        {
            var result = _albumsService.FindAlbumsByGenre(genre);
            return result != null && result.Count > 0 ? Ok(result) : NotFound($"We don't have any {Genres.ToFriendlyString(genre)} albums in stock.");
        }

        [HttpGet("Search")]
        public IActionResult GetAlbumByName(string name)
        {
            var result = _albumsService.FindAlbumsByName(name);
            return result != null && result.Count > 0 ? Ok(result) : NotFound("We couldn't find an album matching your search.");
        }
    }
}
