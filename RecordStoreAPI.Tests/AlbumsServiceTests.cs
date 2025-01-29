using Moq;
using RecordStoreAPI.Entities;
using RecordStoreAPI.Repositories;
using RecordStoreAPI.Services;
using RecordStoreFrontend.Client.Models;
using System.Text.Json;

namespace RecordStoreAPI.Tests
{
    public class AlbumsServiceTests
    {
        private Mock<IAlbumsRepository> _albumsRepositoryMock;
        private AlbumsService _albumsService;

        [SetUp]
        public void Setup()
        {
            _albumsRepositoryMock = new Mock<IAlbumsRepository>();
            _albumsService = new AlbumsService(_albumsRepositoryMock.Object);
        }

        [Test]
        public void FindAllAlbums_Calls_Correct_Repo_Method()
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

            _albumsRepositoryMock.Setup(s => s.FindAllAlbums()).Returns(albums);

            _albumsService.FindAllAlbums();

            _albumsRepositoryMock.Verify(s => s.FindAllAlbums(), Times.Once());
        }

        [Test]
        public void FindAllAlbums_Returns_Albums()
        {
            List<Album> albums =
            [
                new() {
                Id = 1,
                Name = "Name1",
                Artist = new() { Id = 1, Name = "Artist1" },
                ReleaseYear = 2001,
                AlbumGenres = [],
                Information = "Information",
                ImageURL = "URL"}
            ];

            List<AlbumDetails> albumDetails =
            [
                new() { Id = 1,
                    Name = "Name1",
                    ArtistID = 1,
                    ArtistName = "Artist1",
                    ReleaseYear = 2001,
                    Genres = [],
                    Information = "Information",
                    ImageURL = "URL"}
            ];

            _albumsRepositoryMock.Setup(s => s.FindAllAlbums()).Returns(albums);

            var result = _albumsService.FindAllAlbums();

            var jsonResult = JsonSerializer.Serialize(result);
            var jsonAlbumDetails = JsonSerializer.Serialize(albumDetails);

            Assert.That(jsonResult, Is.EqualTo(jsonAlbumDetails));
        }

        [Test]
        public void FindAlbumById_Calls_Correct_Repo_Method()
        {
            _albumsService.FindAlbumById(1);

            _albumsRepositoryMock.Verify(s => s.FindAlbumById(1), Times.Once());
        }

        [Test]
        public void FindAlbumById_Returns_Correct_Album()
        {
            Album album = new()
            {
                Id = 1,
                Name = "Name1",
                Artist = new() { Id = 1, Name = "Artist1" },
                ReleaseYear = 2001,
                AlbumGenres = [],
                Information = "Information",
                ImageURL = "URL"
            };

            AlbumDetails albumDetails = new() 
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

            _albumsRepositoryMock.Setup(s => s.FindAlbumById(1)).Returns(album);

            var result = _albumsService.FindAlbumById(1);

            var jsonResult = JsonSerializer.Serialize(result);
            var jsonAlbumDetails = JsonSerializer.Serialize(albumDetails);

            Assert.That(jsonResult, Is.EqualTo(jsonAlbumDetails));
        }

        [Test]
        public void FindAlbumById_Returns_Null_If_Not_Found()
        {
            Album? album = null;

            _albumsRepositoryMock.Setup(s => s.FindAlbumById(1)).Returns(album);

            var result = _albumsService.FindAlbumById(1);

            Assert.That(result, Is.EqualTo(null));
        }

        [Test]
        public void AddNewAlbum_Calls_Correct_Repo_Method()
        {
            AlbumDetails albumDetails = new()
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

            _albumsService.AddNewAlbum(albumDetails);

            _albumsRepositoryMock.Verify(s => s.AddNewAlbum(albumDetails), Times.Once());
        }

