using RecordStoreAPI.Models;
using RecordStoreAPI.Repositories;

namespace RecordStoreAPI.Services
{
    public interface IAlbumService
    {
        (bool Boolean, List<Album>? Value) GetAllAlbums();
    }
    public class AlbumService : IAlbumService
    {
        private readonly IAlbumRepository _albumRepository;

        public AlbumService(IAlbumRepository albumRepository)
        {
            _albumRepository = albumRepository;
        }

        public (bool Boolean, List<Album>? Value) GetAllAlbums()
        {
            List<Album>? alllAlbums = _albumRepository.GetAllAlbums()!.ToList();
            if(alllAlbums != null) return (true, alllAlbums);
            return (false, alllAlbums);
        }
    }
}
