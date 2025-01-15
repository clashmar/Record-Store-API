using System.ComponentModel.DataAnnotations;

namespace RecordStoreAPI.Models
{
    public class Artist
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string? Name { get; set; }
    }
}
