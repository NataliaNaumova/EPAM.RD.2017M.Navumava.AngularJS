using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM.Entities;

namespace ORM
{
    public partial class EntityModel : DbContext
    {
        static EntityModel()
        {
            Database.SetInitializer(new DbInitializer());
        }

        public EntityModel()
            : base("name=EntityModel")
        {
        }

        public virtual DbSet<Album> Albums { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Like> Likes { get; set; }
        public virtual DbSet<Profile> Profiles { get; set; }

    }
}
