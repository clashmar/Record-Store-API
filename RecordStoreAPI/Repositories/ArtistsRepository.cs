using RecordStoreAPI.Data;
using RecordStoreAPI.Models;

namespace RecordStoreAPI.Repositories
{
    public interface IArtistsRepository
    {
        IEnumerable<Artist> FindAllArtists();
        List<AlbumReturnDto>? FindAlbumsByArtistId(int id);
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
            return _db.Artists;
        }
        public List<AlbumReturnDto>? FindAlbumsByArtistId(int id)
        {
            Artist? artist = _db.Artists.FirstOrDefault(a => a.ArtistID == id)!;
            if(artist == null) return null;

            return _db.Albums.Where(album => album.ArtistID == id)
                .Select(album => ModelExtensions.ToAlbumReturnDto(album, artist.Name!))
                .ToList();
        }
    }
}
