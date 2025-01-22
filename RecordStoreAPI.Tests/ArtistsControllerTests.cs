using Microsoft.AspNetCore.Mvc;
using Moq;
using RecordStoreAPI.Controllers;
using RecordStoreAPI.Entities;
using RecordStoreAPI.Services;

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
        public void GetAllArtists_Returns_All_Artists()
        {
            List<ArtistDto> artists =
            [
                new(1, "Artist1", [])
            ];

            _artistsServiceMock.Setup(s => s.FindAllArtists()).Returns(artists);

            var result = _artistsController.GetAllArtists() as ObjectResult;

            if (result is OkObjectResult okObjectResult) Assert.That(okObjectResult.Value, Is.EqualTo(artists));
            else Assert.Fail();
        }

        [Test]
        public void GetAllArtists_Returns_Bad_Request_If_Empty()
        {
            _artistsServiceMock.Setup(s => s.FindAllArtists()).Returns([]);

            var result = _artistsController.GetAllArtists();

            if (result is NotFoundObjectResult notFoundObjectResult) Assert.Pass();
            else Assert.Fail();
        }

        [Test]
        public void GetAlbumsByArtistId_Calls_Correct_Service_Method()
        {
            _artistsController.GetAlbumsByArtistId(1);

            _artistsServiceMock.Verify(s => s.FindAlbumsByArtistId(1), Times.Once());
        }

        [Test]
        public void GetAlbumsByArtistId_Returns_Correct_Album()
        {
            List<AlbumReturnDto> albums = new List<AlbumReturnDto>
            {
                new(1, "Name1", "Artist1", 2001, [], "Information", 1),
                new(2, "Name2", "Artist1", 2002, [], "Information", 2)
            };

            _artistsServiceMock.Setup(s => s.FindAlbumsByArtistId(1)).Returns(albums);

            var result = _artistsController.GetAlbumsByArtistId(1) as ObjectResult;

            if (result is OkObjectResult okObjectResult) Assert.That(okObjectResult.Value, Is.EqualTo(albums));
            else Assert.Fail();
        }
    }
}
