using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LabnetClient.Controllers
{
    public class ShareController : Controller
    {
        //
        // GET: /Share/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SetCulture(string id)
        {
            HttpCookie userCookie = Request.Cookies["Culture"];
            userCookie.Value = "en-US";
            userCookie.Expires = DateTime.Now.AddYears(100);
            Response.SetCookie(userCookie);
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}
