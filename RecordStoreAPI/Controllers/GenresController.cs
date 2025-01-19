using Microsoft.AspNetCore.Mvc;
using RecordStoreAPI.Services;

namespace RecordStoreAPI.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class GenresController : ControllerBase
    {
        private readonly IGenresService _genreService;

        public GenresController(IGenresService genresService)
        {
            _genreService = genresService;
        }

        [HttpGet]
        public IActionResult GetAllGenres()
        {
            var result = _genreService.FindAllGenres();
            if (result != null && result.Count > 0) return Ok(result);
            return BadRequest("There are currenty no genres.");
        }
    }
}
