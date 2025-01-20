using RecordStoreAPI.Models;
using RecordStoreAPI.Repositories;

namespace RecordStoreAPI.Services
{
    public interface IAlbumsService
    {
        List<AlbumReturnDto>? FindAllAlbums();
        AlbumReturnDto? FindAlbumById(int id);
        AlbumReturnDto? AddNewAlbum(AlbumPutDto album);
        AlbumReturnDto? UpdateAlbum(int id, AlbumPutDto album);
        bool TryRemoveAlbumById(int id);
        List<AlbumReturnDto>? FindAlbumsByReleaseYear(int releaseYear);
        List<AlbumReturnDto>? FindAlbumsByGenre(Genres genre);
        List<AlbumReturnDto>? FindAlbumByName(string name);
    }
    public class AlbumsService : IAlbumsService
    {
        private readonly IAlbumsRepository _albumsRepository;

        public AlbumsService(IAlbumsRepository albumsRepository)
        {
            _albumsRepository = albumsRepository;
        }

        public List<AlbumReturnDto>? FindAllAlbums()
        {
            return _albumsRepository.FindAllAlbums()
                .Select(a => DTOExtensions.ToAlbumReturnDto(a))
                .ToList();
        }

        public AlbumReturnDto? FindAlbumById(int id)
        {
            Album? album = _albumsRepository.FindAlbumById(id);
            return album != null ? DTOExtensions.ToAlbumReturnDto(album) : null;
        }

        public AlbumReturnDto? AddNewAlbum(AlbumPutDto newAlbum)
        {
            Album? album = _albumsRepository.AddNewAlbum(newAlbum);
            return album != null ? DTOExtensions.ToAlbumReturnDto(album) : null;
        }

        public AlbumReturnDto? UpdateAlbum(int id, AlbumPutDto album)
        {
            Album? updatedAlbum = _albumsRepository.UpdateAlbum(id, album);
            return updatedAlbum != null ? DTOExtensions.ToAlbumReturnDto(updatedAlbum) : null;
        }

        public bool TryRemoveAlbumById(int id)
        {
            return _albumsRepository.TryRemoveAlbumById(id);
        }

        public List<AlbumReturnDto>? FindAlbumsByReleaseYear(int releaseYear)
        {
            List<Album>? albums = _albumsRepository.FindAlbumsByReleaseYear(releaseYear);
            return albums?.Select(a => DTOExtensions.ToAlbumReturnDto(a)).ToList();
        }

        public List<AlbumReturnDto>? FindAlbumsByGenre(Genres genre)
        {
            List<Album>? albums = _albumsRepository.FindAlbumsByGenre(genre);
            return albums?.Select(a => DTOExtensions.ToAlbumReturnDto(a)).ToList();
        }

        public List<AlbumReturnDto>? FindAlbumByName(string name)
        {
            return _albumsRepository.FindAllAlbums()
                .Where(a => a.Name.ToLower().Contains(name.ToLower()))
                .Select(a => DTOExtensions.ToAlbumReturnDto(a))
                .ToList();
        }
    }
}
