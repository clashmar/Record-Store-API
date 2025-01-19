namespace RecordStoreAPI.Models
{
    public class DTOExtensions
    {
        public static AlbumReturnDto ToAlbumReturnDto(Album album, string artistName)
        {
            return new AlbumReturnDto(
                        album.Id,
                        album.Name,
                        artistName,
                        album.ReleaseYear,
                        Genre.ToString(album.GenreID),
                        album.Information,
                        album.StockQuantity
                        );
        }
        public static Album PutDtoToAlbum(AlbumPutDto albumPutDto)
        {
            return new Album()
            {
                Name = albumPutDto.Name,
                ArtistID = albumPutDto.ArtistID,
                ReleaseYear = albumPutDto.ReleaseYear,
                GenreID = albumPutDto.GenreID,
                Information = albumPutDto.Information,
                StockQuantity = albumPutDto.StockQuantity
            };
        }
        public static void MapAlbumProperties(Album target, AlbumPutDto source)
        {
            target.Name = source.Name;
            target.ArtistID = source.ArtistID;
            target.ReleaseYear = source.ReleaseYear;
            target.GenreID = source.GenreID;
            target.Information = source.Information;
            target.StockQuantity = source.StockQuantity;
        }
    }
}
