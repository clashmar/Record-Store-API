using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RecordStoreAPI.Data;
using RecordStoreAPI.Models;

namespace RecordStoreAPI.Repositories
{
    public interface IAlbumsRepository
    {
        List<Album> FindAllAlbums();
        Album? FindAlbumById(int id);
        AlbumReturnDto? AddNewAlbum(AlbumPutDto albumDto);
        AlbumReturnDto? UpdateAlbum(int id, AlbumPutDto album);
        bool TryRemoveAlbumById(int id);
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
            return _db.Albums
                .Include(a => a.Artist)
                .ToList();
        }

        //public List<AlbumReturnDto> FindAllAlbums()
        //{
        //    return _db.Albums
        //        .Join(_db.Artists,
        //        album => album.ArtistID,
        //        artist => artist.ArtistID,
        //        (album, artist) => new { Album = album, ArtistName = artist.Name }
        //        )
        //        .Select(q => DTOExtensions.ToAlbumReturnDto(q.Album, q.ArtistName!))
        //        .ToList();
        //}
        public Album? FindAlbumById(int id)
        {
            return FindAllAlbums()
                .FirstOrDefault(a => a.Id == id);
        }

        public AlbumReturnDto? AddNewAlbum(AlbumPutDto albumPutDto)
        {
            try
            {
                Artist? artist = CheckArtistExists(albumPutDto.ArtistID);
                if (artist == null) return null;

                if (!CheckGenreExists(albumPutDto.GenreID)) return null;

                Album album = DTOExtensions.PutDtoToAlbum(albumPutDto);
                _db.Albums.Add(album);
                _db.SaveChanges();
                return DTOExtensions.ToAlbumReturnDto(album, artist!.Name!);
            }
            catch
            {
                return null;
            }
        }

        public AlbumReturnDto? UpdateAlbum(int id, AlbumPutDto albumPutDto)
        {
            var albumToUpdate = _db.Albums.FirstOrDefault(a => a.Id == id);
            if (albumToUpdate == null) return null;

            Artist? artist = CheckArtistExists(albumPutDto.ArtistID);
            if (artist == null) return null;

            if (!CheckGenreExists(albumPutDto.GenreID)) return null;

            DTOExtensions.MapAlbumProperties(albumToUpdate, albumPutDto);

            _db.Update(albumToUpdate);
            _db.SaveChanges();
            return DTOExtensions.ToAlbumReturnDto(albumToUpdate, artist.Name!);
        }

        public bool TryRemoveAlbumById(int id)
        {
            Album? album = _db.Albums.FirstOrDefault(a => a.Id == id);
            if (album == null) return false;
            _db.Remove(album);
            _db.SaveChanges();
            return true;
        }

        public Artist? CheckArtistExists(int artistID)
        {
            Artist? artist = _db.Artists.Where(a => a.ArtistID == artistID).FirstOrDefault();
            return artist ?? null;
        }
        public bool CheckGenreExists(Genres genreID)
        {
            return _db.Genres.FirstOrDefault(g => g.GenreID == genreID) != null;
        }
    }
}
