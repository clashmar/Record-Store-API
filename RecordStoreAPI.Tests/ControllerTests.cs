using Microsoft.AspNetCore.Mvc;
using Moq;
using RecordStoreAPI.Controllers;
using RecordStoreAPI.Models;
using RecordStoreAPI.Services;

namespace RecordStoreAPI.Tests
{
    public class ControllerTests
    {
        private Mock<IAlbumService> _albumServiceMock;
        private AlbumController _albumController;

        [SetUp]
        public void Setup()
        {
            _albumServiceMock = new Mock<IAlbumService>();
            _albumController = new AlbumController(_albumServiceMock.Object);
        }

        [Test]
        public void GetAllAlbums_Calls_Correct_Service_Method()
        {
            _albumController.GetAllAlbums();

            _albumServiceMock.Verify(s => s.FindAllAlbums(), Times.Once());
        }

        [Test]
        public void GetAllAlbums_Returns_All_Albums()
        {
            List<Album> albums = new List<Album>
            {
                new Album() { Id = 1, Name = "Name1", Artist = "Artist1", ReleaseYear = 2001, Genre = "Genre1", StockQuantity = 1 },
                new Album() { Id = 2, Name = "Name2", Artist = "Artist2", ReleaseYear = 2002, Genre = "Genre2", StockQuantity = 2 }
            };

            _albumServiceMock.Setup(s => s.FindAllAlbums()).Returns(albums);

            var result = _albumController.GetAllAlbums() as ObjectResult;

            Assert.That(result!.Value, Is.EquivalentTo(albums));
        }

        [Test]
        public void GetAllAlbums_Returns_Bad_Request_If_Empty()
        {
            _albumServiceMock.Setup(s => s.FindAllAlbums()).Returns(new List<Album> { });

            var result = _albumController.GetAllAlbums();

            if(result is BadRequestObjectResult badRequestObjectResult)
            {
                Assert.That(badRequestObjectResult.Value, Is.EqualTo("There are no albums."));
            }
            else
            {
                Assert.Fail();
            }
        }
    }
}