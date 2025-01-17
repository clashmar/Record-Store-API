using System.ComponentModel.DataAnnotations;

namespace RecordStoreAPI.Models
{
    public enum Genres
    {
        Folk,
        Hip_Hop,
        Indie,
        Experimental,
        Rap,
        Art_Rock,
        Electronic,
        Alternative,
        Emo
    }

    public class Genre
    {
        [Key]
        public Genres GenreID { get; set; }

        [Required]
        public string? Name { get; set; }
    }
}
