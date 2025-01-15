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
    }
}
