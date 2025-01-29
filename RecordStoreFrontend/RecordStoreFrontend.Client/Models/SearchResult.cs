using System.Text.Json.Serialization;

namespace RecordStoreFrontend.Client.Models
{
    public enum SearchResultType
    {
        Album,
        Artist
    }
    public class SearchResult
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("resultType")]
        public SearchResultType ResultType { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = "";

        [JsonPropertyName("description")]
        public string Description { get; set; } = "";

        [JsonPropertyName("imageURL")]
        public string ImageURL { get; set; } = "";
    }
}
