using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FamousPeopleGallery.Models
{
    public class AlbumModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<PhotoModel> Photos { get; set; }
    }
}