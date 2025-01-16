using System.ComponentModel.DataAnnotations;

namespace RecordStoreAPI.Models
{
    public class Artist
    {
        [Key]
        public int ArtistID { get; set; }
        
        [Required]
        public string? Name { get; set; }
    }
}
