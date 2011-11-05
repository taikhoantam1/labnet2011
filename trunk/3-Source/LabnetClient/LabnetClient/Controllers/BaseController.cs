﻿using System.Web.Mvc;
using DataRepository;
using LabnetClient.App_Code;
using LabnetClient.CustomAttribute;

namespace LabnetClient.Controllers
{
    [SetCulture]
    public class BaseController : Controller
    {
        public IDataRepository Repository;
        public BaseController()
        {
            Repository = new Repository();
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            AppHelper.WriteErrorLog(filterContext.Exception);
            base.OnException(filterContext);
        }

    }
}