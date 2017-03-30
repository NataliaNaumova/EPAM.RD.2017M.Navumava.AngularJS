using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FamousPeopleGallery.Models;
using ORM;
using System.Web.Helpers;
using FamousPeopleGallery.Infrastructure.Mappers;
using ORM.Entities;


namespace FamousPeopleGallery.Controllers
{
    public class AccountController : Controller
    {
        private EntityModel db = new EntityModel();

        public JsonResult Login(string login, string password)
        {
            var user = db.Set<User>().FirstOrDefault(u => u.Login == login).ToMvcUser();

            if (user != null && Crypto.VerifyHashedPassword(user.Password, password))
            {
                Session["user_id"] = user.Id;
                Session["role"] = db.Set<Role>().First(r => r.Id == user.RoleId).Name;
                Response.SetCookie(new HttpCookie("user_id", user.Id.ToString()));
                Response.SetCookie(new HttpCookie("role", user.Id.ToString()));
                return Json(new { success = true, message = "User was successfully authorized." }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false, message = "Wrong login or password." }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LogOut()
        {
            Session.Clear();
            Response.SetCookie(new HttpCookie("user_id", null)
            {
                Expires = DateTime.Now.AddDays(-1)
            });
            Response.SetCookie(new HttpCookie("role", null)
            {
                Expires = DateTime.Now.AddDays(-1)
            });
            return Json(new { success = true, message = "User successfully logged out." }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Register(string email, string login, string password)
        {
            if (db.Set<User>().FirstOrDefault(u => u.Email == email) != null)
            {
                return Json(new {success = false, message = "User with this email already exists."}, JsonRequestBehavior.AllowGet);
            }
            if (db.Set<User>().FirstOrDefault(u => u.Login == login) != null)
            {
                return Json(new { success = false, message = "User with this login already exists." }, JsonRequestBehavior.AllowGet);
            }

            var user = new UserModel
            {
                Email = email,
                Login = login,
                Password = Crypto.HashPassword(password),
                RoleId = db.Set<Role>().First(role => role.Name == "User").Id
            };

            db.Set<User>().Add(user.ToOrmUser());

            user = db.Set<User>().FirstOrDefault(u => u.Login == login).ToMvcUser();

            var profile = new ProfileModel
            {
                Id = user.Id
            };

            db.Set<Profile>().Add(profile.ToOrmProfile());
            db.SaveChanges();

            return Json(new {success = true, message = "User was successfully registered."}, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUserLogin()
        {
            string login = null;

            if (Session["user_id"] != null)
            {
                int currentUserId = int.Parse(Session["user_id"].ToString());
                UserModel user = db.Set<User>().FirstOrDefault(u => u.Id == currentUserId).ToMvcUser();

                if (user != null)
                {
                    login = user.Login;
                }
            }

            return Json(new {login = login}, JsonRequestBehavior.AllowGet);
        }

    }
}
