using RecordStoreAPI.Entities;
using RecordStoreFrontend.Client.Interfaces;
using RecordStoreFrontend.Client.Models;

namespace RecordStoreAPI.Models
{
    public class ModelExtensions
    {
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
    }
}
