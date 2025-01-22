using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecordStoreAPI.Entities
{
    [PrimaryKey("AlbumID", "GenreID")]
    public class AlbumGenre
    {
        [ForeignKey("AlbumID")]
        public int AlbumID { get; set; }

        public Album? Album { get; set; }

        [ForeignKey("GenreID")]
        public Genres GenreID { get; set; }

        public Genre? Genre { get; set; }
    }
}