        [Test]
        public void AddNewAlbum_Returns_Correct_Album()
        {
            Album album = new()
            {
                Id = 1,
                Name = "Name1",
                Artist = new() { Id = 1, Name = "Artist1" },
                ReleaseYear = 2001,
                AlbumGenres = [],
                Information = "Information",
                ImageURL = "URL"
            };

            AlbumDetails albumDetails = new()
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

            _albumsRepositoryMock.Setup(s => s.AddNewAlbum(albumDetails)).Returns(album);

            var result = _albumsService.AddNewAlbum(albumDetails);

            var jsonResult = JsonSerializer.Serialize(result);
            var jsonAlbumDetails = JsonSerializer.Serialize(albumDetails);

            Assert.That(jsonResult, Is.EquivalentTo(jsonAlbumDetails));
        }

        [Test]
        public void AddNewAlbum_Returns_Null_If_Not_Added()
        {
            AlbumDetails albumDetails = new()
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

            Album? nullAlbum = null;

            AlbumDetails? nullDetails = null;

            _albumsRepositoryMock.Setup(s => s.AddNewAlbum(albumDetails)).Returns(nullAlbum);

            var result = _albumsService.AddNewAlbum(albumDetails);

            Assert.That(result, Is.EqualTo(nullDetails));
        }

        [Test]
        public void UpdateAlbum_Calls_Correct_Repo_Method()
        {
            AlbumDetails albumDetails = new()
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

            _albumsService.UpdateAlbum(1, albumDetails);

            _albumsRepositoryMock.Verify(s => s.UpdateAlbum(1, albumDetails), Times.Once());
        }

        [Test]
        public void UpdateAlbum_Returns_Correct_Album()
        {
            AlbumDetails albumDetails = new()
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

            Album album = new()
            {
                Id = 1,
                Name = "Name1",
                Artist = new() { Id = 1, Name = "Artist1" },
                ReleaseYear = 2001,
                AlbumGenres = [],
                Information = "Information",
                ImageURL = "URL"
            };

            _albumsRepositoryMock.Setup(s => s.UpdateAlbum(1, albumDetails)).Returns(album);

            var result = _albumsService.UpdateAlbum(1, albumDetails);

            var jsonResult = JsonSerializer.Serialize(result);
            var jsonAlbumDetails = JsonSerializer.Serialize(albumDetails);

            Assert.That(jsonResult, Is.EqualTo(jsonAlbumDetails));
        }

        [Test]
        public void UpdateAlbum_Returns_Null_If_Not_Updated()
        {
            AlbumDetails albumDetails = new()
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

            Album? nullAlbum = null;

            AlbumDetails? nullDetails = null;

            _albumsRepositoryMock.Setup(s => s.UpdateAlbum(1, albumDetails)).Returns(nullAlbum);

            var result = _albumsService.UpdateAlbum(1, albumDetails);

            Assert.That(result, Is.EqualTo(nullDetails));
        }

        [Test]
        public void TryRemoveAlbumById_Calls_Correct_Repo_Method()
        {
            _albumsService.TryRemoveAlbumById(1);

            _albumsRepositoryMock.Verify(s => s.TryRemoveAlbumById(1), Times.Once());
        }

