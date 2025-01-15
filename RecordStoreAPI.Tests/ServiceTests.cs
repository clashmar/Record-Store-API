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
                new Album() { Id = 1, Name = "Name1", Artist = "Artist1", ReleaseYear = 2001, Genre = Genres.Folk, StockQuantity = 1 },
                new Album() { Id = 2, Name = "Name2", Artist = "Artist2", ReleaseYear = 2002, Genre = Genres.Hip_Hop, StockQuantity = 2 }
            };

            List<AlbumDto> dtoList = new List<AlbumDto>
            {
                new AlbumDto(1, "Name1", "Artist1", 2001, "Folk", 1),
                new AlbumDto(2, "Name2", "Artist2", 2002, "Hip-Hop", 2)
            };

            _albumRepositoryMock.Setup(s => s.FindAllAlbums()).Returns(albumList as IEnumerable<Album>);

            var result = _albumService.FindAllAlbums();

            Assert.That(result, Is.EquivalentTo(dtoList));
        }
    }
}
