using Microsoft.EntityFrameworkCore;
using RecordStoreAPI.Data;
using RecordStoreAPI.Entities;
using RecordStoreAPI.Models;
using RecordStoreFrontend.Client.Interfaces;
using RecordStoreFrontend.Client.Models;

namespace RecordStoreAPI.Repositories
{
    public interface IAlbumsRepository
    {
        List<Album> FindAllAlbums();
        Album? FindAlbumById(int id);
        Album? AddNewAlbum(AlbumDetails albumDetails);
        Album? UpdateAlbum(int id, AlbumDetails albumDetails);
        bool TryRemoveAlbumById(int id);
        List<Album>? FindAlbumsByReleaseYear(int releaseYear);
        List<Album>? FindAlbumsByGenre(GenreEnum genre);
        List<SearchResult>? FindSearchResults(string searchTerm);
    }
    public class AlbumsRepository : IAlbumsRepository
    {
        private readonly ApplicationDbContext _db;

        public AlbumsRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<Album> FindAllAlbums()
        {
            return [];
            return _db.Albums
                .Include(a => a.Artist)
                .Include(a => a.AlbumGenres)!
                .ThenInclude(ag => ag.Genre)
                .OrderBy(a => a.Name)
                .ToList();
        }

        public Album? FindAlbumById(int id)
        {
            return _db.Albums
                .Include(a => a.Artist)
                .Include(a => a.AlbumGenres)!
                .ThenInclude(ag => ag.Genre)
                .FirstOrDefault(a => a.Id == id);
        }

        public Album? AddNewAlbum(AlbumDetails albumDetails)
        {
            Artist? artist = CheckArtistExists(albumDetails.ArtistName);
            if (artist == null) return null; albumDetails.ArtistID = artist.Id;

            Album album = albumDetails.ToAlbum();

            album.AlbumGenres = albumDetails.Genres
                .Select(g => new AlbumGenre() { AlbumID = album.Id, GenreID = g })
                .ToList();

            _db.Albums.Add(album);
            _db.SaveChanges();

            return album;
        }

        public Album? UpdateAlbum(int id, AlbumDetails albumDetails)
        {
            var albumToUpdate = _db.Albums.FirstOrDefault(a => a.Id == id);
            if (albumToUpdate == null) return null; 

            Artist? artist = CheckArtistExists(albumDetails.ArtistName);
            if (artist == null) return null; albumDetails.ArtistID = artist.Id;

            albumToUpdate.MapToAlbum(albumDetails);

            _db.AlbumGenre.RemoveRange(_db.AlbumGenre.Where(ag => ag.AlbumID == albumToUpdate.Id));

            _db.Update(albumToUpdate);
            _db.SaveChanges();

            return albumToUpdate;
        }

        public bool TryRemoveAlbumById(int id)
        {
            Album? album = _db.Albums.FirstOrDefault(a => a.Id == id);
            if (album == null) return false;
            _db.Remove(album);
            _db.SaveChanges();
            return true;
        }

        public List<Album>? FindAlbumsByReleaseYear(int releaseYear)
        {
            return _db.Albums
                .Include(a => a.Artist)
                .Include(a => a.AlbumGenres)!
                .ThenInclude(ag => ag.Genre)
                .Where(a => a.ReleaseYear == releaseYear)
                .ToList();
        }

        public List<Album>? FindAlbumsByGenre(GenreEnum genre)
        {
            return _db.Albums
                .Include(a => a.Artist)
                .Include(a => a.AlbumGenres)!
                .ThenInclude(ag => ag.Genre)
                .Where(a => a.AlbumGenres!.Any(g => g.GenreID == genre))
                .ToList();
        }

        public List<SearchResult>? FindSearchResults(string searchTerm)
        {
            List<SearchResult> results = [];

            var artistResults = _db.Artists
                .Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()))
                .Select(a => a.ToSearchResult())
                .ToList();

            results.AddRange(artistResults);

            var albumResults = _db.Albums
                .Include(a => a.Artist)
                .Include(a => a.AlbumGenres)!
                .ThenInclude(ag => ag.Genre)
                .Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()))
                .Select(a => a.ToSearchResult())
                .ToList();

            results.AddRange(albumResults);

            return results;
        }

        public Artist? CheckArtistExists(string artistName)
        {
            Artist? artist = _db.Artists.Where(a => a.Name.ToLower() == artistName.ToLower()).FirstOrDefault();
            return artist ?? null;
        }
        public bool CheckGenreExists(GenreEnum genre)
        {
            return _db.Genres.FirstOrDefault(g => g.GenreID == genre) != null;
        }
    }
}
