using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using FamousPeopleGallery.Infrastructure.Mappers;
using FamousPeopleGallery.Models;
using ORM;
using ORM.Entities;

namespace FamousPeopleGallery.Controllers
{
    public class LikeController : Controller
    {
        private EntityModel db = new EntityModel();

        public JsonResult Like(int photoId)
        {
            var photo = db.Set<Photo>().FirstOrDefault(ph => ph.Id == photoId).ToMvcPhoto();

            if (Session["user_id"] == null)
            {
                return Json(photo.Likes, JsonRequestBehavior.AllowGet);
            }

            int userId = Int32.Parse(Session["user_id"].ToString());
            var profile = db.Set<Profile>().FirstOrDefault(p => p.Id == userId).ToMvcProfile();
            

            var like = new LikeModel()
            {
                PhotoId = photoId,
                ProfileId = profile.Id
            };
            if (photo.Likes.FirstOrDefault(l => l.ProfileId == profile.Id) != null)
            {
                var dbLike =
                    db.Set<Like>().First(l => l.ProfileId == like.ProfileId && l.PhotoId == like.PhotoId);
                db.Set<Like>().Remove(dbLike);
                
            }
            else
            {
                db.Set<Like>().Add(like.ToOrmLike());
            }

            db.SaveChanges();

            photo = db.Set<Photo>().FirstOrDefault(ph => ph.Id == photoId).ToMvcPhoto();

            return Json(photo.Likes, JsonRequestBehavior.AllowGet);
        }

    }
}
