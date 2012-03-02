using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataRepository;
using System.Web.Caching;
using LibraryFuntion;
namespace LabnetClient.Controllers
{
    public class UserController : BaseController
    {
        //
        // GET: /User/

        public ActionResult Index()
        {
            return View();
        }

        public string GetMD5Hash(string input)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(input);
            bs = x.ComputeHash(bs);
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            string password = s.ToString();
            return password;
        }

        public JsonpResult Login(string UserName, string Password)
        {
            LabUser account = Repository.GetLabUser(UserName, Password);
            if (account != null)
            {
               string token=GetMD5Hash( DateTime.Now.Ticks.ToString()+UserName+Password);
               System.Web.HttpContext.Current.Cache.Insert(token, account, null, DateTime.Now.AddSeconds(30), Cache.NoSlidingExpiration);
               return this.Jsonp(token);
            }
            return this.Jsonp("False");
            
        }

    }
}
