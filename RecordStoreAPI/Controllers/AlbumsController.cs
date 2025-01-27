using Microsoft.AspNetCore.Mvc;
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
            return result != null && result.Count > 0 ? Ok(result) : NotFound("The API could not access the database.");
        }

        [HttpGet("{id}")]
        public IActionResult GetAlbumById(int id)
        {
            var result = _albumsService.FindAlbumById(id);
            return result != null ? Ok(result) : BadRequest("The API could not access any albums with that Id.");
        }

        [HttpPost]
        public IActionResult PostNewAlbum(AlbumDetails album)
        {
            if (album == null || !ModelState.IsValid) return BadRequest("Invalid input.");
            var result = _albumsService.AddNewAlbum(album);
            return result != null ? Ok(result) : NotFound("The API could not process the request.");
        }

        [HttpPut("{id}")]
        public IActionResult PutAlbumById(int id, AlbumDetails album)
        {
            if (album == null || !ModelState.IsValid) return BadRequest("Invalid input.");
            var result = _albumsService.UpdateAlbum(id, album);
            return result != null ? Ok(result) : NotFound("The API could not process the request.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAlbumById(int id)
        {
            return _albumsService.TryRemoveAlbumById(id) ? Ok("Album was removed.") : BadRequest("Either the API could not access that artist, or the artist has still has releases in the database. Please remove these first.");
        }

        [HttpGet("Year/{releaseYear}")]
        public IActionResult GetAlbumsByReleaseYear(int releaseYear)
        {
            var result = _albumsService.FindAlbumsByReleaseYear(releaseYear);
            return result != null && result.Count > 0 ? Ok(result) : NotFound($"The API could not access any albums from {releaseYear}.");
        }

        [HttpGet("Genre/{genre}")]
        public IActionResult GetAlbumsByGenre(GenreEnum genre)
        {
            var result = _albumsService.FindAlbumsByGenre(genre);
            return result != null && result.Count > 0 ? Ok(result) : NotFound($"The API could not access any {Genres.ToFriendlyString(genre)} albums.");
        }

        //[HttpGet("Search")]
        //public IActionResult GetAlbumByName(string name)
        //{
        //    var result = _albumsService.FindAlbumsByName(name);
        //    return result != null && result.Count > 0 ? Ok(result) : NotFound("We couldn't find an album matching your search.");
        //}

        [HttpGet("Search/{searchTerm}")]
        public IActionResult GetSearchResults(string searchTerm)
        {
            var result = _albumsService.FindSearchResults(searchTerm!);
            return result != null && result.Count > 0 ? Ok(result) : NotFound("The API couldn't access any results matching your search.");
        }

        [HttpGet("TopFive")]
        public IActionResult GetTopFive()
        {
            var result = _albumsService.FindAllAlbums().GetRange(0, 5);
            return result != null && result.Count > 0 ? Ok(result) : NotFound("The API could not access the database.");
        }
    }
}
