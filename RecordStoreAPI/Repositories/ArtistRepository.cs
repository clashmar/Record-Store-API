using RecordStoreAPI.Data;
using RecordStoreAPI.Models;

namespace RecordStoreAPI.Repositories
{
    public interface IArtistRepository
    {
        IEnumerable<Artist> FindAllArtists();
    }
    public class ArtistRepository : IArtistRepository
    {
        private readonly ApplicationDbContext _db;

        public ArtistRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Artist> FindAllArtists()
        {
            return _db.Artists;
        }
    }
}
