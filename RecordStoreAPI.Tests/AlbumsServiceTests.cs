using Moq;
using RecordStoreAPI.Models;
using RecordStoreAPI.Repositories;
using RecordStoreAPI.Services;

namespace RecordStoreAPI.Tests
{
    public class AlbumsServiceTests
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
            List<AlbumReturnDto> dtoList =
            [
                new(1, "Name1", "Artist1", 2001, "Folk", "Information", 1),
                new(2, "Name2", "Artist2", 2002, "Folk", "Information", 2)
                
            ];

            _albumRepositoryMock.Setup(s => s.FindAllAlbums()).Returns(dtoList);

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
            AlbumReturnDto returnDto = new(1, "Name1", "Artist1", 2001, "Folk", "Information", 1);

            _albumRepositoryMock.Setup(s => s.FindAlbumById(1)).Returns(returnDto);

            var result = _albumService.FindAlbumById(1);

            Assert.That(result, Is.EqualTo(returnDto));
        }

        [Test]
        public void FindAlbumById_Returns_Null_If_Not_Found()
        {
            AlbumReturnDto? album = null;

            _albumRepositoryMock.Setup(s => s.FindAlbumById(1)).Returns(album);

            var result = _albumService.FindAlbumById(1);

            Assert.That(result, Is.EqualTo(null));
        }

        [Test]
        public void AddNewAlbum_Calls_Correct_Repo_Method()
        {
            AlbumPutDto putDto = new("Name1", 1, 2001, 0, "Information", 1);

            _albumService.AddNewAlbum(putDto);

            _albumRepositoryMock.Verify(s => s.AddNewAlbum(putDto), Times.Once());
        }

        [Test]
        public void AddNewAlbum_Returns_Correct_Album_Dto()
        {
            AlbumPutDto putDto = new("Name1", 1, 2001, 0, "Information", 1);
            AlbumReturnDto returnDto = new(1, "Name1", "Artist1", 2001, "Folk", "Information", 1);

            _albumRepositoryMock.Setup(s => s.AddNewAlbum(putDto)).Returns(returnDto);

            var result = _albumService.AddNewAlbum(putDto);

            Assert.That(result, Is.EqualTo(returnDto));
        }

        [Test]
        public void AddNewAlbum_Returns_Null_If_Not_Added()
        {
            AlbumPutDto putDto = new("Name1", 1, 2001, 0, "Information", 1);
            AlbumReturnDto? returnDto = null;

            _albumRepositoryMock.Setup(s => s.AddNewAlbum(putDto)).Returns(returnDto);

            var result = _albumService.AddNewAlbum(putDto);

            Assert.That(result, Is.EqualTo(returnDto));
        }

        [Test]
        public void UpdateAlbum_Calls_Correct_Repo_Method()
        {
            AlbumPutDto putDto = new("Name1", 1, 2001, 0, "Information", 1);

            _albumService.UpdateAlbum(1, putDto);

            _albumRepositoryMock.Verify(s => s.UpdateAlbum(1, putDto), Times.Once());
        }

        [Test]
        public void UpdateAlbum_Returns_Correct_Album()
        {
            AlbumPutDto putDto = new("Name1", 1, 2001, 0, "Information", 1);
            AlbumReturnDto returnDto = new(1, "Name1", "Artist1", 2001, "Folk", "Information", 1);

            _albumRepositoryMock.Setup(s => s.UpdateAlbum(1, putDto)).Returns(returnDto);

            var result = _albumService.UpdateAlbum(1, putDto);

            Assert.That(result, Is.EqualTo(returnDto));
        }

        [Test]
        public void UpdateAlbum_Returns_Null_If_Not_Updated()
        {
            AlbumPutDto putDto = new("Name1", 1, 2001, 0, "Information", 1);
            AlbumReturnDto? returnDto = null;

            _albumRepositoryMock.Setup(s => s.UpdateAlbum(1, putDto)).Returns(returnDto);

            var result = _albumService.UpdateAlbum(1, putDto);

            Assert.That(result, Is.EqualTo(returnDto));
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

        [Test]
        public void FindAlbumsByReleaseYear_Calls_Correct_Repo_Method()
        {
            _albumService.FindAllAlbums();

            _albumRepositoryMock.Verify(s => s.FindAllAlbums(), Times.Once());
        }

        [Test]
        public void FindAlbumsByReleaseYear_Returns_Correct_AlbumDtos()
        {
            List<AlbumReturnDto> albums =
            [
                new(1, "Name1", "Artist1", 2001, "Genre1", "Information", 1),
                new(2, "Name2", "Artist2", 2002, "Genre2", "Information", 2)
            ];

            _albumRepositoryMock.Setup(s => s.FindAllAlbums()).Returns(albums);

            var result = _albumService.FindAllAlbums();

            Assert.That(result, Is.EqualTo(albums));
        }

        [Test]
        public void FindAlbumsByReleaseYear_Returns_Empty_List_If_Not_Found()
        {
            List<AlbumReturnDto> albums = [];

            _albumRepositoryMock.Setup(s => s.FindAllAlbums()).Returns(albums);

            var result = _albumService.FindAllAlbums();

            Assert.That(result, Is.EqualTo(albums));
        }
    }
}
