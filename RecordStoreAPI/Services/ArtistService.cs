using RecordStoreAPI.Models;
using RecordStoreAPI.Repositories;

namespace RecordStoreAPI.Services
{
    public interface IArtistService
    {
        List<Artist>? FindAllArtists();
    }
    public class ArtistService : IArtistService
    {
        private readonly IArtistRepository _artistRepository;

        public ArtistService(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
        }

        public List<Artist>? FindAllArtists()
        {
            return _artistRepository.FindAllArtists().ToList();
        }
        //public List<AlbumDto>? FindAlbumsByArtistId(int id)
        //{
        //    return _artistRepository.FindAlbumsByArtistId()
        //        .Select(a => ModelExtensions.ToAlbumDto(a))
        //        .ToList();
        //}

    }
}
