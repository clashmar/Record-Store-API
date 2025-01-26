using RecordStoreAPI.Entities;
using RecordStoreAPI.Models;
using RecordStoreAPI.Repositories;
using RecordStoreFrontend.Client.Models;

namespace RecordStoreAPI.Services
{
    public interface IArtistsService
    {
        List<ArtistReturnDto>? FindAllArtists();
        List<AlbumReturnDto>? FindAlbumsByArtistId(int id);
        ArtistReturnDto? FindArtistById(int id);
    }
    public class ArtistsService : IArtistsService
    {
        private readonly IArtistsRepository _artistsRepository;

        public ArtistsService(IArtistsRepository artistsRepository)
        {
            _artistsRepository = artistsRepository;
        }

        public List<ArtistReturnDto>? FindAllArtists()
        {
            return _artistsRepository.FindAllArtists()
                .Select(a => DTOExtensions.ToArtistDto(a))
                .ToList();
        }

        public ArtistReturnDto? FindArtistById(int id)
        {
            Artist? artist = _artistsRepository.FindArtistById(id);
            return artist != null ? DTOExtensions.ToArtistDto(artist) : null;
                
        }
        public List<AlbumReturnDto>? FindAlbumsByArtistId(int id)
        {
            return _artistsRepository.FindAlbumsByArtistId(id)!
                .Where(a => a.ArtistID == id)   
                .Select(a => DTOExtensions.ToAlbumReturnDto(a))
                .ToList();  
        }
    }
}
