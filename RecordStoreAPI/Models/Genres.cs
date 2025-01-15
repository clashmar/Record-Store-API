using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace RecordStoreAPI.Models
{
    public enum Genres
    {
        Folk,
        Hip_Hop,
        Indie,
        Experimental,
        Rap,
        Art_Rock
    }

    public class Genre
    {
        [Key]
        public Genres GenreID { get; set; }

        [Required]
        public string? Name { get; set; }
    }
}
