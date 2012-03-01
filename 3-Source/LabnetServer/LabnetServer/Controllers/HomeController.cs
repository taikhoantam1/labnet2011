using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataRepository;

namespace LabnetServer.Controllers
{
    [HandleError]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ViewData["Message"] = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        [HttpPost]
        public string Login(string UserName, string Password)
        {
            LabnetAccount account = Repository.GetAccount(UserName);
            if (account != null)
            {
                if (account.Password == Password)
                {
                    return account.Domain;
                }
                else
                {
                    return "Sai mật khẩu";
                }
            }
            return "Không tồn tại tài khoản này";
        }
    }
}
