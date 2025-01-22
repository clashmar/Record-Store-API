using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecordStoreAPI.Entities
{
    public class Album
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [ForeignKey("ArtistID")]
        public int ArtistID { get; set; }

        public Artist? Artist { get; set; }

        public int ReleaseYear { get; set; }

        public List<AlbumGenre> AlbumGenres { get; set; } = [];

        public string Information { get; set; } = "No information available.";

        public int StockQuantity { get; set; }

        public int PriceInPence { get; set; }
    }

    public record AlbumReturnDto(
        int Id,
        string Name,
        string? Artist,
        int ReleaseYear,
        List<string>? Genres,
        string Information,
        int StockQuantity,
        int PriceInPence
        );

    public record AlbumPutDto(
        string Name,
        int ArtistID,
        int ReleaseYear,
        List<Genres> Genres,
        string Information,
        int StockQuantity,
        int PriceInPence
        );
}
