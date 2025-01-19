using Microsoft.AspNetCore.Mvc;
using Moq;
using RecordStoreAPI.Controllers;
using RecordStoreAPI.Models;
using RecordStoreAPI.Repositories;
using RecordStoreAPI.Services;

namespace RecordStoreAPI.Tests
{
    public class ArtistServiceTests
    {
        private Mock<IArtistsRepository> _artistsRepositoryMock;
        private ArtistsService _artistsService;

        [SetUp]
        public void Setup()
        {
            _artistsRepositoryMock = new Mock<IArtistsRepository>();
            _artistsService = new ArtistsService(_artistsRepositoryMock.Object);
        }

        [Test]
        public void FindAllArtists_Calls_Correct_Service_Method()
        {
            _artistsService.FindAllArtists();

            _artistsRepositoryMock.Verify(s => s.FindAllArtists(), Times.Once());
        }

        [Test]
        public void FindAllArtists_Returns_All_Artists()
        {
            List<Artist> artists =
            [
                new() { ArtistID = 1, Name = "Artist1"},
                new() { ArtistID = 2, Name = "Artist2"}
            ];

            _artistsRepositoryMock.Setup(s => s.FindAllArtists()).Returns(artists as IEnumerable<Artist>);

            var result = _artistsService.FindAllArtists();

            Assert.That(result, Is.EquivalentTo(artists));
        }

        [Test]
        public void FindAlbumsByArtistId_Calls_Correct_Service_Method()
        {
            _artistsService.FindAlbumsByArtistId(1);

            _artistsRepositoryMock.Verify(s => s.FindAlbumsByArtistId(1), Times.Once());
        }

        [Test]
        public void FindAlbumsByArtistId_Returns_Correct_Albums()
        {
            List<AlbumReturnDto> albums = new List<AlbumReturnDto>
            {
                new(1, "Name1", "Artist1", 2001, "Genre1", "Information", 1),
                new(2, "Name2", "Artist2", 2002, "Genre2", "Information", 2)
            };

            _artistsRepositoryMock.Setup(s => s.FindAlbumsByArtistId(1)).Returns(albums);

            var result = _artistsService.FindAlbumsByArtistId(1);

            Assert.That(result, Is.EquivalentTo(albums));
        }
    }
}
