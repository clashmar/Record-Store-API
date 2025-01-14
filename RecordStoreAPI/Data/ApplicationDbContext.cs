using Microsoft.EntityFrameworkCore;
using RecordStoreAPI.Models;

namespace RecordStoreAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        List<Album> AlbumList = new List<Album>
        {
            new Album
            {
                Name = "Merriweather Post Pavillion",
                Artist = "Animal Collective",
                ReleaseYear = 2009,
                Genre = "Experimental",
                Information = null,
                StockQuantity = 1
            },
            new Album
            {
                Name = "Blue",
                Artist = "Joni Mitchell",
                ReleaseYear = 1971,
                Genre = "Folk",
                Information = null,
                StockQuantity = 1
            },
            new Album
            {
                Name = "The Money Store",
                Artist = "Death Grips",
                ReleaseYear = 2012,
                Genre = "Industrial Hip-Hop",
                Information = null,
                StockQuantity = 2
            }
        };

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option) 
        {
            Albums.AddRange(AlbumList);
            SaveChanges();
        }

        public DbSet<Album> Albums { get; set; }

    }
}
