using RecordStoreAPI.Entities;
using RecordStoreFrontend.Client.Models;

namespace RecordStoreAPI.Models
{
    public class DTOExtensions
    {
        public static AlbumReturnDto ToAlbumReturnDto(Album album)
        {
            return new AlbumReturnDto(
                        album.Id,
                        album.Name,
                        album.Artist?.Name,
                        album.Artist!.Id,
                        album.ReleaseYear,
                        album.AlbumGenres?.Select(g => g.GenreID).ToList()!,
                        album.Information,
                        album.StockQuantity,
                        album.PriceInPence,
                        album.ImageURL
                        );
        }

        public static ArtistDto ToArtistDto(Artist artist)
        {
            return new ArtistDto(
                artist.Id,
                artist.Name!,
                artist.Albums?.Select(a => ToAlbumReturnDto(a)).ToList()!
                );
        }
    }
}
