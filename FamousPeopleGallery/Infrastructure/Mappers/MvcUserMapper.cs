using FamousPeopleGallery.Models;
using ORM.Entities;

namespace FamousPeopleGallery.Infrastructure.Mappers
{
    public static class MvcUserMapper
    {
        public static UserModel ToMvcUser(this User userEntity)
        {
            if (userEntity == null)
            {
                return null;
            }

            return new UserModel
            {
                Id = userEntity.Id,
                Login = userEntity.Login,
                Password = userEntity.Password,
                Email = userEntity.Email,
                RoleId = userEntity.RoleId
            };
        }

        public static User ToOrmUser(this UserModel userModel)
        {
            if (userModel == null)
            {
                return null;
            }

            return new User
            {
                Id = userModel.Id,
                Login = userModel.Login,
                Password = userModel.Password,
                Email = userModel.Email,
                RoleId = userModel.RoleId
            };
        }
    }
}