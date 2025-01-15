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
            List<Album> albums = new List<Album>
            {
                new Album() { Id = 1, Name = "Name1", Artist = "Artist1", ReleaseYear = 2001, Genre = "Genre1", StockQuantity = 1 },
                new Album() { Id = 2, Name = "Name2", Artist = "Artist2", ReleaseYear = 2002, Genre = "Genre2", StockQuantity = 2 }
            };

            _albumRepositoryMock.Setup(s => s.FindAllAlbums()).Returns(albums as IEnumerable<Album>);

            var result = _albumService.FindAllAlbums();

            Assert.That(result, Is.EquivalentTo(albums.ToList()));
        }
    }
}
