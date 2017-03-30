using FamousPeopleGallery.Models;
using ORM.Entities;

namespace FamousPeopleGallery.Infrastructure.Mappers
{
    public static class MvcLikeMapper
    {
        public static LikeModel ToMvcLike(this Like like)
        {
            if (like == null)
            {
                return null;
            }

            return new LikeModel
            {
                Id = like.Id,
                ProfileId = like.ProfileId,
                PhotoId = like.PhotoId
            };
        }

        public static Like ToOrmLike(this LikeModel likeModel)
        {
            if (likeModel == null)
            {
                return null;
            }

            return new Like
            {
                Id = likeModel.Id,
                ProfileId = likeModel.ProfileId,
                PhotoId = likeModel.PhotoId
            };
        }
    }
}