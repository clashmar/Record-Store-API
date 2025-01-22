using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecordStoreAPI.Entities
{
    public class AlbumGenre
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("ArtistID")]
        public int AlbumID { get; set; }

        public Album? Album { get; set; }

        [ForeignKey("GenreID")]
        public Genres GenreID { get; set; }

        public Genre? Genre { get; set; }
    }
}
