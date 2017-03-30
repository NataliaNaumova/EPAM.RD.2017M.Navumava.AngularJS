using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FamousPeopleGallery.Models
{
    public class RoleModel
    {
        public RoleModel()
        {
            Users = new HashSet<UserModel>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<UserModel> Users { get; set; }

    }
}

