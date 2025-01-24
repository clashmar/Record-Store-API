using RecordStoreAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace RecordStoreAPI.Entities
{
    public class Artist
    {
        [Key]
        public int ArtistID { get; set; }

        [Required]
        public string Name { get; set; }

        public List<Album> Albums { get; set; } = [];
    }
}
