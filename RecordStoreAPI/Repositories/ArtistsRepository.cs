using Microsoft.EntityFrameworkCore;
using RecordStoreAPI.Data;
using RecordStoreAPI.Entities;

namespace RecordStoreAPI.Repositories
{
    public interface IArtistsRepository
    {
        List<Artist> FindAllArtists();
        List<Album>? FindAlbumsByArtistId(int id);

        Artist? FindArtistById(int id);
    }
    public class ArtistsRepository : IArtistsRepository
    {
        private readonly ApplicationDbContext _db;

        public ArtistsRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<Artist> FindAllArtists()
        {
            return _db.Artists
                .Include(a => a.Albums)!
                .ThenInclude(a => a.AlbumGenres)!
                .ThenInclude(ag => ag.Genre)
                .OrderBy(a => a.Name)
                .ToList();
        }

        public Artist? FindArtistById(int id)
        {
            return _db.Artists
                .Include(a => a.Albums)!
                .ThenInclude(a => a.AlbumGenres)!
                .ThenInclude(ag => ag.Genre)
                .Where(a => a.Id == id) 
                .FirstOrDefault();
        }

        public List<Album>? FindAlbumsByArtistId(int id)
        {
            Artist? artist = _db.Artists
                .Include(a => a.Albums)!
                .ThenInclude(a => a.AlbumGenres)!
                .ThenInclude(ag => ag.Genre)
                .FirstOrDefault(a => a.Id == id);

            return artist?.Albums;
        }
    }
}
