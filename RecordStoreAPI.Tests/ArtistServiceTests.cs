using Moq;
using RecordStoreAPI.Entities;
using RecordStoreAPI.Repositories;
using RecordStoreAPI.Services;
using System.Text.Json;

namespace RecordStoreAPI.Tests
{
    public class ArtistServiceTests
    {
        private Mock<IArtistsRepository> _artistsRepositoryMock;
        private ArtistsService _artistsService;

        [SetUp]
        public void Setup()
        {
            _artistsRepositoryMock = new Mock<IArtistsRepository>();
            _artistsService = new ArtistsService(_artistsRepositoryMock.Object);
        }

        [Test]
        public void FindAllArtists_Calls_Correct_Service_Method()
        {
            List<Artist> artists =
            [
                new() { ArtistID = 1, Name = "Artist1", Albums = []}
            ];

            _artistsRepositoryMock.Setup(s => s.FindAllArtists()).Returns(artists);

            _artistsService.FindAllArtists();

            _artistsRepositoryMock.Verify(s => s.FindAllArtists(), Times.Once());
        }

        [Test]
        public void FindAllArtists_Returns_All_Artists()
        {
            List<Artist> artists =
            [
                new() { ArtistID = 1, Name = "Artist1", Albums = []}
            ];

            List<ArtistDto> dtos =
            [
                new(1, "Artist1", [])
            ];

            _artistsRepositoryMock.Setup(s => s.FindAllArtists()).Returns(artists);

            var result = _artistsService.FindAllArtists();

            var jsonResult = JsonSerializer.Serialize(result);
            var jsonDto = JsonSerializer.Serialize(dtos);

            Assert.That(jsonDto, Is.EqualTo(jsonResult));
        }

        [Test]
        public void FindAlbumsByArtistId_Calls_Correct_Service_Method()
        {
            _artistsService.FindAlbumsByArtistId(1);

            _artistsRepositoryMock.Verify(s => s.FindAlbumsByArtistId(1), Times.Once());
        }

        [Test]
        public void FindAlbumsByArtistId_Returns_Correct_Albums()
        {
            List<Album> albums =
            [
                new() {
                    Id = 1,
                    Name = "Name1",
                    Artist = new() { Name = "Artist1" },
                    ReleaseYear = 2001,
                    AlbumGenres = [],
                    Information = "Information",
                    StockQuantity = 1 },
            ];

            List<AlbumReturnDto> dtos = new List<AlbumReturnDto>
            {
                new(1, "Name1", "Artist1", 2001, [], "Information", 1),
            };

            _artistsRepositoryMock.Setup(s => s.FindAlbumsByArtistId(1)).Returns(albums);

            var result = _artistsService.FindAlbumsByArtistId(1);

            var jsonResult = JsonSerializer.Serialize(result);
            var jsonDto = JsonSerializer.Serialize(dtos);

            Assert.That(jsonDto, Is.EqualTo(jsonResult));
        }
    }
}
