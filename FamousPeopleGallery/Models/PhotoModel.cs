using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

namespace FamousPeopleGallery.Models
{
    public class PhotoModel
    {
        public PhotoModel()
        {
            Likes = new HashSet<LikeModel>();
        }

        public int Id { get; set; }

        public DateTime CreationTime { get; set; }

        public string Name { get; set; }

        public int? ProfileId { get; set; }

        public int? AlbumId { get; set; }

        public string FileName { get; set; }

        public virtual ProfileModel Profile { get; set; }

        public virtual ICollection<LikeModel> Likes { get; set; }
    }
}
