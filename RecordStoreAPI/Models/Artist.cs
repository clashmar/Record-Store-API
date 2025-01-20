using System.ComponentModel.DataAnnotations;

namespace RecordStoreAPI.Models
{
    public class Artist
    {
        [Key]
        public int ArtistID { get; set; }
        
        [Required]
        public string? Name { get; set; }

        public List<Album> Albums { get; set; } = [];
    }
    public record ArtistDto(
            int ArtistID,
            string Name,
            List<AlbumReturnDto> Albums
            );
}
