using RecordStoreAPI.Models;
using RecordStoreAPI.Repositories;

namespace RecordStoreAPI.Services
{
    public interface IAlbumService
    {
        List<Album> FindAllAlbums();
    }
    public class AlbumService : IAlbumService
    {
        private readonly IAlbumRepository _albumRepository;

        public AlbumService(IAlbumRepository albumRepository)
        {
            _albumRepository = albumRepository;
        }

        public List<Album> FindAllAlbums()
        {
            return _albumRepository.FindAllAlbums().ToList();
        }
    }
}
