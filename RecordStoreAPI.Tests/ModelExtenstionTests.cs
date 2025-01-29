using RecordStoreAPI.Entities;
using RecordStoreAPI.Models;
using RecordStoreFrontend.Client.Models;
using System.Text.Json;

namespace RecordStoreAPI.Tests
{
    public class ModelExtenstionTests
    {
        [Test]
        public void ToAlbumDetails_Returns_Correct_AlbumDetails()
        {
            Album album = new()
            {
                Id = 1,
                Name = "Name1",
                Artist = new() { Id = 1, Name = "Artist1" },
                ReleaseYear = 2001,
                AlbumGenres = new([new() { GenreID = GenreEnum.Folk }]),
                Information = "Information",
                ImageURL = "URL"
            };

            AlbumDetails albumDetails = new()
            {
                Id = 1,
                Name = "Name1",
                ArtistID = 1,
                ArtistName = "Artist1",
                ReleaseYear = 2001,
                Genres = [GenreEnum.Folk],
                Information = "Information",
                ImageURL = "URL"
            };

            var result = ModelExtensions.ToAlbumDetails(album);

            var jsonResult = JsonSerializer.Serialize(result);
            var jsonAlbumDetails = JsonSerializer.Serialize(albumDetails);

            Assert.That(jsonResult, Is.EqualTo(jsonAlbumDetails));
        }
       

        [Test]
        public void AlbumDetailsToAlbum_Returns_Correct_Album()
        {
            Album album = new()
            {
                Name = "Name1",
                ArtistID= 1,
                ReleaseYear = 2001,
                Information = "Information",
                ImageURL = "URL"
            };

            AlbumDetails albumDetails = new()
            {
                Id = 1,
                Name = "Name1",
                ArtistID = 1,
                ReleaseYear = 2001,
                Information = "Information",
                ImageURL = "URL"
            };

            var result = ModelExtensions.AlbumDetailsToAlbum(albumDetails);

            var jsonResult = JsonSerializer.Serialize(result);
            var jsonAlbumDetails = JsonSerializer.Serialize(album);

            Assert.That(jsonResult, Is.EqualTo(jsonAlbumDetails));
        }

        [Test]
        public void ToArtistDetails_Returns_Correct_Dto()
        {
            Artist artist = new()
            {
                Id = 1,
                Name = "Artist",
                Albums = [],
                PerformerType = "Type",
                Origin = "Origin",
                ImageURL = "URL"
            };

            ArtistDetails artistDetails = new()
            {
                Id = 1,
                Name = "Artist",
                Albums = [],
                PerformerType = "Type",
                Origin = "Origin",
                ImageURL = "URL"
            };

            var result = ModelExtensions.ToArtistDetails(artist);

            var jsonResult = JsonSerializer.Serialize(result);
            var jsonArtistDetails = JsonSerializer.Serialize(artistDetails);

            Assert.That(jsonResult, Is.EqualTo(jsonArtistDetails));
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
                AlbumGenres = new([new() { AlbumID = 1, GenreID = GenreEnum.Folk }]),
                Information = "Information",
                ImageURL = "URL"
            };

            AlbumDetails albumDetails = new()
            {
                Id = 1,
                Name = "Name2",
                ArtistID = 2,
                ArtistName = "Artist2",
                ReleaseYear = 2002,
                Genres = [GenreEnum.Indie],
                Information = "MoreInformation",
                ImageURL = "URX"
            };

            Album updatedAlbum = new()
            {
                Id = 1,
                Name = "Name2",
                ArtistID = 2,
                ReleaseYear = 2002,
                AlbumGenres = new([new() { AlbumID = 1, GenreID = GenreEnum.Indie }]),
                Information = "MoreInformation",
                ImageURL = "URX"
            };

            ModelExtensions.MapAlbumDetailsProperties(album, albumDetails);

            var jsonResult = JsonSerializer.Serialize(album);
            var jsonAlbumDetails = JsonSerializer.Serialize(updatedAlbum);

            Assert.That(jsonResult, Is.EqualTo(jsonAlbumDetails));
        }

        [Test]
        public void MapArtistProperties_Maps_Correct_Properties()
        {
            Artist artist = new()
            {
                Id = 1,
                Name = "Artist",
                Albums = [],
                PerformerType = "Type",
                Origin = "Origin",
                ImageURL = "URL"
            };

            ArtistDetails artistDetails = new()
            {
                Id = 1,
                Name = "Artist2",
                Albums = [],
                PerformerType = "Type2",
                Origin = "Origin2",
                ImageURL = "URX"
            };

            Artist updatedArtist = new()
            {
                Id = 1,
                Name = "Artist2",
                Albums = [],
                PerformerType = "Type2",
                Origin = "Origin2",
                ImageURL = "URX"
            };

            ModelExtensions.MapArtistDetailsProperties(artist, artistDetails);

            var jsonResult = JsonSerializer.Serialize(artist);
            var jsonArtistDetails = JsonSerializer.Serialize(updatedArtist);

            Assert.That(jsonResult, Is.EqualTo(jsonArtistDetails));
        }
    }
}
