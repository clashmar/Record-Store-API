namespace RecordStoreFrontend.Client.Models
{
    public record AlbumReturnDto(
        int AlbumID,
        string Name,
        string? Artist,
        int ArtistId,
        int ReleaseYear,
        List<GenreEnum> Genres,
        string Information,
        int StockQuantity,
        int PriceInPence,
        string ImageURL
        );
}
