using RecordStoreAPI.Entities;
using RecordStoreFrontend.Client.Interfaces;
using RecordStoreFrontend.Client.Models;

namespace RecordStoreAPI.Models
{
    public class ModelExtensions
    {
        public static AlbumDetails ToAlbumDetails(Album album)
        {
            return new AlbumDetails
            {
                Id = album.Id,
                Name = album.Name,
                ArtistID = album.Artist!.Id,
                ArtistName = album.Artist!.Name,
                ReleaseYear = album.ReleaseYear,
                Genres = album.AlbumGenres?.Select(g => g.GenreID).ToList()!,
                Information = album.Information,
                StockQuantity = album.StockQuantity,
                PriceInPence = album.PriceInPence,
                ImageURL = album.ImageURL
            };
        }

        public static ArtistDetails ToArtistDetails(Artist artist)
        {
            return new ArtistDetails
            {
                Id = artist.Id,
                Name = artist.Name!,
                Albums = artist.Albums?.Select(a => ToAlbumDetails(a)).ToList()!,
                ImageURL = artist.ImageURL,
                PerformerType = artist.PerformerType,
                Origin = artist.Origin
            };
        }

        public static Album AlbumDetailsToAlbum(AlbumDetails albumDetails)
        {
            return new Album()
            {
                Name = albumDetails.Name,
                ArtistID = albumDetails.ArtistID,
                ReleaseYear = albumDetails.ReleaseYear,
                Information = albumDetails.Information,
                StockQuantity = albumDetails.StockQuantity,
                PriceInPence = albumDetails.PriceInPence,
                ImageURL = albumDetails.ImageURL,
            };
        }

        public static Artist ArtistDetailsToArtist(ArtistDetails artist)
        {
            return new Artist()
            {
                Name = artist.Name,
                PerformerType = artist.PerformerType,
                Origin = artist.Origin,
                ImageURL = artist.ImageURL
            };
        }

        public static SearchResult ToSearchResult(ISearchable searchItem)
        {
            return new SearchResult()
            {
                Id = searchItem.Id,
                Name = searchItem.Name,
                ImageURL = searchItem.ImageURL,
                ResultType = searchItem.ResultType,
                Description = searchItem.Description()
            };
        }

        public static void MapAlbumDetailsProperties(Album target, AlbumDetails source)
        {
            target.Name = source.Name;
            target.ArtistID = source.ArtistID;
            target.ReleaseYear = source.ReleaseYear;
            target.AlbumGenres = source.Genres?
                .Select(g => new AlbumGenre() { AlbumID = target.Id, GenreID = g })
                .ToList()!;
            target.Information = source.Information;
            target.StockQuantity = source.StockQuantity;
            target.PriceInPence = source.PriceInPence;
            target.ImageURL = source.ImageURL;
        }

        public static void MapArtistDetailsProperties(Artist target, ArtistDetails source)
        {
            target.Name = source.Name;
            target.PerformerType = source.PerformerType;
            target.Origin = source.Origin;
            target.ImageURL = source.ImageURL;
        }
    }
}
