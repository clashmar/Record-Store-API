using RecordStoreFrontend.Client.Interfaces;
using RecordStoreFrontend.Client.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecordStoreAPI.Entities
{
    public class Artist : ISearchable
    {
        [Key]
        [Column("ArtistID")]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = "";

        public List<Album> Albums { get; set; } = [];

        [NotMapped]
        public SearchResultType ResultType {  get; set; } = SearchResultType.Artist;

        public string ImageURL { get; set; } = "";

        public string PerformerType { get; set; } = "";

        public string Origin { get; set; } = "";

        public string Description()
        {
            return $"{PerformerType} from {Origin}.";
        }
    }
}
