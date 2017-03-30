using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FamousPeopleGallery.Models;
using ORM.Entities;

namespace FamousPeopleGallery.Infrastructure.Mappers
{
    public static class MvcPhotoMapper
    {
        public static PhotoModel ToMvcPhoto(this Photo photo)
        {
            if (photo == null)
                return null;

            return new PhotoModel()
            {
                Id = photo.Id,
                CreationTime = photo.CreationTime,
                Name = photo.Name,
                ProfileId = photo.ProfileId,
                Profile = photo.Profile.ToMvcProfile(),
                FileName = photo.FileName,
                AlbumId = photo.AlbumId,
                Likes = photo.Likes.Select(l => l.ToMvcLike()).ToList()
            };
        }

        public static Photo ToOrmPhoto(this PhotoModel photoModel)
        {
            return new Photo()
            {
                Id = photoModel.Id,
                CreationTime = photoModel.CreationTime,
                Name = photoModel.Name,
                ProfileId = photoModel.ProfileId,
                Profile = photoModel.Profile.ToOrmProfile(),
                FileName = photoModel.FileName,
                AlbumId = photoModel.AlbumId,
                Likes = photoModel.Likes.Select(l => l.ToOrmLike()).ToList()
            };
        }
    }
}