using RecordStoreAPI.Models;
using RecordStoreAPI.Repositories;

namespace RecordStoreAPI.Services
{
    public interface IGenresService
    {
        List<Genre> FindAllGenres();
    }
    public class GenresService : IGenresService
    {
        private readonly IGenresRepository _genresRepository;

        public GenresService(IGenresRepository genresRepository)
        {
            _genresRepository = genresRepository;
        }

        public List<Genre> FindAllGenres()
        {
            return _genresRepository.FindAllGenres()
                .Select(g => { g.Name = Genre.ToString(g.GenreID); return g; })
                .ToList();
        }
    }
}
