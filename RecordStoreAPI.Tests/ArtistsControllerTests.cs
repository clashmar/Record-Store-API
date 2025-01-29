using Microsoft.AspNetCore.Mvc;
using Moq;
using RecordStoreAPI.Controllers;
using RecordStoreAPI.Services;
using RecordStoreFrontend.Client.Models;

namespace RecordStoreAPI.Tests
{
    public class ArtistsControllerTests
    {
        private Mock<IArtistsService> _artistsServiceMock;
        private ArtistsController _artistsController;

        [SetUp]
        public void Setup()
        {
            _artistsServiceMock = new Mock<IArtistsService>();
            _artistsController = new ArtistsController(_artistsServiceMock.Object);
        }

        [Test]
        public void GetAllArtists_Calls_Correct_Service_Method()
        {
            _artistsController.GetAllArtists();

            _artistsServiceMock.Verify(s => s.FindAllArtists(), Times.Once());
        }

        [Test]
        public void GetAllArtists_Returns_Artists()
        {
            List<ArtistDetails> artists =
            [
                new() 
                { 
                    Id = 1, 
                    Name = "Name", 
                    Albums = [], 
                    PerformerType = "Type", 
                    Origin = "Origin", 
                    ImageURL = "URl"
                }
            ];

            _artistsServiceMock.Setup(s => s.FindAllArtists()).Returns(artists);

            var result = _artistsController.GetAllArtists() as ObjectResult;

            if (result is OkObjectResult okObjectResult) Assert.That(okObjectResult.Value, Is.EqualTo(artists));
            else Assert.Fail();
        }

        [Test]
        public void GetAllArtists_Returns_Not_Found_If_Empty()
        {
            _artistsServiceMock.Setup(s => s.FindAllArtists()).Returns([]);

            var result = _artistsController.GetAllArtists();

            if (result is NotFoundObjectResult notFoundObjectResult) Assert.Pass();
            else Assert.Fail();
        }

        [Test]
        public void GetArtistById_Calls_Correct_Service_Method()
        {
            _artistsController.GetArtistById(1);

            _artistsServiceMock.Verify(s => s.FindArtistById(1), Times.Once());
        }

        [Test]
        public void GetArtistById_Returns_Correct_Album()
        {
            ArtistDetails artist = new()
            {
                Id = 1,
                Name = "Name",
                Albums = [],
                PerformerType = "Type",
                Origin = "Origin",
                ImageURL = "URl"
            };

            _artistsServiceMock.Setup(s => s.FindArtistById(1)).Returns(artist);

            var result = _artistsController.GetArtistById(1) as ObjectResult;

            if (result is OkObjectResult okObjectResult) Assert.That(okObjectResult.Value, Is.EqualTo(artist));
            else Assert.Fail();
        }

        [Test]
        public void GetArtistById_Returns_Bad_Request_If_Not_Found()
        {
            ArtistDetails? artist = null; 

            _artistsServiceMock.Setup(s => s.FindArtistById(1)).Returns(artist);

            var result = _artistsController.GetArtistById(1) as ObjectResult;

            if (result is BadRequestObjectResult badRequestObjectResult) Assert.Pass();
            else Assert.Fail();
        }
    }
}
