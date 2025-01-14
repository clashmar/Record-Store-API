using RecordStoreAPI.Models;
using RecordStoreAPI.Repositories;

namespace RecordStoreAPI.Services
{
    public interface IAlbumService
    {
        (bool Boolean, IEnumerable<Album>? Value) GetAllAlbums();
    }
    public class AlbumService : IAlbumService
    {
        private readonly IAlbumRepository _albumRepository;

        public AlbumService(IAlbumRepository albumRepository)
        {
            _albumRepository = albumRepository;
        }

        public (bool Boolean, IEnumerable<Album>? Value) GetAllAlbums()
        {
            IEnumerable<Album>? alllAlbums = _albumRepository.GetAllAlbums();
            if(alllAlbums != null) return (true, alllAlbums);
            return (false, alllAlbums);
        }
    }
}
