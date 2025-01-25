using Microsoft.AspNetCore.Mvc;
using Moq;
using RecordStoreAPI.Controllers;
using RecordStoreAPI.Entities;
using RecordStoreAPI.Services;

namespace RecordStoreAPI.Tests
{
    public class AlbumsControllerTests
    {
        private Mock<IAlbumsService> _albumsServiceMock;
        private AlbumsController _albumsController;

        [SetUp]
        public void Setup()
        {
            _albumsServiceMock = new Mock<IAlbumsService>();
            _albumsController = new AlbumsController(_albumsServiceMock.Object);
        }

        [Test]
        public void GetAllAlbums_Calls_Correct_Service_Method()
        {
            _albumsController.GetAllAlbums();

            _albumsServiceMock.Verify(s => s.FindAllAlbums(), Times.Once());
        }

        [Test]
        public void GetAllAlbums_Returns_All_Albums()
        {
            List<AlbumReturnDto> albums =
            [
                new(1, "Name1", "Artist1", 2001, [], "Information", 1, 1),
                new(2, "Name2", "Artist2", 2002, [], "Information", 2, 1)
            ];

            _albumsServiceMock.Setup(s => s.FindAllAlbums()).Returns(albums);

            var result = _albumsController.GetAllAlbums();

            if (result is OkObjectResult okObjectResult) Assert.That(okObjectResult.Value, Is.EqualTo(albums));
            else Assert.Fail();
        }

        [Test]
        public void GetAllAlbums_Returns_Not_Found_If_Empty()
        {
            _albumsServiceMock.Setup(s => s.FindAllAlbums()).Returns([]);

            var result = _albumsController.GetAllAlbums();

            if (result is NotFoundObjectResult notFoundObjectResult) Assert.Pass();
            else Assert.Fail();
        }

        [Test]
        public void GetAlbumById_Calls_Correct_Service_Method()
        {
            _albumsController.GetAlbumById(1);

            _albumsServiceMock.Verify(s => s.FindAlbumById(1), Times.Once());
        }

        [Test]
        public void GetAlbumById_Returns_Correct_AlbumDto()
        {
            AlbumReturnDto? returnDto = new(1, "Name1", "Artist1", 2001, [], "Information", 1, 1);

            _albumsServiceMock.Setup(s => s.FindAlbumById(1)).Returns(returnDto);

            var result = _albumsController.GetAlbumById(1);

            if (result is OkObjectResult okObjectResult) Assert.That(okObjectResult.Value, Is.EqualTo(returnDto));
            else Assert.Fail();
        }

        [Test]
        public void GetAlbumById_Returns_Bad_Request_If_Not_Added()
        {
            AlbumReturnDto? returnDto = null;

            _albumsServiceMock.Setup(s => s.FindAlbumById(1)).Returns(returnDto);

            var result = _albumsController.GetAlbumById(1);

            if (result is BadRequestObjectResult) Assert.Pass();
            else Assert.Fail();
        }

        [Test]
        public void PostNewAlbum_Calls_Correct_Service_Method()
        {
            AlbumPutDto album = new("Album1", 1, 2001, [], "Informtion", 1, 1);

            _albumsController.PostNewAlbum(album);

            _albumsServiceMock.Verify(s => s.AddNewAlbum(album), Times.Once());
        }

        [Test]
        public void PostNewAlbum_Returns_Correct_AlbumDto()
        {
            AlbumPutDto putDto = new("Album1", 1, 2001, [], "Information", 1, 1);
            AlbumReturnDto? returnDto = new(1, "Name1", "Artist1", 2001, [], "Information", 1, 1);

            _albumsServiceMock.Setup(s => s.AddNewAlbum(putDto)).Returns(returnDto);

            var result = _albumsController.PostNewAlbum(putDto);

            if(result is OkObjectResult okObjectResult) Assert.That(okObjectResult.Value, Is.EqualTo(returnDto));
            else Assert.Fail();
        }

        [Test]
        public void PostNewAlbum_Returns_Not_Found_If_Not_Added()
        {
            AlbumPutDto putDto = new("Album1", 1, 2001, [], "Information", 1, 1);
            AlbumReturnDto? returnDto = null;

            _albumsServiceMock.Setup(s => s.AddNewAlbum(putDto)).Returns(returnDto);

            var result = _albumsController.PostNewAlbum(putDto);

            if (result is NotFoundObjectResult) Assert.Pass();
            else Assert.Fail();
        }

        [Test]
        public void PutAlbumById_Calls_Correct_Service_Method()
        {
            AlbumPutDto putDto = new("Album1", 1, 2001, [], "Informtion", 1, 1);

            _albumsController.PutAlbumById(1, putDto);

            _albumsServiceMock.Verify(s => s.UpdateAlbum(1, putDto), Times.Once());
        }

        [Test]
        public void PutAlbumById_Returns_Correct_AlbumDto()
        {
            AlbumPutDto putDto = new("Album1", 1, 2001, [], "Informtion", 1, 1);
            AlbumReturnDto? returnDto = new(1, "Name1", "Artist1", 2001, [], "Information", 1, 1);

            _albumsServiceMock.Setup(s => s.UpdateAlbum(1, putDto)).Returns(returnDto);

            var result = _albumsController.PutAlbumById(1, putDto);

            if (result is OkObjectResult okObjectResult) Assert.That(okObjectResult.Value, Is.EqualTo(returnDto));
            else Assert.Fail();
        }

