using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System;
using System.Security.Policy;

namespace ORM.Entities
{
    public class Photo
    {
        public Photo()
        {
            Likes = new HashSet<Like>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Created")]
        public DateTime CreationTime { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string FileName { get; set; }

        public int? ProfileId { get; set; }


        [ForeignKey("ProfileId")]
        public virtual Profile Profile { get; set; }

        public int? AlbumId { get; set; }

        [ForeignKey("AlbumId")]
        public virtual Album Album { get; set; }

        public virtual ICollection<Like> Likes { get; set; }

        
    }
}
