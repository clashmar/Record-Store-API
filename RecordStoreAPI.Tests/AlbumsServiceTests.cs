using Moq;
using RecordStoreAPI.Models;
using RecordStoreAPI.Repositories;
using RecordStoreAPI.Services;

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
            _albumsService.FindAllAlbums();

            _albumsRepositoryMock.Verify(s => s.FindAllAlbums(), Times.Once());
        }

        [Test]
        public void FindAllAlbums_Returns_All_Albums()
        {
            List<AlbumReturnDto> allAlbumDtos =
            [
                new(1, "Name1", "Artist1", 2001, "Folk", "Information", 1),
                new(2, "Name2", "Artist2", 2002, "Folk", "Information", 2)
                
            ];

            _albumsRepositoryMock.Setup(s => s.FindAllAlbums()).Returns(allAlbumDtos);

            var result = _albumsService.FindAllAlbums();

            Assert.That(result, Is.EquivalentTo(allAlbumDtos));
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
            AlbumReturnDto returnDto = new(1, "Name1", "Artist1", 2001, "Folk", "Information", 1);

            _albumsRepositoryMock.Setup(s => s.FindAlbumById(1)).Returns(returnDto);

            var result = _albumsService.FindAlbumById(1);

            Assert.That(result, Is.EqualTo(returnDto));
        }

        [Test]
        public void FindAlbumById_Returns_Null_If_Not_Found()
        {
            AlbumReturnDto? album = null;

            _albumsRepositoryMock.Setup(s => s.FindAlbumById(1)).Returns(album);

            var result = _albumsService.FindAlbumById(1);

            Assert.That(result, Is.EqualTo(null));
        }

        [Test]
        public void AddNewAlbum_Calls_Correct_Repo_Method()
        {
            AlbumPutDto putDto = new("Name1", 1, 2001, 0, "Information", 1);

            _albumsService.AddNewAlbum(putDto);

            _albumsRepositoryMock.Verify(s => s.AddNewAlbum(putDto), Times.Once());
        }

        [Test]
        public void AddNewAlbum_Returns_Correct_Album_Dto()
        {
            AlbumPutDto putDto = new("Name1", 1, 2001, 0, "Information", 1);
            AlbumReturnDto returnDto = new(1, "Name1", "Artist1", 2001, "Folk", "Information", 1);

            _albumsRepositoryMock.Setup(s => s.AddNewAlbum(putDto)).Returns(returnDto);

            var result = _albumsService.AddNewAlbum(putDto);

            Assert.That(result, Is.EqualTo(returnDto));
        }

        [Test]
        public void AddNewAlbum_Returns_Null_If_Not_Added()
        {
            AlbumPutDto putDto = new("Name1", 1, 2001, 0, "Information", 1);
            AlbumReturnDto? returnDto = null;

            _albumsRepositoryMock.Setup(s => s.AddNewAlbum(putDto)).Returns(returnDto);

            var result = _albumsService.AddNewAlbum(putDto);

            Assert.That(result, Is.EqualTo(returnDto));
        }

        [Test]
        public void UpdateAlbum_Calls_Correct_Repo_Method()
        {
            AlbumPutDto putDto = new("Name1", 1, 2001, 0, "Information", 1);

            _albumsService.UpdateAlbum(1, putDto);

            _albumsRepositoryMock.Verify(s => s.UpdateAlbum(1, putDto), Times.Once());
        }

        [Test]
        public void UpdateAlbum_Returns_Correct_Album()
        {
            AlbumPutDto putDto = new("Name1", 1, 2001, 0, "Information", 1);
            AlbumReturnDto returnDto = new(1, "Name1", "Artist1", 2001, "Folk", "Information", 1);

            _albumsRepositoryMock.Setup(s => s.UpdateAlbum(1, putDto)).Returns(returnDto);

            var result = _albumsService.UpdateAlbum(1, putDto);

            Assert.That(result, Is.EqualTo(returnDto));
        }

        [Test]
        public void UpdateAlbum_Returns_Null_If_Not_Updated()
        {
            AlbumPutDto putDto = new("Name1", 1, 2001, 0, "Information", 1);
            AlbumReturnDto? returnDto = null;

            _albumsRepositoryMock.Setup(s => s.UpdateAlbum(1, putDto)).Returns(returnDto);

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
            List<AlbumReturnDto> allAlbumDtos =
            [
                new(1, "Name1", "Artist1", 2001, "Genre1", "Information", 1),
            ];

            _albumsRepositoryMock.Setup(s => s.FindAllAlbums()).Returns(allAlbumDtos);

            _albumsService.FindAlbumsByReleaseYear(2025);

            _albumsRepositoryMock.Verify(s => s.FindAllAlbums(), Times.Once());
        }

        [Test]
        public void FindAlbumsByReleaseYear_Returns_Correct_AlbumDtos()
        {
            List<AlbumReturnDto> allAlbumDtos =
            [
                new(1, "Name1", "Artist1", 2001, "Genre1", "Information", 1),
                new(1, "Name2", "Artist1", 2002, "Genre1", "Information", 1)
            ];

            List<AlbumReturnDto> albumsWithYear =
            [
                new(1, "Name1", "Artist1", 2001, "Genre1", "Information", 1),
            ];

            _albumsRepositoryMock.Setup(s => s.FindAllAlbums()).Returns(allAlbumDtos);

            var result = _albumsService.FindAlbumsByReleaseYear(2001);

            Assert.That(result, Is.EqualTo(albumsWithYear));
        }

        [Test]
        public void FindAlbumsByReleaseYear_Returns_Empty_List_If_Not_Found()
        {
            List<AlbumReturnDto> emptyList = [];

            _albumsRepositoryMock.Setup(s => s.FindAllAlbums()).Returns(emptyList);

            var result = _albumsService.FindAlbumsByReleaseYear(2025);

            Assert.That(result, Is.EqualTo(emptyList));
        }

        [Test]
        public void FindAlbumsByGenre_Calls_Correct_Repo_Method()
        {
            List<AlbumReturnDto> allAlbumDtos =
            [
                new(1, "Name1", "Artist1", 2001, "Genre1", "Information", 1),
            ];

            _albumsRepositoryMock.Setup(s => s.FindAllAlbums()).Returns(allAlbumDtos);

            _albumsService.FindAlbumsByGenre(Genres.Folk);

            _albumsRepositoryMock.Verify(s => s.FindAllAlbums(), Times.Once());
        }

        [Test]
        public void FindAlbumsByGenre_Returns_Correct_AlbumDtos()
        {
            List<AlbumReturnDto> allAlbumDtos =
            [
                new(1, "Name1", "Artist1", 2001, "Folk", "Information", 1),
                new(1, "Name2", "Artist1", 2002, "Genre2", "Information", 1)
            ];

            List<AlbumReturnDto> albumsWithGenre =
            [
                new(1, "Name1", "Artist1", 2001, "Folk", "Information", 1),
            ];

            _albumsRepositoryMock.Setup(s => s.FindAllAlbums()).Returns(allAlbumDtos);

            var result = _albumsService.FindAlbumsByGenre(Genres.Folk);

            Assert.That(result, Is.EqualTo(albumsWithGenre));
        }

        [Test]
        public void FindAlbumsByGenre_Returns_Empty_List_If_Not_Found()
        {
            List<AlbumReturnDto> emptyList = [];

            _albumsRepositoryMock.Setup(s => s.FindAllAlbums()).Returns(emptyList);

            var result = _albumsService.FindAlbumsByGenre(Genres.Folk);

            Assert.That(result, Is.EqualTo(emptyList));
        }

        [Test]
        public void FindAlbumByName_Calls_Correct_Repo_Method()
        {
            List<AlbumReturnDto> allAlbumDtos =
            [
                new(1, "Name1", "Artist1", 2001, "Genre1", "Information", 1),
            ];

            _albumsRepositoryMock.Setup(s => s.FindAllAlbums()).Returns(allAlbumDtos);

            _albumsService.FindAlbumByName("Name1");

            _albumsRepositoryMock.Verify(s => s.FindAllAlbums(), Times.Once());
        }

        [Test]
        public void FindAlbumByName_Returns_Correct_AlbumDtos()
        {
            List<AlbumReturnDto> allAlbumDtos =
            [
                new(1, "Name1", "Artist1", 2001, "Genre1", "Information", 1),
                new(1, "Name2", "Artist1", 2001, "Genre1", "Information", 1)
            ];

            List<AlbumReturnDto> albumsWithName =
            [
                new(1, "Name1", "Artist1", 2001, "Genre1", "Information", 1),
            ];

            _albumsRepositoryMock.Setup(s => s.FindAllAlbums()).Returns(allAlbumDtos);

            var result = _albumsService.FindAlbumByName("Name1");

            Assert.That(result, Is.EqualTo(albumsWithName));
        }

        [Test]
        public void FindAlbumByName_Returns_Empty_List_If_Not_Found()
        {
            List<AlbumReturnDto> emptyList = [];

            _albumsRepositoryMock.Setup(s => s.FindAllAlbums()).Returns(emptyList);

            var result = _albumsService.FindAlbumByName("Name");

            Assert.That(result, Is.EqualTo(emptyList));
        }
    }
}
