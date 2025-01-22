using Moq;
using RecordStoreAPI.Entities;
using RecordStoreAPI.Repositories;
using RecordStoreAPI.Services;

namespace RecordStoreAPI.Tests
{
    public class GenresServiceTests
    {
        private Mock<IGenresRepository> _genresRepositoryMock;
        private GenresService _genresService;

        [SetUp]
        public void Setup()
        {
            _genresRepositoryMock = new Mock<IGenresRepository>();
            _genresService = new GenresService(_genresRepositoryMock.Object);
        }

        [Test]
        public void GetAllGenres_Calls_Correct_Service_Method()
        {
            _genresService.FindAllGenres();

            _genresRepositoryMock.Verify(s => s.FindAllGenres(), Times.Once());
        }

        [Test]
        public void GetAllGenres_Returns_All_Genres()
        {
            IEnumerable<Genre> genres =
            [
                new() { GenreID = Genres.Folk, Name = "Folk" }
            ];

            List<GenreDto> dtos =
            [
                new(Genres.Folk, "Folk")
            ];

            _genresRepositoryMock.Setup(s => s.FindAllGenres()).Returns(genres);

            var result = _genresService.FindAllGenres();

            Assert.That(result, Is.EqualTo(dtos));
        }
    }
}
