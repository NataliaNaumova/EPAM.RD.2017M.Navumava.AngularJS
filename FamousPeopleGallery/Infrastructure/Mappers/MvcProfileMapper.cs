using System.Web.Providers.Entities;
using FamousPeopleGallery.Models;
using ORM.Entities;

namespace FamousPeopleGallery.Infrastructure.Mappers
{
    public static class MvcProfileMapper
    {
        public static ProfileModel ToMvcProfile(this Profile profile)
        {
            return new ProfileModel()
            {
                Id = profile.Id,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                User = profile.User.ToMvcUser()
            };
        }

        public static Profile ToOrmProfile(this ProfileModel profileModel)
        {
            return new Profile()
            {
                Id = profileModel.Id,
                FirstName = profileModel.FirstName,
                LastName = profileModel.LastName,
                User = profileModel.User.ToOrmUser()
            };
        }
    }
}