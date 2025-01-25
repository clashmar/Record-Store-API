﻿using RecordStoreAPI.Entities;
using RecordStoreAPI.Models;
using RecordStoreFrontend.Client.Models;
using RecordStoreAPI.Repositories;

namespace RecordStoreAPI.Services
{
    public interface IAlbumsService
    {
        List<AlbumReturnDto> FindAllAlbums();
        AlbumReturnDto? FindAlbumById(int id);
        AlbumReturnDto? AddNewAlbum(AlbumDetails album);
        AlbumReturnDto? UpdateAlbum(int id, AlbumDetails album);
        bool TryRemoveAlbumById(int id);
        List<AlbumReturnDto> FindAlbumsByReleaseYear(int releaseYear);
        List<AlbumReturnDto> FindAlbumsByGenre(GenreEnum genre);
        List<AlbumReturnDto> FindAlbumsByName(string name);
    }
    public class AlbumsService : IAlbumsService
    {
        private readonly IAlbumsRepository _albumsRepository;

        public AlbumsService(IAlbumsRepository albumsRepository)
        {
            _albumsRepository = albumsRepository;
        }

        public List<AlbumReturnDto> FindAllAlbums()
        {
            return _albumsRepository.FindAllAlbums()
                .Select(a => DTOExtensions.ToAlbumReturnDto(a))
                .ToList() ?? [];
        }

        public AlbumReturnDto? FindAlbumById(int id)
        {
            Album? album = _albumsRepository.FindAlbumById(id);
            return album != null ? DTOExtensions.ToAlbumReturnDto(album) : null;
        }

        public AlbumReturnDto? AddNewAlbum(AlbumDetails newAlbum)
        {
            Album? album = _albumsRepository.AddNewAlbum(newAlbum);
            return album != null ? DTOExtensions.ToAlbumReturnDto(album) : null;
        }

        public AlbumReturnDto? UpdateAlbum(int id, AlbumDetails album)
        {
            Album? updatedAlbum = _albumsRepository.UpdateAlbum(id, album);
            return updatedAlbum != null ? DTOExtensions.ToAlbumReturnDto(updatedAlbum) : null;
        }

        public bool TryRemoveAlbumById(int id)
        {
            return _albumsRepository.TryRemoveAlbumById(id);
        }

        public List<AlbumReturnDto> FindAlbumsByReleaseYear(int releaseYear)
        {
            List<Album>? albums = _albumsRepository.FindAlbumsByReleaseYear(releaseYear);
            return albums?.Select(a => DTOExtensions.ToAlbumReturnDto(a)).ToList() ?? [];
        }

        public List<AlbumReturnDto> FindAlbumsByGenre(GenreEnum genre)
        {
            List<Album>? albums = _albumsRepository.FindAlbumsByGenre(genre);
            return albums?.Select(a => DTOExtensions.ToAlbumReturnDto(a)).ToList() ?? [];
        }

        public List<AlbumReturnDto> FindAlbumsByName(string name)
        {
            return _albumsRepository.FindAllAlbums()
                .Where(a => a.Name.ToLower().Contains(name.ToLower()))
                .Select(a => DTOExtensions.ToAlbumReturnDto(a))
                .ToList() ?? [];
        }
    }
}
