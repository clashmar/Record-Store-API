using RecordStoreAPI.Models;
using RecordStoreAPI.Repositories;

namespace RecordStoreAPI.Services
{
    public interface IArtistsService
    {
        List<Artist>? FindAllArtists();
        List<Album>? FindAlbumsByArtistId(int id);
    }
    public class ArtistsService : IArtistsService
    {
        private readonly IArtistsRepository _artistsRepository;

        public ArtistsService(IArtistsRepository artistsRepository)
        {
            _artistsRepository = artistsRepository;
        }

        public List<Artist>? FindAllArtists()
        {
            return _artistsRepository.FindAllArtists().ToList();
        }
        public List<Album>? FindAlbumsByArtistId(int id)
        {
            return _artistsRepository.FindAlbumsByArtistId(id);
        }
    }
}
