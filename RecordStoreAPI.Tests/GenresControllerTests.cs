using Microsoft.AspNetCore.Mvc;
using Moq;
using RecordStoreAPI.Controllers;
using RecordStoreAPI.Models;
using RecordStoreAPI.Services;

namespace RecordStoreAPI.Tests
{
    public class GenresControllerTests
    {
        private Mock<IGenresService> _genresServiceMock;
        private GenresController _genresController;

        [SetUp]
        public void Setup()
        {
            _genresServiceMock = new Mock<IGenresService>();
            _genresController = new GenresController(_genresServiceMock.Object);
        }

        [Test]
        public void GetAllGenres_Calls_Correct_Service_Method()
        {
            _genresController.GetAllGenres();

            _genresServiceMock.Verify(s => s.FindAllGenres(), Times.Once());
        }

        [Test]
        public void GetAllGenres_Returns_All_Genres()
        {
            List<GenreDto> dtos =
            [
                new(Genres.Folk, "Folk")
            ];

            _genresServiceMock.Setup(s => s.FindAllGenres()).Returns(dtos);

            var result = _genresController.GetAllGenres() as ObjectResult;

            if (result is OkObjectResult okObjectResult) Assert.That(okObjectResult.Value, Is.EqualTo(dtos));
            else Assert.Fail();
        }
    }
}
