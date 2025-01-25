using Moq;
using RecordStoreAPI.Entities;
using RecordStoreAPI.Repositories;
using RecordStoreAPI.Services;
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
        public void FindAllAlbums_Returns_All_AlbumDtos()
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
                StockQuantity = 1,
                PriceInPence = 1 }
            ];

            List<AlbumReturnDto> dtos =
            [
                new(1, "Name1", "Artist1", 2001, [], "Information", 1, 1),
            ];

            _albumsRepositoryMock.Setup(s => s.FindAllAlbums()).Returns(albums);

            var result = _albumsService.FindAllAlbums();

            var jsonResult = JsonSerializer.Serialize(result);
            var jsonDto = JsonSerializer.Serialize(dtos);

            Assert.That(jsonDto, Is.EqualTo(jsonResult));
        }

        [Test]
        public void FindAlbumById_Calls_Correct_Repo_Method()
        {
            _albumsService.FindAlbumById(1);

            _albumsRepositoryMock.Verify(s => s.FindAlbumById(1), Times.Once());
        }

        [Test]
        public void FindAlbumById_Returns_Correct_AlbumDto()
        {
            Album album = new()
            {
                Id = 1,
                Name = "Name1",
                Artist = new() { Name = "Artist1" },
                ReleaseYear = 2001,
                AlbumGenres = [],
                Information = "Information",
                StockQuantity = 1,
                PriceInPence = 1
            };

            AlbumReturnDto dto = new(1, "Name1", "Artist1", 2001, [], "Information", 1, 1);

            _albumsRepositoryMock.Setup(s => s.FindAlbumById(1)).Returns(album);

            var result = _albumsService.FindAlbumById(1);

            var jsonResult = JsonSerializer.Serialize(result);
            var jsonDto = JsonSerializer.Serialize(dto);

            Assert.That(jsonDto, Is.EqualTo(jsonResult));
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
            AlbumPutDto dto = new("Name1", 1, 2001, [], "Information", 1, 1);

            _albumsService.AddNewAlbum(dto);

            _albumsRepositoryMock.Verify(s => s.AddNewAlbum(dto), Times.Once());
        }

        [Test]
        public void AddNewAlbum_Returns_Correct_Album_Dto()
        {
            AlbumPutDto putDto = new("Name1", 1, 2001, [], "Information", 1, 1);

            Album album = new()
            {
                Id = 1,
                Name = "Name1",
                Artist = new() { Name = "Artist1" },
                ReleaseYear = 2001,
                AlbumGenres = [],
                Information = "Information",
                StockQuantity = 1,
                PriceInPence = 1
            };

            AlbumReturnDto? returnDto = new(1, "Name1", "Artist1", 2001, [], "Information", 1, 1);

            _albumsRepositoryMock.Setup(s => s.AddNewAlbum(putDto)).Returns(album);

            var result = _albumsService.AddNewAlbum(putDto);

            var jsonResult = JsonSerializer.Serialize(result);
            var jsonDto = JsonSerializer.Serialize(returnDto);

            Assert.That(jsonDto, Is.EqualTo(jsonResult));
        }

        [Test]
        public void AddNewAlbum_Returns_Null_If_Not_Added()
        {
            AlbumPutDto putDto = new("Name1", 1, 2001, [], "Information", 1, 1);
            Album? album = null;
            AlbumReturnDto? returnDto = null;

            _albumsRepositoryMock.Setup(s => s.AddNewAlbum(putDto)).Returns(album);

            var result = _albumsService.AddNewAlbum(putDto);

            Assert.That(result, Is.EqualTo(returnDto));
        }

        [Test]
        public void UpdateAlbum_Calls_Correct_Repo_Method()
        {
            AlbumPutDto putDto = new("Name1", 1, 2001, [], "Information", 1, 1);

            _albumsService.UpdateAlbum(1, putDto);

            _albumsRepositoryMock.Verify(s => s.UpdateAlbum(1, putDto), Times.Once());
        }

        [Test]
        public void UpdateAlbum_Returns_Correct_Album()
        {
            AlbumPutDto putDto = new("Name1", 1, 2001, [], "Information", 1, 1);

            Album album = new()
            {
                Id = 1,
                Name = "Name1",
                Artist = new() { Name = "Artist1" },
                ReleaseYear = 2001,
                AlbumGenres = [],
                Information = "Information",
                StockQuantity = 1,
                PriceInPence = 1
            };

            AlbumReturnDto returnDto = new(1, "Name1", "Artist1", 2001, [], "Information", 1, 1);

            _albumsRepositoryMock.Setup(s => s.UpdateAlbum(1, putDto)).Returns(album);

            var result = _albumsService.UpdateAlbum(1, putDto);

            var jsonResult = JsonSerializer.Serialize(result);
            var jsonDto = JsonSerializer.Serialize(returnDto);

            Assert.That(jsonDto, Is.EqualTo(jsonResult));
        }

        [Test]
        public void UpdateAlbum_Returns_Null_If_Not_Updated()
        {
            AlbumPutDto putDto = new("Name1", 1, 2001, [], "Information", 1, 1);
            Album? album = null;
            AlbumReturnDto? returnDto = null;

            _albumsRepositoryMock.Setup(s => s.UpdateAlbum(1, putDto)).Returns(album);

            var result = _albumsService.UpdateAlbum(1, putDto);

            Assert.That(result, Is.EqualTo(returnDto));
        }

        [Test]
        public void TryRemoveAlbumById_Calls_Correct_Repo_Method()
        {
            _albumsService.TryRemoveAlbumById(1);

            _albumsRepositoryMock.Verify(s => s.TryRemoveAlbumById(1), Times.Once());
        }

        [Test]
        public void TryRemoveAlbumById_Returns_True()
        {
            _albumsRepositoryMock.Setup(s => s.TryRemoveAlbumById(1)).Returns(true);

            var result = _albumsService.TryRemoveAlbumById(1);

            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void TryRemoveAlbumById_Returns_False()
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
                Artist = new() { Name = "Artist1" },
                ReleaseYear = 2001,
                AlbumGenres = [],
                Information = "Information",
                StockQuantity = 1,
                PriceInPence = 1 },
            ];

            _albumsRepositoryMock.Setup(s => s.FindAlbumsByReleaseYear(2001)).Returns(albums);

            _albumsService.FindAlbumsByReleaseYear(2001);

            _albumsRepositoryMock.Verify(s => s.FindAlbumsByReleaseYear(2001), Times.Once());
        }

        [Test]
        public void FindAlbumsByReleaseYear_Returns_Correct_AlbumDtos()
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
                StockQuantity = 1,
                PriceInPence = 1 },
            ];

            List<AlbumReturnDto> dtos =
            [
                new(1, "Name1", "Artist1", 2001, [], "Information", 1, 1),
            ];

            _albumsRepositoryMock.Setup(s => s.FindAlbumsByReleaseYear(2001)).Returns(albums);

            var result = _albumsService.FindAlbumsByReleaseYear(2001);

            var jsonResult = JsonSerializer.Serialize(result);
            var jsonDto = JsonSerializer.Serialize(dtos);

            Assert.That(jsonDto, Is.EqualTo(jsonResult));
        }

        [Test]
        public void FindAlbumsByReleaseYear_Returns_Empty_List_If_Not_Found()
        {
            List<AlbumReturnDto> dtos = [];

            _albumsRepositoryMock.Setup(s => s.FindAlbumsByReleaseYear(2025)).Returns([]);

            var result = _albumsService.FindAlbumsByReleaseYear(2025);

            Assert.That(result, Is.EqualTo(dtos));
        }

        [Test]
        public void FindAlbumsByGenre_Calls_Correct_Repo_Method()
        {
            List<Album> albums =
            [
                new() {
                Id = 1,
                Name = "Name1",
                Artist = new() { Name = "Artist1" },
                ReleaseYear = 2001,
                AlbumGenres = [new() { GenreID = Genre.Folk }],
                Information = "Information",
                StockQuantity = 1,
                PriceInPence = 1 },
            ];

            _albumsRepositoryMock.Setup(s => s.FindAlbumsByGenre(Genre.Folk)).Returns(albums);

            _albumsService.FindAlbumsByGenre(Genre.Folk);

            _albumsRepositoryMock.Verify(s => s.FindAlbumsByGenre(Genre.Folk), Times.Once());
        }

        [Test]
        public void FindAlbumsByGenre_Returns_Correct_AlbumDtos()
        {
            List<Album> albums =
            [
                new() {
                Id = 1,
                Name = "Name1",
                Artist = new() { Name = "Artist1" },
                ReleaseYear = 2001,
                AlbumGenres = [new() { GenreID = Genre.Folk }],
                Information = "Information",
                StockQuantity = 1,
                PriceInPence = 1 },
            ];

            List<AlbumReturnDto> dtos =
            [
                new(1, "Name1", "Artist1", 2001, ["Folk"], "Information", 1, 1),
            ];

            _albumsRepositoryMock.Setup(s => s.FindAlbumsByGenre(Genre.Folk)).Returns(albums);

            var result = _albumsService.FindAlbumsByGenre(Genre.Folk);

            var jsonResult = JsonSerializer.Serialize(result);
            var jsonDto = JsonSerializer.Serialize(dtos);

            Assert.That(jsonResult, Is.EqualTo(jsonDto));
        }

        [Test]
        public void FindAlbumsByGenre_Returns_Null_If_Not_Found()
        {
            List<AlbumReturnDto>? dtos = null;

            _albumsRepositoryMock.Setup(s => s.FindAllAlbums()).Returns([]);

            var result = _albumsService.FindAlbumsByGenre(Genre.Folk);

            Assert.That(result, Is.EqualTo(dtos));
        }

        [Test]
        public void FindAlbumByName_Calls_Correct_Repo_Method()
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
                StockQuantity = 1,
                PriceInPence = 1 },
            ];

            _albumsRepositoryMock.Setup(s => s.FindAllAlbums()).Returns(albums);

            _albumsService.FindAlbumsByName("Name1");

            _albumsRepositoryMock.Verify(s => s.FindAllAlbums(), Times.Once());
        }

        [Test]
        public void FindAlbumByName_Returns_Correct_AlbumDtos()
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
                StockQuantity = 1,
                PriceInPence = 1 },
            ];

            List<AlbumReturnDto> dtos =
            [
                new(1, "Name1", "Artist1", 2001, [], "Information", 1, 1),
            ];

            _albumsRepositoryMock.Setup(s => s.FindAllAlbums()).Returns(albums);

            var result = _albumsService.FindAlbumsByName("Name1");

            var jsonResult = JsonSerializer.Serialize(result);
            var jsonDto = JsonSerializer.Serialize(dtos);

            Assert.That(jsonResult, Is.EqualTo(jsonDto));
        }

        [Test]
        public void FindAlbumByName_Returns_Empty_List_If_Not_Found()
        {
            List<Album> albums =
            [
                new() {
                Id = 1,
                Name = "Name1",
                ArtistID = 1,
                ReleaseYear = 2001,
                AlbumGenres = [],
                Information = "Information",
                StockQuantity = 1,
                PriceInPence = 1 },
            ];

            List<AlbumReturnDto> emptyList = [];

            _albumsRepositoryMock.Setup(s => s.FindAllAlbums()).Returns(albums);

            var result = _albumsService.FindAlbumsByName("Name2");

            Assert.That(result, Is.EqualTo(emptyList));
        }
    }
}
