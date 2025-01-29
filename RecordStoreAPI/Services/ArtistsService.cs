using RecordStoreAPI.Entities;
using RecordStoreAPI.Models;
using RecordStoreAPI.Repositories;
using RecordStoreFrontend.Client.Models;

namespace RecordStoreAPI.Services
{
    public interface IArtistsService
    {
        List<ArtistDetails>? FindAllArtists();
        ArtistDetails? FindArtistById(int id);
        ArtistDetails? AddNewArtist(ArtistDetails newArtist);
        ArtistDetails? UpdateArtist(int id, ArtistDetails artist);
        bool TryRemoveArtistById(int id);
    }
    public class ArtistsService : IArtistsService
    {
        private readonly IArtistsRepository _artistsRepository;

        public ArtistsService(IArtistsRepository artistsRepository)
        {
            _artistsRepository = artistsRepository;
        }

        public List<ArtistDetails>? FindAllArtists()
        {
            return _artistsRepository.FindAllArtists()
                .Select(a => ModelExtensions.ToArtistDetails(a))
                .ToList();
        }

        public ArtistDetails? FindArtistById(int id)
        {
            Artist? artist = _artistsRepository.FindArtistById(id);
            return artist != null ? ModelExtensions.ToArtistDetails(artist) : null;
                
        }

        public ArtistDetails? AddNewArtist(ArtistDetails newArtist)
        {
            Artist? artist = _artistsRepository.AddNewArtist(newArtist);
            return artist != null ? ModelExtensions.ToArtistDetails(artist) : null;
        }

        public ArtistDetails? UpdateArtist(int id, ArtistDetails artist)
        {
            Artist? updatedArtist = _artistsRepository.UpdateArtist(id, artist);
            return updatedArtist != null ? ModelExtensions.ToArtistDetails(updatedArtist) : null;
        }
        public bool TryRemoveArtistById(int id)
        {
            return _artistsRepository.TryRemoveArtistById(id);
        }
    }
}
