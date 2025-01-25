using RecordStoreAPI.Entities;
using RecordStoreAPI.Models;
using RecordStoreAPI.Repositories;
using RecordStoreFrontend.Client.Models;

namespace RecordStoreAPI.Services
{
    public interface IArtistsService
    {
        List<ArtistDto>? FindAllArtists();
        List<AlbumReturnDto>? FindAlbumsByArtistId(int id);
    }
    public class ArtistsService : IArtistsService
    {
        private readonly IArtistsRepository _artistsRepository;

        public ArtistsService(IArtistsRepository artistsRepository)
        {
            _artistsRepository = artistsRepository;
        }

        public List<ArtistDto>? FindAllArtists()
        {
            return _artistsRepository.FindAllArtists()
                .Select(a => DTOExtensions.ToArtistDto(a))
                .ToList();
        }
        public List<AlbumReturnDto>? FindAlbumsByArtistId(int id)
        {
            List<Album>? albums = _artistsRepository.FindAlbumsByArtistId(id);
            return albums?.Select(a => DTOExtensions.ToAlbumReturnDto(a)).ToList();
        }
    }
}
