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
                new(1, "Name1", "Artist1", 2001, "Genre1", 1),
                new(2, "Name2", "Artist2", 2002, "Genre2", 2)
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

            if (result is NotFoundObjectResult notFoundObjectResult) Assert.Pass();
            else Assert.Fail();
        }

        [Test]
        public void PostNewAlbum_Calls_Correct_Service_Method()
        {
            Album album = new() { Id = 1, Name = "Name1", Artist = "Artist1", ReleaseYear = 2001, Genre = Genres.Folk, StockQuantity = 1 };

            _albumController.PostNewAlbum(album);

            _albumServiceMock.Verify(s => s.AddNewAlbum(album), Times.Once());
        }

        [Test]
        public void PostNewAlbum_Returns_New_AlbumDto()
        {
            Album album = new() { Id = 1, Name = "Name1", Artist = "Artist1", ReleaseYear = 2001, Genre = Genres.Folk, StockQuantity = 1 };
            AlbumDto? albumDto = new(1, "Name1", "Artist1", 2001, "Folk", 1);

            _albumServiceMock.Setup(s => s.AddNewAlbum(album)).Returns(albumDto);

            var result = _albumController.PostNewAlbum(album);

            if(result is OkObjectResult okObjectResult) Assert.That(okObjectResult.Value, Is.EqualTo(albumDto));
            else Assert.Fail();
        }

        [Test]
        public void PostNewAlbum_Returns_Not_Found_If_Not_Added()
        {
            Album album = new() { Id = 1, Name = "Name1", Artist = "Artist1", ReleaseYear = 2001, Genre = Genres.Folk, StockQuantity = 1 };
            AlbumDto? albumDto = null;

            _albumServiceMock.Setup(s => s.AddNewAlbum(album)).Returns(albumDto);

            var result = _albumController.PostNewAlbum(album);

            if (result is NotFoundObjectResult) Assert.Pass();
            else Assert.Fail();
        }

        [Test]
        public void PutAlbumById_Calls_Correct_Service_Method()
        {
            Album album = new() { Id = 1, Name = "Name1", Artist = "Artist1", ReleaseYear = 2001, Genre = Genres.Folk, StockQuantity = 1 };

            _albumController.PutAlbumById(1, album);

            _albumServiceMock.Verify(s => s.UpdateAlbum(1, album), Times.Once());
        }

        [Test]
        public void PutAlbumById_Returns_Correct_AlbumDto()
        {
            Album album = new() { Id = 1, Name = "Name1", Artist = "Artist1", ReleaseYear = 2001, Genre = Genres.Folk, StockQuantity = 1 };
            AlbumDto? albumDto = new(1, "Name1", "Artist1", 2001, "Folk", 1);

            _albumServiceMock.Setup(s => s.UpdateAlbum(1, album)).Returns(albumDto);

            var result = _albumController.PutAlbumById(1, album);

            if (result is OkObjectResult okObjectResult) Assert.That(okObjectResult.Value, Is.EqualTo(albumDto));
            else Assert.Fail();
        }

        [Test]
        public void PutAlbumById_Returns_Not_Found_If_Not_Updated()
        {
            Album album = new() { Id = 1, Name = "Name1", Artist = "Artist1", ReleaseYear = 2001, Genre = Genres.Folk, StockQuantity = 1 };
            AlbumDto? albumDto = null;

            _albumServiceMock.Setup(s => s.UpdateAlbum(1, album)).Returns(albumDto);

            var result = _albumController.PostNewAlbum(album);

            if (result is NotFoundObjectResult) Assert.Pass();
            else Assert.Fail();
        }

        [Test]
        public void DeleteAlbumById_Calls_Correct_Service_Method()
        {
            _albumController.DeleteAlbumById(1);

            _albumServiceMock.Verify(s => s.TryRemoveAlbumById(1), Times.Once());
        }

        [Test]
        public void DeleteAlbumById_Returns_Ok_Result()
        {
            _albumServiceMock.Setup(s => s.TryRemoveAlbumById(1)).Returns(true);

            var result = _albumController.DeleteAlbumById(1);

            if (result is OkObjectResult okObjectResult) Assert.Pass();
            Assert.Fail();
        }

        [Test]
        public void PutAlbumById_Returns_Bad_Result()
        {
            _albumServiceMock.Setup(s => s.TryRemoveAlbumById(1)).Returns(false);

            var result = _albumController.DeleteAlbumById(1);

            if (result is BadRequestObjectResult) Assert.Pass();
            else Assert.Fail();
        }
    }
}