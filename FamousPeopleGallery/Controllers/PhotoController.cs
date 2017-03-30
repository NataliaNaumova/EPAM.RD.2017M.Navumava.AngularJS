using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FamousPeopleGallery.Infrastructure.Mappers;
using FamousPeopleGallery.Models;
using ORM;
using ORM.Entities;

namespace FamousPeopleGallery.Controllers
{
    public class PhotoController : Controller
    {
        private EntityModel db = new EntityModel();
        private static readonly string photoStoragePath = "~/Content/photos/";

        public JsonResult Add(string name, string data, int albumId)
        {
            var dataIndex = data.IndexOf("base64", StringComparison.Ordinal) + 7;
            var cleareData = data.Substring(dataIndex);
            var fileData = Convert.FromBase64String(cleareData);
            var bytes = fileData.ToArray();

            int startIndex = data.IndexOf(':') + 1;
            int length = data.IndexOf(';') - startIndex;
            var fullFormat = data.Substring(startIndex, length);
            var format = fullFormat.Substring(fullFormat.IndexOf("/") + 1);

            var path = GetPathToImg(name, format);
            using (var fileStream = System.IO.File.Create(path))
            {
                fileStream.Write(bytes, 0, bytes.Length);
            }

            var photo = new PhotoModel
            {
                FileName = name + "." + format,
                CreationTime = DateTime.Now.ToUniversalTime(),
                Name = name,
                ProfileId = Int32.Parse(Session["user_id"].ToString()),
                AlbumId = albumId
            };

            db.Set<Photo>().Add(photo.ToOrmPhoto());
            db.SaveChanges();

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        private string GetPathToImg(string fileName, string format)
        {
            var serverPath = Server.MapPath("~");
            return Path.Combine(serverPath, "Content", "photos", fileName) + "." + format;
        }

        public JsonResult GetAllPhotos()
        {
            var Photos = db.Photos.ToList().Select(i => i.ToMvcPhoto()).ToList();
            Photos.ForEach(i => i.FileName = Url.Content(photoStoragePath + i.FileName));

            return Json(Photos, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAlbumPhotos(int id)
        {
            List<PhotoModel> Photos;
            if (id == 0)
            { 
                Photos = db.Set<Photo>()
                    .ToList()
                    .Select(i => i.ToMvcPhoto())
                    .ToList();
            }
            else
            {
                Photos = db.Set<Album>()
                    .FirstOrDefault(a => a.Id == id)
                    .Photos
                    .ToList()
                    .Select(i => i.ToMvcPhoto())
                    .ToList();
            }

            Photos.ForEach(i => i.FileName = Url.Content(photoStoragePath + i.FileName));

            return Json(Photos, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAlbumNames()
        {
            var albums = db.Set<Album>()
                .Select(a => new { Id = a.Id, Name = a.Name })
                .ToList();
            albums.Insert(0, new { Id = 0, Name = "All" });

            return Json(albums, JsonRequestBehavior.AllowGet);
        }

    }
}
