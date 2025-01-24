using RecordStoreAPI.Entities;

namespace RecordStoreAPI.Models
{
    public record AlbumReturnDto(
        int Id,
        string Name,
        string? Artist,
        int ArtistId,
        int ReleaseYear,
        List<Genres> Genres,
        string Information,
        int StockQuantity,
        int PriceInPence
        );
}
