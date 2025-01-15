using RecordStoreAPI.Data;
using RecordStoreAPI.Models;

namespace RecordStoreAPI.Repositories
{
    public interface IAlbumRepository
    {
        IEnumerable<Album> FindAllAlbums();
        Album? FindAlbumById(int id);
    }
    public class AlbumRepository : IAlbumRepository
    {
        private readonly ApplicationDbContext _db;

        public AlbumRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Album> FindAllAlbums()
        {
            return _db.Albums;
        }

        public Album? FindAlbumById(int id)
        {
            return _db.Albums.FirstOrDefault(a => a.Id == id);
        }
    }
}
