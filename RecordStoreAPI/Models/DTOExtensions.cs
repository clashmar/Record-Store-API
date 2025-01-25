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
                        album.Artist!.ArtistID,
                        album.ReleaseYear,
                        album.AlbumGenres?.Select(g => g.GenreID).ToList()!,
                        album.Information,
                        album.StockQuantity,
                        album.PriceInPence
                        );
        }

        public static ArtistDto ToArtistDto(Artist artist)
        {
            return new ArtistDto(
                artist.ArtistID,
                artist.Name!,
                artist.Albums?.Select(a => ToAlbumReturnDto(a)).ToList()!
                );
        }
    }
}
