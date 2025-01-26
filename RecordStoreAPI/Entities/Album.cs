using RecordStoreFrontend.Client.Interfaces;
using RecordStoreFrontend.Client.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecordStoreAPI.Entities
{
    public class Album : ISearchable
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = "";

        [ForeignKey("ArtistID")]
        public int ArtistID { get; set; }

        public Artist? Artist { get; set; }

        public int ReleaseYear { get; set; }

        public List<AlbumGenre> AlbumGenres { get; set; } = [];

        public string Information { get; set; } = "No information available.";

        public int StockQuantity { get; set; } = 0;

        public int PriceInPence { get; set; } = 2000;

        public string ImageURL { get; set; } = "";

        [NotMapped]
        public SearchResultType ResultType {  get; set; } = SearchResultType.Album;
        
        public string Description()
        {
            return $"Album by {Artist!.Name}.";
        }
    }
}
