using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RecordStoreFrontend.Client.Models
{
    public class ArtistDetails
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        [Required(ErrorMessage = "Please enter an artist name.")]
        public string Name { get; set; } = "";

        [JsonPropertyName("albums")]
        public List<AlbumDetails> Albums { get; set; } = [];

        [JsonPropertyName("imageURL")]
        public string ImageURL { get; set; } = "";

        [JsonPropertyName("performerType")]
        [Required(ErrorMessage = "Please enter a performer type.")]
        public string PerformerType { get; set; } = "";

        [JsonPropertyName("origin")]
        [Required(ErrorMessage = "Please enter an origin.")]
        public string Origin { get; set; } = "";
    }
}
