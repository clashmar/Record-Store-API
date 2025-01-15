using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RecordStoreAPI.Models
{
    public class Album
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; } 

        public string? Artist { get; set; }

        [DisplayName("Release Year")]
        public int ReleaseYear { get; set; }

        public Genres Genre { get; set; }

        [DisplayName("Stock Quantity")]
        public int StockQuantity { get; set; }
    }

    public record AlbumDto(
        int Id,
        string Name,
        string Artist,
        int ReleaseYear,
        string Genre,
        int StockQuantity
        );

    
}
