using Microsoft.AspNetCore.Mvc;
using Moq;
using RecordStoreAPI.Controllers;
using RecordStoreAPI.Services;
using RecordStoreFrontend.Client.Models;

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
        public void GetAllAlbums_Returns_Albums()
        {
            List<AlbumDetails> albums =
            [
                new() 
                { 
                    Id = 1, 
                    Name = "Name1", 
                    ArtistID = 1, 
                    ArtistName = "Artist1", 
                    ReleaseYear = 2001, 
                    Genres = [], 
                    Information = "Information", 
                    ImageURL = "URL" 
                }
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
        public void GetAlbumById_Returns_Correct_Album()
        {
            AlbumDetails? album = new() 
            { 
                Id = 1, 
                Name = "Name1", 
                ArtistID = 1, 
                ArtistName = "Artist1", 
                ReleaseYear = 2001, 
                Genres = [], 
                Information = "Information", 
                ImageURL = "URL" 
            };

            _albumsServiceMock.Setup(s => s.FindAlbumById(1)).Returns(album);

            var result = _albumsController.GetAlbumById(1);

            if (result is OkObjectResult okObjectResult) Assert.That(okObjectResult.Value, Is.EqualTo(album));
            else Assert.Fail();
        }

        [Test]
        public void GetAlbumById_Returns_Bad_Request_If_Not_Added()
        {
            AlbumDetails? nullAlbum = null;

            _albumsServiceMock.Setup(s => s.FindAlbumById(1)).Returns(nullAlbum);

            var result = _albumsController.GetAlbumById(1);

            if (result is BadRequestObjectResult) Assert.Pass();
            else Assert.Fail();
        }

        [Test]
        public void PostNewAlbum_Calls_Correct_Service_Method()
        {
            AlbumDetails album = new() 
            { 
                Id = 1, 
                Name = "Name1", 
                ArtistID = 1, 
                ArtistName = "Artist1", 
                ReleaseYear = 2001, 
                Genres = [], 
                Information = "Information", 
                ImageURL = "URL" 
            };

            _albumsController.PostNewAlbum(album);

            _albumsServiceMock.Verify(s => s.AddNewAlbum(album), Times.Once());
        }

        [Test]
        public void PostNewAlbum_Returns_Correct_Album()
        {
            AlbumDetails album = new() 
            { 
                Id = 1, 
                Name = "Name1", 
                ArtistID = 1, 
                ArtistName = "Artist1", 
                ReleaseYear = 2001, 
                Genres = [], 
                Information = "Information", 
                ImageURL = "URL" 
            };

            _albumsServiceMock.Setup(s => s.AddNewAlbum(album)).Returns(album);

            var result = _albumsController.PostNewAlbum(album);

            if(result is OkObjectResult okObjectResult) Assert.That(okObjectResult.Value, Is.EqualTo(album));
            else Assert.Fail();
        }

        [Test]
        public void PostNewAlbum_Returns_Not_Found_If_Not_Added()
        {
            AlbumDetails album = new() 
            { 
                Id = 1, 
                Name = "Name1", 
                ArtistID = 1, 
                ArtistName = "Artist1", 
                ReleaseYear = 2001, 
                Genres = [], 
                Information = "Information", 
                ImageURL = "URL" 
            };

            AlbumDetails? nullAlbum = null;

            _albumsServiceMock.Setup(s => s.AddNewAlbum(album)).Returns(nullAlbum);

            var result = _albumsController.PostNewAlbum(album);

            if (result is NotFoundObjectResult) Assert.Pass();
            else Assert.Fail();
        }

        [Test]
        public void PutAlbumById_Calls_Correct_Service_Method()
        {
            AlbumDetails album = new() 
            { 
                Id = 1, 
                Name = "Name1", 
                ArtistID = 1, 
                ArtistName = "Artist1", 
                ReleaseYear = 2001, 
                Genres = [], 
                Information = "Information", 
                ImageURL = "URL" 
            };

            _albumsController.PutAlbumById(1, album);

            _albumsServiceMock.Verify(s => s.UpdateAlbum(1, album), Times.Once());
        }

        [Test]
        public void PutAlbumById_Returns_Correct_Album()
        {
            AlbumDetails album = new() 
            { 
                Id = 1, 
                Name = "Name1", 
                ArtistID = 1, 
                ArtistName = "Artist1", 
                ReleaseYear = 2001, 
                Genres = [], 
                Information = "Information", 
                ImageURL = "URL" 
            };

            _albumsServiceMock.Setup(s => s.UpdateAlbum(1, album)).Returns(album);

            var result = _albumsController.PutAlbumById(1, album);

            if (result is OkObjectResult okObjectResult) Assert.That(okObjectResult.Value, Is.EqualTo(album));
            else Assert.Fail();
        }

        [Test]
        public void PutAlbumById_Returns_Not_Found_If_Not_Updated()
        {
            AlbumDetails album = new() { Id = 1, Name = "Name1", ArtistID = 1, ArtistName = "Artist1", ReleaseYear = 2001, Genres = [], Information = "Information", ImageURL = "URL" };
            AlbumDetails? nullAlbum = null;

            _albumsServiceMock.Setup(s => s.UpdateAlbum(1, album)).Returns(nullAlbum);

            var result = _albumsController.PutAlbumById(1, album);

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
        public void DeleteAlbumById_Returns_Ok_Result_If_Deleted()
        {
            _albumsServiceMock.Setup(s => s.TryRemoveAlbumById(1)).Returns(true);

            var result = _albumsController.DeleteAlbumById(1);

            if (result is OkObjectResult okObjectResult) Assert.Pass();
            Assert.Fail();
        }

        [Test]
        public void DeleteAlbumById_Returns_Bad_Result_If_Not_Deleted()
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
        public void GetAlbumsByReleaseYear_Returns_Correct_Album()
        {
            List<AlbumDetails> albums =
            [
                new() 
                { 
                    Id = 1, 
                    Name = "Name1", 
                    ArtistID = 1, 
                    ArtistName = "Artist1", 
                    ReleaseYear = 2001, 
                    Genres = [], 
                    Information = "Information", 
                    ImageURL = "URL" 
                }
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
            _albumsController.GetAlbumsByGenre(GenreEnum.Folk);

            _albumsServiceMock.Verify(s => s.FindAlbumsByGenre(GenreEnum.Folk), Times.Once());
        }

        [Test]
        public void GetAlbumsByGenre_Returns_Correct_Album()
        {
            List<AlbumDetails> albums =
            [
                new() 
                { 
                    Id = 1, 
                    Name = "Name1", 
                    ArtistID = 1, 
                    ArtistName = "Artist1", 
                    ReleaseYear = 2001, 
                    Genres = [], 
                    Information = "Information", 
                    ImageURL = "URL" 
                }
            ];

            _albumsServiceMock.Setup(s => s.FindAlbumsByGenre(GenreEnum.Folk)).Returns(albums);

            var result = _albumsController.GetAlbumsByGenre(GenreEnum.Folk);

            if (result is OkObjectResult okObjectResult) Assert.That(okObjectResult.Value, Is.EqualTo(albums));
            else Assert.Fail();
        }

        [Test]
        public void GetAlbumsByGenre_Returns_Not_Found_If_Not_Found()
        {
            _albumsServiceMock.Setup(s => s.FindAlbumsByGenre(GenreEnum.Folk)).Returns([]);

            var result = _albumsController.GetAlbumsByGenre(GenreEnum.Folk);

            if (result is NotFoundObjectResult) Assert.Pass();
            else Assert.Fail();
        }

        [Test]
        public void GetSearchResults_Calls_Correct_Service_Method()
        {
            _albumsController.GetSearchResults("Search Term");

            _albumsServiceMock.Verify(s => s.FindSearchResults("Search Term"), Times.Once());
        }

        [Test]
        public void GetSearchResults_Returns_Correct_Search_Results()
        {
            List<SearchResult> results =
            [
                new() 
                { 
                    Id = 1, 
                    Name = "Name", 
                    ResultType = SearchResultType.Album, 
                    Description = "Description", 
                    ImageURL = "URL" 
                },
            ];

            _albumsServiceMock.Setup(s => s.FindSearchResults("Name")).Returns(results);

            var result = _albumsController.GetSearchResults("Name");

            if (result is OkObjectResult okObjectResult) Assert.That(okObjectResult.Value, Is.EqualTo(results));
            else Assert.Fail();
        }

        [Test]
        public void GetSearchResults_Returns_Not_Found_If_Not_Found()
        {
            _albumsServiceMock.Setup(s => s.FindAlbumsByName("Name")).Returns([]);

            var result = _albumsController.GetSearchResults("Name");

            if (result is NotFoundObjectResult) Assert.Pass();
            else Assert.Fail();
        }
    }
}