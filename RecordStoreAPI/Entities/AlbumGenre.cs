using Microsoft.EntityFrameworkCore;
using RecordStoreFrontend.Client.Models;
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
        public GenreEnum GenreID { get; set; }

        public Genre? Genre { get; set; }
    }
}
