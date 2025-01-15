using Microsoft.AspNetCore.Mvc;
using RecordStoreAPI.Services;

namespace RecordStoreAPI.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        public IActionResult GetAllGenres()
        {
            var result = _genreService.FindAllGenres();
            if (result != null && result.Count > 0) return Ok(result);
            return BadRequest("There are no genres.");
        }
    }
}
