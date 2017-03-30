using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FamousPeopleGallery.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public int RoleId { get; set; }

        public virtual RoleModel Role { get; set; }

        public virtual ProfileModel Profile { get; set; }

    }
}

