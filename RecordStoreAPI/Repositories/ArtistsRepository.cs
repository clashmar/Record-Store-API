using Microsoft.EntityFrameworkCore;
using RecordStoreAPI.Data;
using RecordStoreAPI.Entities;
using RecordStoreAPI.Models;
using RecordStoreFrontend.Client.Models;

namespace RecordStoreAPI.Repositories
{
    public interface IArtistsRepository
    {
        List<Artist> FindAllArtists();
        List<Album>? FindAlbumsByArtistId(int id);
        Artist? FindArtistById(int id);
        Artist? AddNewArtist(ArtistDetails artistDetails);
        Artist? UpdateArtist(int id, ArtistDetails artistDetails);
        bool TryRemoveArtistById(int id);
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

        public Artist? AddNewArtist(ArtistDetails artistDetails)
        {
            if(CheckArtistExists(artistDetails.Name)) return null;

            Artist artist = ModelExtensions.ArtistDetailsToArtist(artistDetails);

            _db.Artists.Add(artist);
            _db.SaveChanges();

            return artist;
        }

        public Artist? UpdateArtist(int id, ArtistDetails artistDetails)
        {
            var artistToUpdate = _db.Artists.FirstOrDefault(a => a.Id == id);
            if (artistToUpdate == null) return null;

            ModelExtensions.MapArtistDetailsProperties(artistToUpdate, artistDetails);

            _db.Update(artistToUpdate);
            _db.SaveChanges();

            return artistToUpdate;
        }
        public bool TryRemoveArtistById(int id)
        {
            try
            {
                Artist? artist = _db.Artists.FirstOrDefault(a => a.Id == id);
                if (artist == null) return false;
                _db.Remove(artist);
                _db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            
        }
        private bool CheckArtistExists(string name)
        {
            if(_db.Artists.Any(a => a.Name.ToLower() == name.ToLower())) return true;
            return false;
        }
    }
}
