namespace RecordStoreAPI.Models
{
    public class ModelExtensions
    {
        public static AlbumDto ToAlbumDto(Album album)
        {
            return new AlbumDto(
                album.Id,
                album.Name!,
                album.Artist!,
                album.ReleaseYear,
                album.Genre.ToString().Replace('_', '-'),
                album.StockQuantity
                );
        }

        public static void MapAlbumProperties(Album target, Album source)
        {
            target.Name = source.Name;
            target.Artist = source.Artist;
            target.ReleaseYear = source.ReleaseYear;
            target.Genre = source.Genre;
            target.StockQuantity = source.StockQuantity;
        }
    }
}
