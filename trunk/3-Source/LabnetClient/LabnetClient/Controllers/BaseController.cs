using System.Web.Mvc;
using DataRepository;
using LabnetClient.App_Code;
using LabnetClient.CustomAttribute;
using System.Configuration;

namespace LabnetClient.Controllers
{
    [SetCulture]
    public class BaseController : Controller
    {
        public IDataRepository Repository;
        public IServerConnector ServerConnector;
        public int LabId =int.Parse( ConfigurationManager.AppSettings["LabId"]);
        public BaseController()
        {
            Repository = new Repository();
            ServerConnector = new ServerConnector();
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            AppHelper.WriteErrorLog(filterContext.Exception);
            base.OnException(filterContext);
        }

    }
}
