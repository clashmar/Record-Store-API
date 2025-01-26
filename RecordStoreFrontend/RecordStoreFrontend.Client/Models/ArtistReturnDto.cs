namespace RecordStoreFrontend.Client.Models
{
    public record ArtistReturnDto(
            int Id,
            string Name,
            List<AlbumReturnDto> Albums,
            string ImageURL,
            string PerformerType,
            string Origin
            );
}
