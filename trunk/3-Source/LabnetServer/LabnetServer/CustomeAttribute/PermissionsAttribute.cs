using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainModel;

namespace LabnetServer
{
    public class PermissionsAttribute : ActionFilterAttribute
    {
        private readonly string role;

        public PermissionsAttribute(string role)
        {
            this.role = role;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var user = filterContext.HttpContext.Session[role];
            if (user == null)
            {
                //send them off to the login page
                var homeUrl = Constant.DomainUrl;
                filterContext.HttpContext.Response.Redirect(homeUrl, true);
            }
        }
    }
}