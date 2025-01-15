using RecordStoreAPI.Models;
using RecordStoreAPI.Repositories;

namespace RecordStoreAPI.Services
{
    public interface IAlbumService
    {
        List<AlbumDto> FindAllAlbums();
        AlbumDto? FindAlbumById(int id);
        AlbumDto? AddNewAlbum(Album album);
        AlbumDto? UpdateAlbum(int id, Album album);
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

        public AlbumDto? FindAlbumById(int id)
        {
            Album? album = _albumRepository.FindAlbumById(id);
            return album != null ? ModelExtensions.ToAlbumDto(album) : null;
        }

        public AlbumDto? AddNewAlbum(Album album)
        {
            return _albumRepository.AddNewAlbum(album) != null ? ModelExtensions.ToAlbumDto(album) : null;
        }

        public AlbumDto? UpdateAlbum(int id, Album album)
        {
            Album? updatedAlbum = _albumRepository.UpdateAlbum(id, album);
            return updatedAlbum != null ? ModelExtensions.ToAlbumDto(updatedAlbum!) : null;
        }
    }
}
