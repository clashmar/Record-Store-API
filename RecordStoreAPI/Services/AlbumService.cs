using RecordStoreAPI.Models;
using RecordStoreAPI.Repositories;

namespace RecordStoreAPI.Services
{
    public interface IAlbumService
    {
        List<AlbumDto> FindAllAlbums();
    }
    public class AlbumService : IAlbumService
    {
        private readonly IAlbumRepository _albumRepository;

        public AlbumService(IAlbumRepository albumRepository)
        {
            _albumRepository = albumRepository;
        }

        public List<AlbumDto> FindAllAlbums()
        {
            return _albumRepository.FindAllAlbums()
                .Select(a => ModelExtensions.ToAlbumDto(a))
                .ToList();
        }
    }
}
