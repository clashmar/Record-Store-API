using RecordStoreAPI.Models;
using RecordStoreAPI.Repositories;

namespace RecordStoreAPI.Services
{
    public interface IAlbumsService
    {
        List<Album> FindAllAlbums();
        Album? FindAlbumById(int id);
        AlbumReturnDto? AddNewAlbum(AlbumPutDto album);
        AlbumReturnDto? UpdateAlbum(int id, AlbumPutDto album);
        bool TryRemoveAlbumById(int id);
        List<Album>? FindAlbumsByReleaseYear(int releaseYear);
        List<Album>? FindAlbumsByGenre(Genres genre);
        List<Album>? FindAlbumByName(string name);
    }
    public class AlbumsService : IAlbumsService
    {
        private readonly IAlbumsRepository _albumsRepository;

        public AlbumsService(IAlbumsRepository albumsRepository)
        {
            _albumsRepository = albumsRepository;
        }

        public List<Album> FindAllAlbums()
        {
            return _albumsRepository.FindAllAlbums();
        }

        public Album? FindAlbumById(int id)
        {
            Album? album = _albumsRepository.FindAlbumById(id);
            return album ?? null;
        }

        public AlbumReturnDto? AddNewAlbum(AlbumPutDto newAlbum)
        {
            AlbumReturnDto? album = _albumsRepository.AddNewAlbum(newAlbum);
            return album ?? null;
        }

        public AlbumReturnDto? UpdateAlbum(int id, AlbumPutDto album)
        {
            AlbumReturnDto? updatedAlbum = _albumsRepository.UpdateAlbum(id, album);
            return updatedAlbum ?? null;
        }

        public bool TryRemoveAlbumById(int id)
        {
            return _albumsRepository.TryRemoveAlbumById(id);
        }

        public List<Album>? FindAlbumsByReleaseYear(int releaseYear)
        {
            return _albumsRepository.FindAllAlbums()
                .Where(a => a.ReleaseYear == releaseYear)
                .ToList();
        }

        public List<Album>? FindAlbumsByGenre(Genres genre)
        {
            return _albumsRepository.FindAlbumsByGenre(genre);
        }

        public List<Album>? FindAlbumByName(string name)
        {
            return _albumsRepository.FindAllAlbums()
                .Where(a => a.Name.ToLower().Contains(name.ToLower()))
                .ToList();
        }
    }
}
