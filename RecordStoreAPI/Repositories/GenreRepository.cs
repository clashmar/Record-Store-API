using RecordStoreAPI.Data;
using RecordStoreAPI.Models;

namespace RecordStoreAPI.Repositories
{
    public interface IGenreRepository
    {
        IEnumerable<Genre> FindAllGenres();
    }
    public class GenreRepository : IGenreRepository
    {
        private readonly ApplicationDbContext _db;
        public GenreRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Genre> FindAllGenres()
        {
            return _db.Genres;
        }
    }
}
