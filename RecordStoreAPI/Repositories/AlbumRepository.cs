using RecordStoreAPI.Data;
using RecordStoreAPI.Models;

namespace RecordStoreAPI.Repositories
{
    public interface IAlbumRepository
    {
        List<AlbumReturnDto> FindAllAlbums();
        AlbumReturnDto? FindAlbumById(int id);
        AlbumReturnDto? AddNewAlbum(AlbumPutDto albumDto);
        AlbumReturnDto? UpdateAlbum(int id, AlbumPutDto album);
        bool TryRemoveAlbumById(int id);
    }
    public class AlbumRepository : IAlbumRepository
    {
        private readonly ApplicationDbContext _db;

        public AlbumRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<AlbumReturnDto> FindAllAlbums()
        {
            return _db.Albums
                .Join(_db.Artists,
                album => album.ArtistID,
                artist => artist.ArtistID,
                (albums, artist) => new {Album = albums, ArtistName = artist.Name}
                )
                .Select(q => ModelExtensions.ToAlbumReturnDto(q.Album, q.ArtistName!))
                .ToList();
        }

        public AlbumReturnDto? FindAlbumById(int id)
        {
            return FindAllAlbums()
                .FirstOrDefault(a => a.Id == id);
        }

        public AlbumReturnDto? AddNewAlbum(AlbumPutDto albumPutDto)
        {
            try
            {
                Artist? artist = CheckArtistExists(albumPutDto.ArtistID);
                if(artist == null) return null;

                if (!CheckGenreExists(albumPutDto.GenreID)) return null;

                Album album = ModelExtensions.PutDtoToAlbum(albumPutDto);
                _db.Albums.Add(album);
                _db.SaveChanges();
                return ModelExtensions.ToAlbumReturnDto(album, artist!.Name!);
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

            if(!CheckGenreExists(albumPutDto.GenreID)) return null;

            ModelExtensions.MapAlbumProperties(albumToUpdate, albumPutDto);

            _db.Update(albumToUpdate);
            _db.SaveChanges();
            return ModelExtensions.ToAlbumReturnDto(albumToUpdate, artist.Name!);
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
