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

        /// <summary>
        /// Returns a user friendly value for the given enum.
        /// </summary>
        /// <param name="genre"></param>
        /// <returns></returns>
        public static string ToString(Genres genre)
        {
            return genre.ToString().Replace('_', '-');
        }
    }
}
