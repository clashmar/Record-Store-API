namespace RecordStoreAPI.Models
{
    public record ArtistDto(
            int ArtistID,
            string Name,
            List<AlbumReturnDto> Albums
            );
}
