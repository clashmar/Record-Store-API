using RecordStoreAPI.Models;
using RecordStoreAPI.Repositories;

namespace RecordStoreAPI.Services
{
    public interface IAlbumService
    {
        List<AlbumReturnDto> FindAllAlbums();
        AlbumReturnDto? FindAlbumById(int id);
        AlbumReturnDto? AddNewAlbum(AlbumPutDto album);
        AlbumReturnDto? UpdateAlbum(int id, AlbumPutDto album);
        bool TryRemoveAlbumById(int id);
    }
    public class AlbumService : IAlbumService
    {
        private readonly IAlbumRepository _albumRepository;

        public AlbumService(IAlbumRepository albumRepository)
        {
            _albumRepository = albumRepository;
        }

        public List<AlbumReturnDto> FindAllAlbums()
        {
            return _albumRepository.FindAllAlbums();
        }

        public AlbumReturnDto? FindAlbumById(int id)
        {
            AlbumReturnDto? album = _albumRepository.FindAlbumById(id);
            return album ?? null;
        }

        public AlbumReturnDto? AddNewAlbum(AlbumPutDto newAlbum)
        {
            AlbumReturnDto? album = _albumRepository.AddNewAlbum(newAlbum);
            return album ?? null;
        }

        public AlbumReturnDto? UpdateAlbum(int id, AlbumPutDto album)
        {
            AlbumReturnDto? updatedAlbum = _albumRepository.UpdateAlbum(id, album);
            return updatedAlbum ?? null;
        }

        public bool TryRemoveAlbumById(int id)
        {
            return _albumRepository.TryRemoveAlbumById(id);
        }
    }
}
