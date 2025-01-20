﻿namespace RecordStoreAPI.Models
{
    public class DTOExtensions
    {
        public static AlbumReturnDto? ToAlbumReturnDto(Album album)
        {
            return new AlbumReturnDto(
                        album.Id,
                        album.Name,
                        album.Artist?.Name,
                        album.ReleaseYear,
                        album.AlbumGenres?.Select(g => Genre.ToString(g.GenreID)).ToList()!,
                        album.Information,
                        album.StockQuantity
                        );
        }
        public static Album PutDtoToAlbum(AlbumPutDto albumPutDto)
        {
            return new Album()
            {
                Name = albumPutDto.Name,
                ArtistID = albumPutDto.ArtistID,
                ReleaseYear = albumPutDto.ReleaseYear,
                Information = albumPutDto.Information,
                StockQuantity = albumPutDto.StockQuantity,
            };
        }

        public static ArtistDto ToArtistDto(Artist artist)
        {
            return new ArtistDto(
                artist.ArtistID,
                artist.Name!,
                artist.Albums?.Select(a => ToAlbumReturnDto(a)).ToList()!
                );
        }
        
        public static void MapAlbumProperties(Album target, AlbumPutDto source)
        {
            target.Name = source.Name;
            target.ArtistID = source.ArtistID;
            target.ReleaseYear = source.ReleaseYear;
            target.AlbumGenres = source.Genres?
                .Select(g => new AlbumGenre() { AlbumID = target.Id, GenreID = g })
                .ToList()!;
            target.Information = source.Information;
            target.StockQuantity = source.StockQuantity;
        }
    }
}
