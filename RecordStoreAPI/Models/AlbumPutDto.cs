using RecordStoreAPI.Entities;
using System.ComponentModel.DataAnnotations;

namespace RecordStoreAPI.Models
{
    public record AlbumPutDto(
        [Required(ErrorMessage = "Please enter a name.")]
        string Name,
        int ArtistID,
        [Required(ErrorMessage ="Please enter a year.")]
        [Range(1860, int.MaxValue, ErrorMessage="Please enter a release year after 1860.")]
        int ReleaseYear,
        List<Genres> Genres,
        string Information,
        [Range(1, int.MaxValue, ErrorMessage="Please enter a value more than zero.")]
        int StockQuantity,
        [Range(1, int.MaxValue, ErrorMessage="Please enter a value more than zero.")]
        int PriceInPence
        );
}
