using Moq;
using RecordStoreAPI.Entities;
using RecordStoreAPI.Repositories;
using RecordStoreAPI.Services;
using RecordStoreFrontend.Client.Models;
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
                new() 
                { 
                    Id = 1, 
                    Name = "Artist1", 
                    Albums = [], 
                    PerformerType = "Type", 
                    Origin = "Origin"
                } 
            ];

            _artistsRepositoryMock.Setup(s => s.FindAllArtists()).Returns(artists);

            _artistsService.FindAllArtists();

            _artistsRepositoryMock.Verify(s => s.FindAllArtists(), Times.Once());
        }

        [Test]
        public void FindAllArtists_Returns_Artists()
        {
            List<Artist> artists =
             [
                 new() 
                 { 
                     Id = 1, 
                     Name = "Name", 
                     Albums = [], 
                     PerformerType = "Type", 
                     Origin = "Origin",
                     ImageURL = "URL"
                 }
             ];

            List<ArtistDetails> artistDetails =
            [
                new() 
                { 
                    Id = 1,
                    Name = "Name",
                    Albums = [],
                    PerformerType = "Type",
                    Origin = "Origin",
                    ImageURL = "URL"
                }
            ];

            _artistsRepositoryMock.Setup(s => s.FindAllArtists()).Returns(artists);

            var result = _artistsService.FindAllArtists();

            var jsonResult = JsonSerializer.Serialize(result);
            var jsonArtistDetails = JsonSerializer.Serialize(artistDetails);

            Assert.That(jsonResult, Is.EqualTo(jsonArtistDetails));
        }

        [Test]
        public void FindArtistById_Calls_Correct_Service_Method()
        {
            _artistsService.FindArtistById(1);

            _artistsRepositoryMock.Verify(s => s.FindArtistById(1), Times.Once());
        }

        [Test]
        public void FindArtistById_Returns_Correct_Albums()
        {
            Artist artist = new()
            {
                 
                Id = 1,
                Name = "Name",
                Albums = [],
                PerformerType = "Type",
                Origin = "Origin",
                ImageURL = "URL"

            };

            ArtistDetails artistDetails = new()
            {
                Id = 1,
                Name = "Name",
                Albums = [],
                PerformerType = "Type",
                Origin = "Origin",
                ImageURL = "URL"
            };

            _artistsRepositoryMock.Setup(s => s.FindArtistById(1)).Returns(artist);

            var result = _artistsService.FindArtistById(1);

            var jsonResult = JsonSerializer.Serialize(result);
            var jsonAlbumsDetails = JsonSerializer.Serialize(artistDetails);

            Assert.That(jsonResult, Is.EqualTo(jsonAlbumsDetails));
        }
    }
}
