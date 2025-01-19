using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecordStoreAPI.Models
{
    public class Album
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [ForeignKey("ArtistID")]
        public int ArtistID { get; set; }

        public Artist Artist { get; set; } = null!;
        
        public int ReleaseYear { get; set; }

        [ForeignKey("GenreID")]
        public Genres GenreID { get; set; }

        public List<AlbumGenre>? Genres { get; set; }

        public string Information { get; set; } = "No information available.";

        public int StockQuantity { get; set; }
    }

    public record AlbumReturnDto(
        int Id,
        string Name,
        string Artist,
        int ReleaseYear,
        string Genre,
        string Information,
        int StockQuantity
        );

    public record AlbumPutDto(
        string Name,
        int ArtistID,
        int ReleaseYear,
        Genres GenreID,
        string Information,
        int StockQuantity
        );
}
