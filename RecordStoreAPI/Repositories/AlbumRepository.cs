using RecordStoreAPI.Data;
using RecordStoreAPI.Models;

namespace RecordStoreAPI.Repositories
{
    public interface IAlbumRepository
    {
        List<AlbumReturnDto> FindAllAlbums();
        AlbumReturnDto? FindAlbumById(int id);
        AlbumReturnDto? AddNewAlbum(AlbumPutDto albumDto);
        AlbumReturnDto? UpdateAlbum(int id, AlbumPutDto album);
        bool TryRemoveAlbumById(int id);
    }
    public class AlbumRepository : IAlbumRepository
    {
        private readonly ApplicationDbContext _db;

        public AlbumRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public List<AlbumReturnDto> FindAllAlbums()
        {
            return _db.Albums
                .Select(album =>
                    ModelExtensions.ToAlbumReturnDto(album, _db.Artists.Where(x => x.ArtistID == album.ArtistID).FirstOrDefault()!.Name!)  
                ).ToList();
        }
        public AlbumReturnDto? FindAlbumById(int id)
        {
            Album? album = _db.Albums.FirstOrDefault(a => a.Id == id);
            if(album == null) return null;
            return ModelExtensions.ToAlbumReturnDto(album, _db.Artists.Where(x => x.ArtistID == album.ArtistID).FirstOrDefault()!.Name!);
        }
        public AlbumReturnDto? AddNewAlbum(AlbumPutDto albumDto)
        {
            try
            {
                Album album = ModelExtensions.PutDtoToAlbum(albumDto);
                _db.Albums.Add(album);
                _db.SaveChanges();
                return ModelExtensions.ToAlbumReturnDto(album, _db.Artists.Where(x => x.ArtistID == album.ArtistID).FirstOrDefault()!.Name!);
            }
            catch
            {
                return null;
            }
        }
        public AlbumReturnDto? UpdateAlbum(int id, AlbumPutDto albumPutDto)
        {
            var albumToUpdate = _db.Albums.FirstOrDefault(a => a.Id == id);
            if (albumToUpdate == null) return null;

            ModelExtensions.MapAlbumProperties(albumToUpdate, albumPutDto);

            _db.Update(albumToUpdate);
            _db.SaveChanges();
            return ModelExtensions.ToAlbumReturnDto(albumToUpdate, _db.Artists.Where(x => x.ArtistID == albumToUpdate.ArtistID).FirstOrDefault()!.Name!);
        }
        public bool TryRemoveAlbumById(int id)
        {
            Album? album = _db.Albums.FirstOrDefault(a => a.Id == id);
            if (album == null) return false;
            _db.Remove(album);
            _db.SaveChanges();
            return true;
        }
    }
}
