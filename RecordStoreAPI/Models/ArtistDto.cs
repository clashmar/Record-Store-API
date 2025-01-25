namespace RecordStoreFrontend.Client.Models
{
    public record ArtistDto(
            int ArtistID,
            string Name,
            List<AlbumReturnDto> Albums
            );
}
