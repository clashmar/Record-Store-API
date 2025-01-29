using RecordStoreAPI.Entities;
using RecordStoreAPI.Models;
using RecordStoreFrontend.Client.Models;
using RecordStoreAPI.Repositories;

namespace RecordStoreAPI.Services
{
    public interface IAlbumsService
    {
        List<AlbumDetails> FindAllAlbums();
        AlbumDetails? FindAlbumById(int id);
        AlbumDetails? AddNewAlbum(AlbumDetails album);
        AlbumDetails? UpdateAlbum(int id, AlbumDetails album);
        bool TryRemoveAlbumById(int id);
        List<AlbumDetails> FindAlbumsByReleaseYear(int releaseYear);
        List<AlbumDetails> FindAlbumsByGenre(GenreEnum genre);
        List<AlbumDetails> FindAlbumsByName(string name);

        List<SearchResult>? FindSearchResults(string searchTerm);
    }
    public class AlbumsService : IAlbumsService
    {
        private readonly IAlbumsRepository _albumsRepository;

        public AlbumsService(IAlbumsRepository albumsRepository)
        {
            _albumsRepository = albumsRepository;
        }

        public List<AlbumDetails> FindAllAlbums()
        {
            return _albumsRepository.FindAllAlbums()
                .Select(a => ModelExtensions.ToAlbumDetails(a))
                .ToList() ?? [];
        }

        public AlbumDetails? FindAlbumById(int id)
        {
            Album? album = _albumsRepository.FindAlbumById(id);
            return album != null ? ModelExtensions.ToAlbumDetails(album) : null;
        }

        public AlbumDetails? AddNewAlbum(AlbumDetails newAlbum)
        {
            Album? album = _albumsRepository.AddNewAlbum(newAlbum);
            return album != null ? ModelExtensions.ToAlbumDetails(album) : null;
        }

        public AlbumDetails? UpdateAlbum(int id, AlbumDetails album)
        {
            Album? updatedAlbum = _albumsRepository.UpdateAlbum(id, album);
            return updatedAlbum != null ? ModelExtensions.ToAlbumDetails(updatedAlbum) : null;
        }

        public bool TryRemoveAlbumById(int id)
        {
            return _albumsRepository.TryRemoveAlbumById(id);
        }

        public List<AlbumDetails> FindAlbumsByReleaseYear(int releaseYear)
        {
            List<Album>? albums = _albumsRepository.FindAlbumsByReleaseYear(releaseYear);
            return albums?.Select(a => ModelExtensions.ToAlbumDetails(a)).ToList() ?? [];
        }

        public List<AlbumDetails> FindAlbumsByGenre(GenreEnum genre)
        {
            List<Album>? albums = _albumsRepository.FindAlbumsByGenre(genre);
            return albums?.Select(a => ModelExtensions.ToAlbumDetails(a)).ToList() ?? [];
        }

        public List<AlbumDetails> FindAlbumsByName(string name)
        {
            return _albumsRepository.FindAllAlbums()
                .Where(a => a.Name.ToLower().Contains(name.ToLower()))
                .Select(a => ModelExtensions.ToAlbumDetails(a))
                .ToList() ?? [];
        }

        public List<SearchResult>? FindSearchResults(string searchTerm)
        {
            return _albumsRepository.FindSearchResults(searchTerm);
        }
    }
}
