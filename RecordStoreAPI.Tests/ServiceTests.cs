using Moq;
using RecordStoreAPI.Models;
using RecordStoreAPI.Repositories;
using RecordStoreAPI.Services;

namespace RecordStoreAPI.Tests
{
    public class ServiceTests
    {
        private Mock<IAlbumRepository> _albumRepositoryMock;
        private AlbumService _albumService;

        [SetUp]
        public void Setup()
        {
            _albumRepositoryMock = new Mock<IAlbumRepository>();
            _albumService = new AlbumService(_albumRepositoryMock.Object);
        }

        [Test]
        public void FindAllAlbums_Calls_Correct_Repo_Method()
        {
            _albumService.FindAllAlbums();

            _albumRepositoryMock.Verify(s => s.FindAllAlbums(), Times.Once());
        }

        [Test]
        public void FindAllAlbums_Returns_All_Albums()
        {
            List<Album> albumList = new List<Album>
            {
                new() { Id = 1, Name = "Name1", Artist = "Artist1", ReleaseYear = 2001, Genre = Genres.Folk, StockQuantity = 1 },
                new() { Id = 2, Name = "Name2", Artist = "Artist2", ReleaseYear = 2002, Genre = Genres.Hip_Hop, StockQuantity = 2 }
            };

            List<AlbumDto> dtoList = new List<AlbumDto>
            {
                new(1, "Name1", "Artist1", 2001, "Folk", 1),
                new(2, "Name2", "Artist2", 2002, "Hip-Hop", 2)
            };

            _albumRepositoryMock.Setup(s => s.FindAllAlbums()).Returns(albumList as IEnumerable<Album>);

            var result = _albumService.FindAllAlbums();

            Assert.That(result, Is.EquivalentTo(dtoList));
        }

        [Test]
        public void FindAlbumById_Calls_Correct_Repo_Method()
        {
            _albumService.FindAlbumById(1);

            _albumRepositoryMock.Verify(s => s.FindAlbumById(1), Times.Once());
        }

        [Test]
        public void FindAlbumById_Returns_Correct_AlbumDto()
        {
            Album album = new() { Id = 1, Name = "Name1", Artist = "Artist1", ReleaseYear = 2001, Genre = Genres.Folk, StockQuantity = 1 };
            AlbumDto? albumDto  = new(1, "Name1", "Artist1", 2001, "Folk", 1);

            _albumRepositoryMock.Setup(s => s.FindAlbumById(1)).Returns(album);

            var result = _albumService.FindAlbumById(1);

            Assert.That(result, Is.EqualTo(albumDto));
        }

        [Test]
        public void FindAlbumById_Returns_Null_If_Not_Found()
        {
            Album? album = null;

            _albumRepositoryMock.Setup(s => s.FindAlbumById(1)).Returns(album);

            var result = _albumService.FindAlbumById(1);

            Assert.That(result, Is.EqualTo(null));
        }

        [Test]
        public void AddNewAlbum_Calls_Correct_Repo_Method()
        {
            Album album = new() { Id = 1, Name = "Name1", Artist = "Artist1", ReleaseYear = 2001, Genre = Genres.Folk, StockQuantity = 1 };

            _albumService.AddNewAlbum(album);

            _albumRepositoryMock.Verify(s => s.AddNewAlbum(album), Times.Once());
        }

        [Test]
        public void AddNewAlbum_Returns_Correct_Album_Dto()
        {
            Album album = new() { Id = 1, Name = "Name1", Artist = "Artist1", ReleaseYear = 2001, Genre = Genres.Folk, StockQuantity = 1 };
            AlbumDto? albumDto = new(1, "Name1", "Artist1", 2001, "Folk", 1);

            _albumRepositoryMock.Setup(s => s.AddNewAlbum(album)).Returns(album);

            var result = _albumService.AddNewAlbum(album);

            Assert.That(result, Is.EqualTo(albumDto));
        }

        [Test]
        public void AddNewAlbum_Returns_Null_If_Not_Added()
        {
            Album album = new() { Id = 1, Name = "Name1", Artist = "Artist1", ReleaseYear = 2001, Genre = Genres.Folk, StockQuantity = 1 };
            Album? nullAlbum = null;
            AlbumDto? albumDto = null;

            _albumRepositoryMock.Setup(s => s.AddNewAlbum(album)).Returns(nullAlbum);

            var result = _albumService.AddNewAlbum(album);

            Assert.That(result, Is.EqualTo(albumDto));
        }

        [Test]
        public void UpdateAlbum_Calls_Correct_Repo_Method()
        {
            Album album = new() { Id = 1, Name = "Name1", Artist = "Artist1", ReleaseYear = 2001, Genre = Genres.Folk, StockQuantity = 1 };

            _albumService.UpdateAlbum(1, album);

            _albumRepositoryMock.Verify(s => s.UpdateAlbum(1, album), Times.Once());
        }

        [Test]
        public void UpdateAlbum_Returns_Correct_Album()
        {
            Album album = new() { Id = 1, Name = "Name1", Artist = "Artist1", ReleaseYear = 2001, Genre = Genres.Folk, StockQuantity = 1 };
            AlbumDto? albumDto = new(1, "Name1", "Artist1", 2001, "Folk", 1);

            _albumRepositoryMock.Setup(s => s.UpdateAlbum(1, album)).Returns(album);

            var result = _albumService.UpdateAlbum(1, album);

            Assert.That(result, Is.EqualTo(albumDto));
        }

        [Test]
        public void UpdateAlbum_Returns_Null_If_Not_Updated()
        {
            Album album = new() { Id = 1, Name = "Name1", Artist = "Artist1", ReleaseYear = 2001, Genre = Genres.Folk, StockQuantity = 1 };
            Album? nullAlbum = null;
            AlbumDto? albumDto = null;

            _albumRepositoryMock.Setup(s => s.UpdateAlbum(1, album)).Returns(nullAlbum);

            var result = _albumService.UpdateAlbum(1,album);

            Assert.That(result, Is.EqualTo(albumDto));
        }

        [Test]
        public void TryRemoveAlbumById_Calls_Correct_Repo_Method()
        {
            _albumService.TryRemoveAlbumById(1);

            _albumRepositoryMock.Verify(s => s.TryRemoveAlbumById(1), Times.Once());
        }

        [Test]
        public void TryRemoveAlbumById_Returns_True()
        {
            _albumRepositoryMock.Setup(s => s.TryRemoveAlbumById(1)).Returns(true);

            var result = _albumService.TryRemoveAlbumById(1);

            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void TryRemoveAlbumById_Returns_False()
        {
            _albumRepositoryMock.Setup(s => s.TryRemoveAlbumById(1)).Returns(false);

            var result = _albumService.TryRemoveAlbumById(1);

            Assert.That(result, Is.EqualTo(false));
        }
    }
}
