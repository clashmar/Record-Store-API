using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecordStoreAPI.Entities
{
    public class Album
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a name.")]
        public string Name { get; set; } = "";

        [ForeignKey("ArtistID")]
        public int ArtistID { get; set; }

        public Artist? Artist { get; set; }

        [Required(ErrorMessage ="Please enter a year.")]
        [Range(1860, int.MaxValue, ErrorMessage= "Please enter a release year after 1860.")] 
        public int ReleaseYear { get; set; }

        public List<AlbumGenre> AlbumGenres { get; set; } = [];

        public string Information { get; set; } = "No information available.";

        public int StockQuantity { get; set; } = 0;

        public int PriceInPence { get; set; } = 2000;
    }
}
