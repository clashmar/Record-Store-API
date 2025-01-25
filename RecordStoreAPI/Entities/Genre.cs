using System.ComponentModel.DataAnnotations;
using RecordStoreFrontend.Client.Models;

namespace RecordStoreAPI.Entities
{
    public class Genre
    {
        [Key]
        public GenreEnum GenreID { get; set; }

        [Required]
        public string? Name { get; set; }

        public List<AlbumGenre> Albums { get; set; } = [];

    }
}
