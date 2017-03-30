using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FamousPeopleGallery.Controllers
{
    public class AngularController : Controller
    {
        //
        // GET: /Angular/

        public ActionResult Index()
        {
            if (Session["user_id"] == null)
            {
                Response.SetCookie(new HttpCookie("user_id", null)
                {
                    Expires = DateTime.Now.AddDays(-1)
                });
                Response.SetCookie(new HttpCookie("role", null)
                {
                    Expires = DateTime.Now.AddDays(-1)
                });
            }
            return View();
        }

    }
}
