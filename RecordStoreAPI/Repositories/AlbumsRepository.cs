using Microsoft.EntityFrameworkCore;
using RecordStoreAPI.Data;
using RecordStoreAPI.Entities;

namespace RecordStoreAPI.Repositories
{
    public interface IAlbumsRepository
    {
        List<Album> FindAllAlbums();
        Album? FindAlbumById(int id);
        Album? AddNewAlbum(AlbumPutDto albumDto);
        Album? UpdateAlbum(int id, AlbumPutDto album);
        bool TryRemoveAlbumById(int id);
        List<Album>? FindAlbumsByReleaseYear(int releaseYear);
        List<Album>? FindAlbumsByGenre(Genres genre);
    }
    public class AlbumsRepository : IAlbumsRepository
    {
        private readonly ApplicationDbContext _db;

        public AlbumsRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<Album> FindAllAlbums()
        {
            return _db.Albums
                .Include(a => a.Artist)
                .Include(a => a.AlbumGenres)!
                .ThenInclude(ag => ag.Genre)
                .ToList();
        }

        public Album? FindAlbumById(int id)
        {
            return _db.Albums
                .Include(a => a.Artist)
                .Include(a => a.AlbumGenres)!
                .ThenInclude(ag => ag.Genre)
                .FirstOrDefault(a => a.Id == id);
        }

        public Album? AddNewAlbum(AlbumPutDto albumPutDto)
        {
            Artist? artist = CheckArtistExists(albumPutDto.ArtistID);
            if (artist == null) return null;

            Album album = DTOExtensions.PutDtoToAlbum(albumPutDto);

            _db.Attach(album);

            album.AlbumGenres = albumPutDto.Genres
                .Select(g => new AlbumGenre() { AlbumID = album.Id, GenreID = g })
                .ToList();

            _db.Albums.Add(album);
            _db.SaveChanges();

            return album;
        }

        public Album? UpdateAlbum(int id, AlbumPutDto albumPutDto)
        {
            var albumToUpdate = _db.Albums.FirstOrDefault(a => a.Id == id);
            if (albumToUpdate == null) return null;

            Artist? artist = CheckArtistExists(albumPutDto.ArtistID);
            if (artist == null) return null;

            _db.AlbumGenre.RemoveRange(_db.AlbumGenre.Where(ag => ag.AlbumID == albumToUpdate.Id));

            DTOExtensions.MapAlbumProperties(albumToUpdate, albumPutDto);

            _db.Update(albumToUpdate);
            _db.SaveChanges();

            return albumToUpdate;
        }

        public bool TryRemoveAlbumById(int id)
        {
            Album? album = _db.Albums.FirstOrDefault(a => a.Id == id);
            if (album == null) return false;
            _db.Remove(album);
            _db.SaveChanges();
            return true;
        }
        public List<Album>? FindAlbumsByReleaseYear(int releaseYear)
        {
            return _db.Albums
                .Include(a => a.Artist)
                .Include(a => a.AlbumGenres)!
                .ThenInclude(ag => ag.Genre)
                .Where(a => a.ReleaseYear == releaseYear)
                .ToList();
        }

        public List<Album>? FindAlbumsByGenre(Genres genre)
        {
            return _db.Albums
                .Include(a => a.Artist)
                .Include(a => a.AlbumGenres)!
                .ThenInclude(ag => ag.Genre)
                .Where(a => a.AlbumGenres!.Any(g => g.GenreID == genre))
                .ToList();
        }

        public Artist? CheckArtistExists(int artistID)
        {
            Artist? artist = _db.Artists.Where(a => a.ArtistID == artistID).FirstOrDefault();
            return artist ?? null;
        }
        public bool CheckGenreExists(Genres genre)
        {
            return _db.Genres.FirstOrDefault(g => g.GenreID == genre) != null;
        }
    }
}
