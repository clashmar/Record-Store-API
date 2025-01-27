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
        ArtistReturnDto? AddNewArtist(ArtistDetails newArtist);
        ArtistReturnDto? UpdateArtist(int id, ArtistDetails artist);
        bool TryRemoveArtistById(int id);
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
                .Select(a => DTOExtensions.ToArtistReturnDto(a))
                .ToList();
        }

        public ArtistReturnDto? FindArtistById(int id)
        {
            Artist? artist = _artistsRepository.FindArtistById(id);
            return artist != null ? DTOExtensions.ToArtistReturnDto(artist) : null;
                
        }
        public List<AlbumReturnDto>? FindAlbumsByArtistId(int id)
        {
            return _artistsRepository.FindAlbumsByArtistId(id)!
                .Where(a => a.ArtistID == id)   
                .Select(a => DTOExtensions.ToAlbumReturnDto(a))
                .ToList();  
        }
        public ArtistReturnDto? AddNewArtist(ArtistDetails newArtist)
        {
            Artist? artist = _artistsRepository.AddNewArtist(newArtist);
            return artist != null ? DTOExtensions.ToArtistReturnDto(artist) : null;
        }

        public ArtistReturnDto? UpdateArtist(int id, ArtistDetails artist)
        {
            Artist? updatedArtist = _artistsRepository.UpdateArtist(id, artist);
            return updatedArtist != null ? DTOExtensions.ToArtistReturnDto(updatedArtist) : null;
        }
        public bool TryRemoveArtistById(int id)
        {
            return _artistsRepository.TryRemoveArtistById(id);
        }
    }
}
