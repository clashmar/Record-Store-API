using Microsoft.EntityFrameworkCore;
using RecordStoreAPI.Entities;

namespace RecordStoreAPI.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option) 
        {
        }

        public DbSet<Album> Albums { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<Artist> Artists { get; set; }

        public DbSet<AlbumGenre> AlbumGenre { get; set; }
    }
}
