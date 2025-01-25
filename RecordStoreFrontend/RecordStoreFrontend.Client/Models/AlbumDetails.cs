using RecordStoreFrontend.Client.Validation.Attributes;
using System.ComponentModel.DataAnnotations;

namespace RecordStoreFrontend.Client.Models
{
    public class AlbumDetails
    {
        public int AlbumID { get; set; }

        [Required(ErrorMessage = "Please enter an album name.")]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = "Please enter an artist name.")]
        public string ArtistName { get; set; } = "";
        public int ArtistID { get; set; } = 0;

        [ValidReleaseYear]
        public int ReleaseYear { get; set; } = 1860;

        [PopulatedGenres]
        public List<GenreEnum> Genres { get; set; } = [];
        public string Information { get; set; } = "";
        public int StockQuantity { get; set; } = 0;
        public int PriceInPence { get; set; } = 0;
    }
}
