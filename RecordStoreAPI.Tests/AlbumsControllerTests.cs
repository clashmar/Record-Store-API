using Microsoft.AspNetCore.Mvc;
using Moq;
using RecordStoreAPI.Controllers;
using RecordStoreAPI.Models;
using RecordStoreAPI.Services;

namespace RecordStoreAPI.Tests
{
    public class AlbumsControllerTests
    {
        private Mock<IAlbumService> _albumServiceMock;
        private AlbumsController _albumController;

        [SetUp]
        public void Setup()
        {
            _albumServiceMock = new Mock<IAlbumService>();
            _albumController = new AlbumsController(_albumServiceMock.Object);
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
            List<AlbumReturnDto> albums =
            [
                new(1, "Name1", "Artist1", 2001, "Genre1", "Information", 1),
                new(2, "Name2", "Artist2", 2002, "Genre2", "Information", 2)
            ];

            _albumServiceMock.Setup(s => s.FindAllAlbums()).Returns(albums);

            var result = _albumController.GetAllAlbums();

            if (result is OkObjectResult okObjectResult) Assert.That(okObjectResult.Value, Is.EqualTo(albums));
            else Assert.Fail();
        }

        [Test]
        public void GetAllAlbums_Returns_Not_Found_If_Empty()
        {
            _albumServiceMock.Setup(s => s.FindAllAlbums()).Returns([]);

            var result = _albumController.GetAllAlbums();

            if (result is NotFoundObjectResult notFoundObjectResult) Assert.Pass();
            else Assert.Fail();
        }

        [Test]
        public void GetAlbumById_Calls_Correct_Service_Method()
        {
            _albumController.GetAlbumById(1);

            _albumServiceMock.Verify(s => s.FindAlbumById(1), Times.Once());
        }

        [Test]
        public void GetAlbumById_Returns_Correct_AlbumDto()
        {
            AlbumReturnDto? returnDto = new(1, "Name1", "Artist1", 2001, "Genre1", "Information", 1);

            _albumServiceMock.Setup(s => s.FindAlbumById(1)).Returns(returnDto);

            var result = _albumController.GetAlbumById(1);

            if (result is OkObjectResult okObjectResult) Assert.That(okObjectResult.Value, Is.EqualTo(returnDto));
            else Assert.Fail();
        }

        [Test]
        public void GetAlbumById_Returns_Bad_Request_If_Not_Added()
        {
            AlbumReturnDto? returnDto = null;

            _albumServiceMock.Setup(s => s.FindAlbumById(1)).Returns(returnDto);

            var result = _albumController.GetAlbumById(1);

            if (result is BadRequestObjectResult) Assert.Pass();
            else Assert.Fail();
        }

        [Test]
        public void PostNewAlbum_Calls_Correct_Service_Method()
        {
            AlbumPutDto album = new("Album1", 1, 2001, Genres.Folk, "Informtion", 1);

            _albumController.PostNewAlbum(album);

            _albumServiceMock.Verify(s => s.AddNewAlbum(album), Times.Once());
        }

        [Test]
        public void PostNewAlbum_Returns_Correct_AlbumDto()
        {
            AlbumPutDto putDto = new("Album1", 1, 2001, Genres.Folk, "Information", 1);
            AlbumReturnDto? returnDto = new(1, "Name1", "Artist1", 2001, "Folk", "Information", 1);

            _albumServiceMock.Setup(s => s.AddNewAlbum(putDto)).Returns(returnDto);

            var result = _albumController.PostNewAlbum(putDto);

            if(result is OkObjectResult okObjectResult) Assert.That(okObjectResult.Value, Is.EqualTo(returnDto));
            else Assert.Fail();
        }

        [Test]
        public void PostNewAlbum_Returns_Not_Found_If_Not_Added()
        {
            AlbumPutDto putDto = new("Album1", 1, 2001, Genres.Folk, "Information", 1);
            AlbumReturnDto? returnDto = null;

            _albumServiceMock.Setup(s => s.AddNewAlbum(putDto)).Returns(returnDto);

            var result = _albumController.PostNewAlbum(putDto);

            if (result is NotFoundObjectResult) Assert.Pass();
            else Assert.Fail();
        }

        [Test]
        public void PutAlbumById_Calls_Correct_Service_Method()
        {
            AlbumPutDto putDto = new("Album1", 1, 2001, Genres.Folk, "Informtion", 1);

            _albumController.PutAlbumById(1, putDto);

            _albumServiceMock.Verify(s => s.UpdateAlbum(1, putDto), Times.Once());
        }

        [Test]
        public void PutAlbumById_Returns_Correct_AlbumDto()
        {
            AlbumPutDto putDto = new("Album1", 1, 2001, Genres.Folk, "Informtion", 1);
            AlbumReturnDto? returnDto = new(1, "Name1", "Artist1", 2001, "Folk", "Information", 1);

            _albumServiceMock.Setup(s => s.UpdateAlbum(1, putDto)).Returns(returnDto);

            var result = _albumController.PutAlbumById(1, putDto);

            if (result is OkObjectResult okObjectResult) Assert.That(okObjectResult.Value, Is.EqualTo(returnDto));
            else Assert.Fail();
        }

        [Test]
        public void PutAlbumById_Returns_Not_Found_If_Not_Updated()
        {
            AlbumPutDto putDto = new("Album1", 1, 2001, Genres.Folk, "Informtion", 1);
            AlbumReturnDto? returnDto = null;

            _albumServiceMock.Setup(s => s.UpdateAlbum(1, putDto)).Returns(returnDto);

            var result = _albumController.PutAlbumById(1, putDto);

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

        [Test]
        public void GetAlbumsByReleaseYear_Calls_Correct_Service_Method()
        {
            _albumController.GetAlbumsByReleaseYear(2025);

            _albumServiceMock.Verify(s => s.FindAlbumsByReleaseYear(2025), Times.Once());
        }

        [Test]
        public void GetAlbumsByReleaseYear_Returns_Correct_AlbumDtos()
        {
            List<AlbumReturnDto> albums =
            [
                new(1, "Name1", "Artist1", 2025, "Genre1", "Information", 1),
                new(2, "Name2", "Artist2", 2025, "Genre2", "Information", 2)
            ];

            _albumServiceMock.Setup(s => s.FindAlbumsByReleaseYear(2025)).Returns(albums);

            var result = _albumController.GetAlbumsByReleaseYear(2025);

            if (result is OkObjectResult okObjectResult) Assert.That(okObjectResult.Value, Is.EqualTo(albums));
            else Assert.Fail();
        }

        [Test]
        public void GetAlbumsByReleaseYear_Returns_Not_Found_If_Not_Found()
        {
            List<AlbumReturnDto> albums = [];

            _albumServiceMock.Setup(s => s.FindAlbumsByReleaseYear(2025)).Returns(albums);

            var result = _albumController.GetAlbumsByReleaseYear(2025);

            if (result is NotFoundObjectResult) Assert.Pass();
            else Assert.Fail();
        }
    }
}