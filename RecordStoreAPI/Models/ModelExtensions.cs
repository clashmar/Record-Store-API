namespace RecordStoreAPI.Models
{
    public class ModelExtensions
    {
        public static AlbumReturnDto ToAlbumReturnDto(Album album, string artistName)
        {
            return new AlbumReturnDto(
                        album.Id,
                        album.Name,
                        artistName,
                        album.ReleaseYear,
                        album.GenreID.ToString().Replace('_', '-'),
                        album.Information,
                        album.StockQuantity
                        );
        }

        public static AlbumDto ToAlbumDto(Album album)
        {
            return new AlbumDto(
                album.Id,
                album.Name!,
                album.ArtistID!,
                album.ReleaseYear,
                album.GenreID.ToString().Replace('_', '-'),
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
