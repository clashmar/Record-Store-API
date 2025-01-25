namespace RecordStoreFrontend.Client.Models
{
    public record AlbumReturnDto(
        int Id,
        string Name,
        string? Artist,
        int ArtistId,
        int ReleaseYear,
        List<GenreEnum> Genres,
        string Information,
        int StockQuantity,
        int PriceInPence
        );
}
