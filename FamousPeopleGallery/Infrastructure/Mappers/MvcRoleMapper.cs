using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Providers.Entities;
using FamousPeopleGallery.Models;
using ORM.Entities;

namespace FamousPeopleGallery.Infrastructure.Mappers
{
    public static class MvcRoleMapper
    {
        public static RoleModel ToDalRole(this Role role)
        {
            if (role == null)
                return null;
            return new RoleModel()
            {
                Id = role.Id,
                Name = role.Name
            };
        }

        public static Role ToOrmRole(this RoleModel roleModel)
        {
            if (roleModel == null)
                return null;
            return new Role()
            {
                Id = roleModel.Id,
                Name = roleModel.Name
            };
        }
    }
}