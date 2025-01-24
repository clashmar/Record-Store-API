using RecordStoreAPI.Entities;
using RecordStoreAPI.Models;
using System.Text.Json;

namespace RecordStoreAPI.Tests
{
    public class DTOExtensionTests
    {
        [Test]
        public void ToAlbumReturnDto_Returns_Correct_Dto()
        {
            Album album = new() {
                Id = 1,
                Name = "Name1",
                Artist = new() { Name = "Name1" },
                ReleaseYear = 2001,
                AlbumGenres = new([new() { GenreID = Genres.Folk }]),
                Information = "Information",
                StockQuantity = 1,
                PriceInPence = 1
            };

            AlbumReturnDto dto = new(1, "Name1", "Name1", 2001, new(["Folk"]), "Information", 1, 1);

            var result = DTOExtensions.ToAlbumReturnDto(album);

            var jsonResult = JsonSerializer.Serialize(result);
            var jsonDto = JsonSerializer.Serialize(dto);

            Assert.That(jsonResult, Is.EqualTo(jsonDto));
        }

        [Test]
        public void ToAlbumReturnDto_Returns_With_Null_Artist()
        {
            Album album = new()
            {
                Id = 1,
                Name = "Name1",
                Artist = null,
                ReleaseYear = 2001,
                AlbumGenres = [],
                Information = "Information",
                StockQuantity = 1,
                PriceInPence = 1
            };

            AlbumReturnDto dto = new(1, "Name1", null, 2001, [], "Information", 1, 1);

            var result = DTOExtensions.ToAlbumReturnDto(album);

            var jsonResult = JsonSerializer.Serialize(result);
            var jsonDto = JsonSerializer.Serialize(dto);

            Assert.That(jsonResult, Is.EqualTo(jsonDto));
        }

        [Test]
        public void PutDtoToAlbum_Returns_Correct_Album()
        {
            AlbumPutDto dto = new("Name1", 1, 2001, new([Genres.Folk]), "Information", 1, 1);

            Album album = new()
            {
                Name = "Name1",
                ArtistID = 1,
                ReleaseYear = 2001,
                Information = "Information",
                StockQuantity = 1,
                PriceInPence = 1
            };

            var result = DTOExtensions.PutDtoToAlbum(dto);

            var jsonResult = JsonSerializer.Serialize(result);
            var jsonDto = JsonSerializer.Serialize(album);

            Assert.That(jsonResult, Is.EqualTo(jsonDto));
        }

        [Test]
        public void ToArtistDto_Returns_Correct_Dto()
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
                PriceInPence = 1},
            ];

            Artist artist = new() { ArtistID = 1, Name = "Name1", Albums = albums };

            List<AlbumReturnDto> dtos =
            [
                new(1, "Name1", "Artist1", 2001, [], "Information", 1, 1),
            ];

            ArtistDto dto = new(1, "Name1", dtos);

            var result = DTOExtensions.ToArtistDto(artist);

            var jsonResult = JsonSerializer.Serialize(result);
            var jsonDto = JsonSerializer.Serialize(dto);

            Assert.That(jsonResult, Is.EqualTo(jsonDto));
        }

        [Test]
        public void MapAlbumProperties_Maps_Correct_Properties()
        {
            Album album = new()
            {
                Id = 1,
                Name = "Name1",
                ArtistID = 1,
                ReleaseYear = 2001,
                AlbumGenres = new([new() { AlbumID = 1, GenreID = Genres.Folk }]),
                Information = "Information",
                StockQuantity = 1,
                PriceInPence = 1
            };

            AlbumPutDto dto = new("Name2", 2, 2002, new([Genres.Indie]), "MoreInformation", 2, 2);

            Album updatedAlbum = new()
            {
                Id = 1,
                Name = "Name2",
                ArtistID = 2,
                ReleaseYear = 2002,
                AlbumGenres = new([new() { AlbumID = 1, GenreID = Genres.Indie }]),
                Information = "MoreInformation",
                StockQuantity = 2,
                PriceInPence = 2
            };

            DTOExtensions.MapAlbumProperties(album, dto);

            var jsonResult = JsonSerializer.Serialize(album);
            var jsonDto = JsonSerializer.Serialize(updatedAlbum);

            Assert.That(jsonResult, Is.EqualTo(jsonDto));
        }

        [Test]
        public void MapAlbumProperties_Genres_Is_Empty()
        {
            Album album = new()
            {
                Id = 1,
                Name = "Name1",
                ArtistID = 1,
                ReleaseYear = 2001,
                AlbumGenres = new([new() { AlbumID = 1, GenreID = Genres.Folk }]),
                Information = "Information",
                StockQuantity = 1,
                PriceInPence = 1
            };

            AlbumPutDto dto = new("Name2", 2, 2002, [], "MoreInformation", 2, 2);

            Album updatedAlbum = new()
            {
                Id = 1,
                Name = "Name2",
                ArtistID = 2,
                ReleaseYear = 2002,
                AlbumGenres = [],
                Information = "MoreInformation",
                StockQuantity = 2,
                PriceInPence = 2
            };

            DTOExtensions.MapAlbumProperties(album, dto);

            var jsonResult = JsonSerializer.Serialize(album);
            var jsonDto = JsonSerializer.Serialize(updatedAlbum);

            Assert.That(jsonResult, Is.EqualTo(jsonDto));
        }
    }
}
