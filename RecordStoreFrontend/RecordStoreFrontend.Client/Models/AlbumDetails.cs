using RecordStoreFrontend.Client.Validation.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RecordStoreFrontend.Client.Models
{
    public class AlbumDetails
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        [Required(ErrorMessage = "Please enter an album name.")]
        public string Name { get; set; } = "";

        [JsonPropertyName("artistID")]
        public int ArtistID { get; set; } = 0;

        [JsonPropertyName("artistName")]
        [Required(ErrorMessage = "Please enter an artist name.")]
        public string ArtistName { get; set; } = "";

        [JsonPropertyName("releaseYear")]
        [Required(ErrorMessage = "Please enter a realease year."), ValidReleaseYear]
        public int ReleaseYear { get; set; } = 1860;

        [JsonPropertyName("genres")]
        [PopulatedGenres]
        public List<GenreEnum> Genres { get; set; } = [];

        [JsonPropertyName("information")]
        public string Information { get; set; } = "";

        [JsonPropertyName("stockQuantity")]
        public int StockQuantity { get; set; } = 0;

        [JsonPropertyName("priceInPence")]
        public int PriceInPence { get; set; } = 2000;

        [JsonPropertyName("imageURL")]
        [Required(ErrorMessage = "Please enter an image url.")]
        public string ImageURL { get; set; } = "";
    }
}
