using Azure;
using Microsoft.AspNetCore.JsonPatch;
using RecordStoreAPI.Data;
using RecordStoreAPI.Models;

namespace RecordStoreAPI.Repositories
{
    public interface IAlbumRepository
    {
        IEnumerable<Album> FindAllAlbums();
        Album? FindAlbumById(int id);
        Album? AddNewAlbum(Album album);
        Album? UpdateAlbum(int id, Album album);
        bool TryRemoveAlbumById(int id);
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

        public Album? AddNewAlbum(Album album)
        {
            try
            {
                _db.Albums.Add(album);
                _db.SaveChanges();
                return album;
            }
            catch
            {
                return null;
            }
        }

        public Album? UpdateAlbum(int id, Album album)
        {
            var albumToUpdate = FindAlbumById(id);
            if (albumToUpdate == null) return null;

            ModelExtensions.MapAlbumProperties(albumToUpdate, album);

            _db.Update(albumToUpdate);
            _db.SaveChanges();
            return albumToUpdate;
        }

        public bool TryRemoveAlbumById(int id)
        {
            Album? album = FindAlbumById(id);
            if(album == null) return false;
            _db.Remove(album);
            _db.SaveChanges();
            return true;
        }
    }
}
