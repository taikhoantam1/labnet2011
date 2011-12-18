using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;

namespace LabnetClient.App_Code
{
    public class AppHelper
    {

        public static void WriteErrorLog(Exception exception)
        {
            ILog logger = log4net.LogManager.GetLogger("ErrorLogger");
            logger.Error(exception.Message, exception);
        }

        public static int GetLoginUserId()
        {
            return 1;//Current default staffId=1 , will change to LoginUser.Id after implement UserManagement module
        }
    }
}