﻿using System.Web.Mvc;
using DataRepository;
using LabnetServer.CustomAttribute;

namespace LabnetServer.Controllers
{
    [SetCulture]
    public class BaseController : Controller
    {
        public IRepository Repository;
        public BaseController()
        {
            Repository = new Repository();
        }

        protected override void OnException(ExceptionContext filterContext)
        {
          
            base.OnException(filterContext);
        }

    }
}
