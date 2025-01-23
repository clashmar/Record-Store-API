using System.ComponentModel.DataAnnotations;

namespace RecordStoreAPI.Entities
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
        Emo,
        Post_Rock,
        Soul,
        Dance
    }
    public class Genre
    {
        [Key]
        public Genres GenreID { get; set; }

        [Required]
        public string? Name { get; set; }

        public List<AlbumGenre> Albums { get; set; } = [];

        /// <summary>
        /// Returns a user friendly value for the given enum.
        /// </summary>
        /// <param name="genre"></param>
        /// <returns></returns
        /// 
        public static string ToFriendlyString(Genres genre)
        {
            return genre.ToString().Replace('_', '-');
        }

        public static (bool success, Genres genre) ToGenre(string s)
        {
            var result = Enum.TryParse(s.Replace('-', '_'), out Genres genre);

            return (result, genre);
        }

        public static List<string> ReturnGenresInOrder()
        {
            return Enum.GetValues(typeof(Genres))
                .Cast<Genres>()
                .Select(g => ToFriendlyString(g))
                .Order()
                .ToList();
        }
    }
    public record GenreDto(Genres GenreID, string Name);
}
