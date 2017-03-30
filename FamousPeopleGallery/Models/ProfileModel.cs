using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FamousPeopleGallery.Models
{
    public class ProfileModel
    {
        public ProfileModel()
        {
            Photos = new HashSet<PhotoModel>();
            Likes = new HashSet<LikeModel>();
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual UserModel User { get; set; }
        public virtual ICollection<PhotoModel> Photos { get; set; }
        public virtual ICollection<LikeModel> Likes { get; set; }
    }
}