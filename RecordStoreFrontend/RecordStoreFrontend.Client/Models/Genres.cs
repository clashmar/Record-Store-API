namespace RecordStoreFrontend.Client.Models
{
    public enum GenreEnum
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
    
    public class Genres
    {
        public static Dictionary<GenreEnum, string> GenresDictionary()
        {
            return Enum.GetValues(typeof(GenreEnum))
                .Cast<GenreEnum>()
                .Select(g => (g, ToFriendlyString(g)))
                .OrderBy(x => x.Item2)
                .ToDictionary();
        }

        /// <summary>
        /// Returns a user friendly value for the given enum.
        /// </summary>
        /// <param name="genre"></param>
        /// <returns></returns
        /// 
        public static string ToFriendlyString(GenreEnum genre)
        {
            return genre.ToString().Replace('_', '-').Replace('4', '&');
        }

        public static (bool success, GenreEnum genre) ToGenre(string s)
        {
            var result = Enum.TryParse(s.Replace('-', '_').Replace('&', '4'), out GenreEnum genre);

            return (result, genre);
        }
    }
}
    