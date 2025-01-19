using RecordStoreAPI.Models;
using RecordStoreAPI.Repositories;

namespace RecordStoreAPI.Services
{
    public interface IArtistsService
    {
        List<Artist>? FindAllArtists();
        List<AlbumReturnDto>? FindAlbumsByArtistId(int id);
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
        public List<AlbumReturnDto>? FindAlbumsByArtistId(int id)
        {
            return _artistsRepository.FindAlbumsByArtistId(id);
        }
    }
}
