using System.ComponentModel.DataAnnotations;

namespace FamousPeopleGallery.Models
{
    public class LikeModel
    {
        public int Id { get; set; }

        public int ProfileId { get; set; }

        public int PhotoId { get; set; }

        public virtual ProfileModel Profile { get; set; }
        public virtual PhotoModel Photo { get; set; }


    }
}
