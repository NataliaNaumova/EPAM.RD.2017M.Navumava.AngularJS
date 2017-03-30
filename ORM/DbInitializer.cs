using System;
using System.Data.Entity;
using System.IO;
using System.Web.Helpers;
using ORM.Entities;

namespace ORM
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<EntityModel> //Different modes (4 types)
    {
        protected override void Seed(EntityModel context)
        {
            #region Roles initializing
            var role = new Role
            {
                Name = "User"
            };
            var adminRole = new Role
            {
                Name = "Admin"
            };
            context.Roles.Add(role);
            context.Roles.Add(adminRole);
            context.SaveChanges();
            #endregion

            #region Users initializing
            var user1 = new User
            {
                Email = "natallianavumava@gmail.com",
                Password = Crypto.HashPassword("123456"),
                Role = adminRole,
                Login = "Natalia"
            };
            var user2 = new User
            {
                Email = "alenka.borikov@gmail.com",
                Password = Crypto.HashPassword("123456"),
                Role = role,
                Login = "Alyona"
            };
            var user3 = new User
            {
                Email = "artya12@gmail.com",
                Password = Crypto.HashPassword("123456"),
                Role = role,
                Login = "Artyom"
            };

            context.Users.Add(user1);
            context.Users.Add(user2);
            context.Users.Add(user3);
            context.SaveChanges();

            #endregion

            #region Profiles initializing

            var profile1 = new Profile
            {
                FirstName = "Natalia",
                LastName = "Naumova",
                User = user1
            };
            var profile2 = new Profile
            {
                FirstName = "Alyona",
                LastName = "Borikova",
                User = user2
            };
            var profile3 = new Profile
            {
                FirstName = "Artyom",
                LastName = "Kazakov",
                User = user3
            };

            context.Profiles.Add(profile1);
            context.Profiles.Add(profile2);
            context.Profiles.Add(profile3);
            context.SaveChanges();

            #endregion

            #region Albums initialiazing

            var album1 = new Album
            {
                Name = "Actors"
            };

            var album2 = new Album
            {
                Name = "Models"
            };

            var album3 = new Album
            {
                Name = "Bussinessmen"
            };

            var album4 = new Album
            {
                Name = "Scientists"
            };

            context.Albums.Add(album1);
            context.Albums.Add(album2);
            context.Albums.Add(album3);
            context.Albums.Add(album4);
            context.SaveChanges();

            #endregion

            #region Photos inializing

            var photo1 = new Photo
            {
                Album = album1,
                CreationTime = DateTime.Now.ToUniversalTime(),
                Name = "Pierce Brosnan",
                Profile = profile1,
                FileName = "Brosnan.png",
            };

            var photo2 = new Photo
            {
                Album = album1,
                CreationTime = DateTime.Now.ToUniversalTime(),
                Name = "Brad Pitt",
                Profile = profile1,
                FileName = "pitt.jpg",
            };

            var photo3 = new Photo
            {
                Album = album1,
                CreationTime = DateTime.Now.ToUniversalTime(),
                Name = "Leonardo DiCaprio",
                Profile = profile1,
                FileName = "Dicaprio.jpg",
            };

            var photo4 = new Photo
            {
                Album = album1,
                CreationTime = DateTime.Now.ToUniversalTime(),
                Name = "Will Smith",
                Profile = profile1,
                FileName = "Smith.jpg",
            };

            var photo5 = new Photo
            {
                Album = album1,
                CreationTime = DateTime.Now.ToUniversalTime(),
                Name = "Jim Carrey",
                Profile = profile1,
                FileName = "Carrey.jpg",
            };

            var photo6 = new Photo
            {
                Album = album1,
                CreationTime = DateTime.Now.ToUniversalTime(),
                Name = "Jake Gyllenhaal",
                Profile = profile1,
                FileName = "dzhillenxoul.jpg",
            };

            var photo7 = new Photo
            {
                Album = album2,
                CreationTime = DateTime.Now.ToUniversalTime(),
                Name = "Kate Moss",
                Profile = profile1,
                FileName = "moss.jpg",
            };

            context.Photos.Add(photo1);
            context.Photos.Add(photo2);
            context.Photos.Add(photo3);
            context.Photos.Add(photo4);
            context.Photos.Add(photo5);
            context.Photos.Add(photo6);
            context.Photos.Add(photo7);
            context.SaveChanges();

            #endregion
        }
    }
}