        [Test]
        public void TryRemoveAlbumById_Returns_True_If_Removed()
        {
            _albumsRepositoryMock.Setup(s => s.TryRemoveAlbumById(1)).Returns(true);

            var result = _albumsService.TryRemoveAlbumById(1);

            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void TryRemoveAlbumById_Returns_False_If_Not_Removed()
        {
            _albumsRepositoryMock.Setup(s => s.TryRemoveAlbumById(1)).Returns(false);

            var result = _albumsService.TryRemoveAlbumById(1);

            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void FindAlbumsByReleaseYear_Calls_Correct_Repo_Method()
        {
            List<Album> albums =
            [
                new() {
                Id = 1,
                Name = "Name1",
                Artist = new() { Id = 1, Name = "Artist1" },
                ReleaseYear = 2001,
                AlbumGenres = [],
                Information = "Information",
                ImageURL = "URL"}
            ];

            _albumsRepositoryMock.Setup(s => s.FindAlbumsByReleaseYear(2001)).Returns(albums);

            _albumsService.FindAlbumsByReleaseYear(2001);

            _albumsRepositoryMock.Verify(s => s.FindAlbumsByReleaseYear(2001), Times.Once());
        }

        [Test]
        public void FindAlbumsByReleaseYear_Returns_Correct_Album()
        {
            List<Album> albums =
            [
                new() {
                Id = 1,
                Name = "Name1",
                Artist = new() { Id = 1, Name = "Artist1" },
                ReleaseYear = 2001,
                AlbumGenres = [],
                Information = "Information",
                ImageURL = "URL"}
            ];

            List<AlbumDetails> albumDetails =
            [
                new() { Id = 1, 
                    Name = "Name1", 
                    ArtistID = 1, 
                    ArtistName = "Artist1", 
                    ReleaseYear = 2001, 
                    Genres = [], 
                    Information = "Information", 
                    ImageURL = "URL"}
            ];

            _albumsRepositoryMock.Setup(s => s.FindAlbumsByReleaseYear(2001)).Returns(albums);

            var result = _albumsService.FindAlbumsByReleaseYear(2001);

            var jsonResult = JsonSerializer.Serialize(result);
            var jsonAlbumDetails = JsonSerializer.Serialize(albumDetails);

            Assert.That(jsonResult, Is.EqualTo(jsonAlbumDetails));
        }

        [Test]
        public void FindAlbumsByReleaseYear_Returns_Empty_List_If_Not_Found()
        {
            List<AlbumDetails> albums = [];

            _albumsRepositoryMock.Setup(s => s.FindAlbumsByReleaseYear(2025)).Returns([]);

            var result = _albumsService.FindAlbumsByReleaseYear(2025);

            Assert.That(result, Is.EqualTo(albums));
        }

        [Test]
        public void FindAlbumsByGenre_Calls_Correct_Repo_Method()
        {
            List<Album> albums =
            [
                new() {
                Id = 1,
                Name = "Name1",
                Artist = new() { Id = 1, Name = "Artist1" },
                ReleaseYear = 2001,
                AlbumGenres = [new() {GenreID = GenreEnum.Folk }],
                Information = "Information",
                ImageURL = "URL"}
            ];

            _albumsRepositoryMock.Setup(s => s.FindAlbumsByGenre(GenreEnum.Folk)).Returns(albums);

            _albumsService.FindAlbumsByGenre(GenreEnum.Folk);

            _albumsRepositoryMock.Verify(s => s.FindAlbumsByGenre(GenreEnum.Folk), Times.Once());
        }

        [Test]
        public void FindAlbumsByGenre_Returns_Correct_Album()
        {
            List<Album> albums =
            [
                new() {
                Id = 1,
                Name = "Name1",
                Artist = new() { Id = 1, Name = "Artist1" },
                ReleaseYear = 2001,
                AlbumGenres = [new() {GenreID = GenreEnum.Folk }],
                Information = "Information",
                ImageURL = "URL"}
            ];

            List<AlbumDetails> albumDetails =
            [
                new() { Id = 1,
                    Name = "Name1",
                    ArtistID = 1,
                    ArtistName = "Artist1",
                    ReleaseYear = 2001,
                    Genres = [GenreEnum.Folk],
                    Information = "Information",
                    ImageURL = "URL"}
            ];

            _albumsRepositoryMock.Setup(s => s.FindAlbumsByGenre(GenreEnum.Folk)).Returns(albums);

            var result = _albumsService.FindAlbumsByGenre(GenreEnum.Folk);

            var jsonResult = JsonSerializer.Serialize(result);
            var jsonAlbumDetails = JsonSerializer.Serialize(albumDetails);

            Assert.That(jsonResult, Is.EqualTo(jsonAlbumDetails));
        }

        [Test]
        public void FindAlbumsByGenre_Returns_Empty_If_Not_Found()
        {
            List<AlbumDetails>? albums = [];

            _albumsRepositoryMock.Setup(s => s.FindAllAlbums()).Returns([]);

            var result = _albumsService.FindAlbumsByGenre(GenreEnum.Folk);

            Assert.That(result, Is.EqualTo(albums));
        }
    }
}
