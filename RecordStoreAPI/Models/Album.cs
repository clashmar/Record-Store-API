using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RecordStoreAPI.Models
{
    public class Album
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } 

        public string? Artist { get; set; }

        [DisplayName("Release Year")]
        public int ReleaseYear { get; set; }

        public string? Genre { get; set; }

        public string? Information { get; set; }

        [DisplayName("Stock Quantity")]
        public int StockQuantity { get; set; }
    }
}
