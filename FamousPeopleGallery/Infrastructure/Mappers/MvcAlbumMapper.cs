using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FamousPeopleGallery.Models;
using ORM.Entities;

namespace FamousPeopleGallery.Infrastructure.Mappers
{
    public static class MvcAlbumMapper
    {
        public static AlbumModel ToAlbumModel(this Album album)
        {
            if (album == null)
            {
                return null;
            }

            return new AlbumModel
            {
                Id = album.Id,
                Name = album.Name,
                Photos = album.Photos.Select(l => l.ToMvcPhoto()).ToList()
            };
        }

        public static Album ToOrmAlbum(this AlbumModel albumModel)
        {
            if (albumModel == null)
            {
                return null;
            }

            return new Album
            {
                Id = albumModel.Id,
                Name = albumModel.Name,
                Photos = albumModel.Photos.Select(l => l.ToOrmPhoto()).ToList()
            };
        }


    }
}