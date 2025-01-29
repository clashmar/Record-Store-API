using RecordStoreAPI.Repositories;
using RecordStoreFrontend.Client.Models;

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
                .Select(g => new GenreDto(g.GenreID, g.Name = Genres.ToFriendlyString(g.GenreID)))
                .ToList();
        }
    }
}
