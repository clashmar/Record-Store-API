using Microsoft.EntityFrameworkCore;
using RecordStoreAPI.Data;
using RecordStoreAPI.Models;

namespace RecordStoreAPI.Repositories
{
    public interface IArtistsRepository
    {
        IEnumerable<Artist> FindAllArtists();
        List<Album>? FindAlbumsByArtistId(int id);
    }
    public class ArtistsRepository : IArtistsRepository
    {
        private readonly ApplicationDbContext _db;

        public ArtistsRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Artist> FindAllArtists()
        {
            return _db.Artists
                .Include(a => a.Albums);
        }

        public List<Album>? FindAlbumsByArtistId(int id)
        {
            Artist? artist = _db.Artists
                .Include(a => a.Albums)
                .FirstOrDefault(a => a.ArtistID == id);

            return artist?.Albums;
        }
    }
}