        [Test]
        public void PutAlbumById_Returns_Not_Found_If_Not_Updated()
        {
            AlbumPutDto putDto = new("Album1", 1, 2001, [], "Informtion", 1, 1);
            AlbumReturnDto? returnDto = null;

            _albumsServiceMock.Setup(s => s.UpdateAlbum(1, putDto)).Returns(returnDto);

            var result = _albumsController.PutAlbumById(1, putDto);

            if (result is NotFoundObjectResult) Assert.Pass();
            else Assert.Fail();
        }

        [Test]
        public void DeleteAlbumById_Calls_Correct_Service_Method()
        {
            _albumsController.DeleteAlbumById(1);

            _albumsServiceMock.Verify(s => s.TryRemoveAlbumById(1), Times.Once());
        }

        [Test]
        public void DeleteAlbumById_Returns_Ok_Result()
        {
            _albumsServiceMock.Setup(s => s.TryRemoveAlbumById(1)).Returns(true);

            var result = _albumsController.DeleteAlbumById(1);

            if (result is OkObjectResult okObjectResult) Assert.Pass();
            Assert.Fail();
        }

        [Test]
        public void PutAlbumById_Returns_Bad_Result()
        {
            _albumsServiceMock.Setup(s => s.TryRemoveAlbumById(1)).Returns(false);

            var result = _albumsController.DeleteAlbumById(1);

            if (result is BadRequestObjectResult) Assert.Pass();
            else Assert.Fail();
        }

        [Test]
        public void GetAlbumsByReleaseYear_Calls_Correct_Service_Method()
        {
            _albumsController.GetAlbumsByReleaseYear(2025);

            _albumsServiceMock.Verify(s => s.FindAlbumsByReleaseYear(2025), Times.Once());
        }

        [Test]
        public void GetAlbumsByReleaseYear_Returns_Correct_AlbumDtos()
        {
            List<AlbumReturnDto> albums =
            [
                new(1, "Name1", "Artist1", 2025, [], "Information", 1, 1),
                new(2, "Name2", "Artist2", 2025, [], "Information", 2, 1)
            ];

            _albumsServiceMock.Setup(s => s.FindAlbumsByReleaseYear(2025)).Returns(albums);

            var result = _albumsController.GetAlbumsByReleaseYear(2025);

            if (result is OkObjectResult okObjectResult) Assert.That(okObjectResult.Value, Is.EqualTo(albums));
            else Assert.Fail();
        }

        [Test]
        public void GetAlbumsByReleaseYear_Returns_Not_Found_If_Not_Found()
        {
            _albumsServiceMock.Setup(s => s.FindAlbumsByReleaseYear(2025)).Returns([]);

            var result = _albumsController.GetAlbumsByReleaseYear(2025);

            if (result is NotFoundObjectResult) Assert.Pass();
            else Assert.Fail();
        }

        [Test]
        public void GetAlbumsByGenre_Calls_Correct_Service_Method()
        {
            _albumsController.GetAlbumsByGenre(Genre.Folk);

            _albumsServiceMock.Verify(s => s.FindAlbumsByGenre(Genre.Folk), Times.Once());
        }

        [Test]
        public void GetAlbumsByGenre_Returns_Correct_AlbumDtos()
        {
            List<AlbumReturnDto> albums =
            [
                new(1, "Name1", "Artist1", 2025, new() {"Folk"}, "Information", 1, 1),
                new(2, "Name2", "Artist2", 2025, new() {"Folk"}, "Information", 2, 1)
            ];

            _albumsServiceMock.Setup(s => s.FindAlbumsByGenre(Genre.Folk)).Returns(albums);

            var result = _albumsController.GetAlbumsByGenre(Genre.Folk);

            if (result is OkObjectResult okObjectResult) Assert.That(okObjectResult.Value, Is.EqualTo(albums));
            else Assert.Fail();
        }

        [Test]
        public void GetAlbumsByGenre_Returns_Not_Found_If_Not_Found()
        {
            _albumsServiceMock.Setup(s => s.FindAlbumsByGenre(Genre.Folk)).Returns([]);

            var result = _albumsController.GetAlbumsByGenre(Genre.Folk);

            if (result is NotFoundObjectResult) Assert.Pass();
            else Assert.Fail();
        }

        [Test]
        public void GetAlbumByName_Calls_Correct_Service_Method()
        {
            _albumsController.GetAlbumByName("Name");

            _albumsServiceMock.Verify(s => s.FindAlbumsByName("Name"), Times.Once());
        }

        [Test]
        public void GetAlbumByName_Returns_Correct_AlbumDtos()
        {
            List<AlbumReturnDto> albumDtos =
            [
                new(1, "Name1", "Artist1", 2025, [], "Information", 1, 1),
            ];

            List<Album> albums =
            [
                new() { 
                    Id = 1, 
                    Name = "Name1", 
                    ArtistID = 1, 
                    ReleaseYear = 2025, 
                    AlbumGenres = [], 
                    Information = "Information", 
                    StockQuantity = 1 },
            ];

            _albumsServiceMock.Setup(s => s.FindAlbumsByName("Name1")).Returns(albumDtos);

            var result = _albumsController.GetAlbumByName("Name1");

            if (result is OkObjectResult okObjectResult) Assert.That(okObjectResult.Value, Is.EqualTo(albumDtos));
            else Assert.Fail();
        }

        [Test]
        public void GetAlbumByName_Returns_Not_Found_If_Not_Found()
        {
            _albumsServiceMock.Setup(s => s.FindAlbumsByName("Name1")).Returns([]);

            var result = _albumsController.GetAlbumByName("Name1");

            if (result is NotFoundObjectResult) Assert.Pass();
            else Assert.Fail();
        }
    }
}