using RecordStoreAPI.Data;
using RecordStoreAPI.Models;

namespace RecordStoreAPI.Repositories
{
    public interface IGenresRepository
    {
        IEnumerable<Genre> FindAllGenres();
    }
    public class GenresRepository : IGenresRepository
    {
        private readonly ApplicationDbContext _db;
        public GenresRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Genre> FindAllGenres()
        {
            return _db.Genres;
        }
    }
}
