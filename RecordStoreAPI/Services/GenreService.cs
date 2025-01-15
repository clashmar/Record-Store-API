using RecordStoreAPI.Models;
using RecordStoreAPI.Repositories;

namespace RecordStoreAPI.Services
{
    public interface IGenreService
    {
        List<Genre> FindAllGenres();
    }
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public List<Genre> FindAllGenres()
        {
            return _genreRepository.FindAllGenres()
                .Select(g => { g.Name = g.Name!.Replace('_', '-'); return g; })
                .ToList();
        }
    }
}
