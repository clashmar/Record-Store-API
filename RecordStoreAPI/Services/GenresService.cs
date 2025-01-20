using RecordStoreAPI.Models;
using RecordStoreAPI.Repositories;
using static RecordStoreAPI.Models.Genre;

namespace RecordStoreAPI.Services
{
    public interface IGenresService
    {
        List<GenreDto> FindAllGenres();
    }
    public class GenresService : IGenresService
    {
        private readonly IGenresRepository _genresRepository;

        public GenresService(IGenresRepository genresRepository)
        {
            _genresRepository = genresRepository;
        }

        public List<GenreDto> FindAllGenres()
        {
            return _genresRepository.FindAllGenres()
                .Select(g => new GenreDto(g.GenreID, g.Name = Genre.ToString(g.GenreID)))
                .ToList();
        }
    }
}
