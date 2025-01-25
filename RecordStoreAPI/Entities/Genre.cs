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
        Dance,
        R4B
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
        
        public static Dictionary<Genres, string> GenresDictionary()
        {
            return Enum.GetValues(typeof(Genres))
                .Cast<Genres>()
                .Select(g => (g, ToFriendlyString(g)))
                .OrderBy(x => x.Item2)
                .ToDictionary();
        }
        
        public static string ToFriendlyString(Genres genre)
        {
            return genre.ToString().Replace('_', '-').Replace('4', '&');
        }

        public static (bool success, Genres genre) ToGenre(string s)
        {
            var result = Enum.TryParse(s.Replace('-', '_').Replace('&', '4'), out Genres genre);

            return (result, genre);
        }
    }
    public record GenreDto(Genres GenreID, string Name);
}
