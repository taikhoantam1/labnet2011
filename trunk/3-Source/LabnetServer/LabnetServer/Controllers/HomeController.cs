﻿using System;
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
            return View();
        }

        public ActionResult Login()
        {
            return PartialView();
        }

        [HttpPost]
        public string Login(string UserName, string Password)
        {
            LabnetAccount account = Repository.GetAccount(UserName);
            if (account != null)
            {
                if (account.Password == Password)
                {
                    return new { Url = account.Domain, LabId = account.LabClient.LabId, Message = "Success" }.ToJson();
                }
                else
                {
                    return new {Message = "Sai mật khẩu" }.ToJson(); ;
                }
            }
            return new { Message = "Không tồn tại tài khoản này"}.ToJson(); 
        }
    }
}
