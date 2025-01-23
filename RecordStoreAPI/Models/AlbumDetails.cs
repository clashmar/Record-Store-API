using RecordStoreAPI.Entities;

namespace RecordStoreAPI.Models
{
    public class AlbumDetails
    {
        public int AlbumID { get; set; }
        public string Name { get; set; } = "";
        public string Artist { get; set; } = "";
        public int ArtistId { get; set; } = 0;
        public int ReleaseYear { get; set; } = 1860;
        public List<Genres> Genres { get; set; } = [];
        public string Information { get; set; } = "";
        public int StockQuantity { get; set; } = 0;
        public int PriceInPence { get; set; } = 0;
    }
}
