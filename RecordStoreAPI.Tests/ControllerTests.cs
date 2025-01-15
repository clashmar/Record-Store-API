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
            List<AlbumDto> albums = new List<AlbumDto>
            {
                new AlbumDto(1, "Name1", "Artist1", 2001, "Genre1", 1),
                new AlbumDto(2, "Name2", "Artist2", 2002, "Genre2", 2)
            };

            _albumServiceMock.Setup(s => s.FindAllAlbums()).Returns(albums);

            var result = _albumController.GetAllAlbums() as ObjectResult;

            Assert.That(result!.Value, Is.EquivalentTo(albums));
        }

        [Test]
        public void GetAllAlbums_Returns_Bad_Request_If_Empty()
        {
            _albumServiceMock.Setup(s => s.FindAllAlbums()).Returns(new List<AlbumDto> { });

            var result = _albumController.GetAllAlbums();

            if(result is NotFoundObjectResult notFoundObjectResult)
            {
                Assert.That(notFoundObjectResult.Value, Is.EqualTo("There are no albums."));
            }
            else
            {
                Assert.Fail();
            }
        }
    }
}