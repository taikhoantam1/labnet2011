using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryFuntion
{
    public class UrlFunction
    {
        public static String Action(String Controller, String Action, String Variable)
        {
            String Result="";
            
            return Result;
        }
        
        public static String FullHostName(HttpRequest request)
        {
            String Result = "";
            if (request.Url.Port != 80)
            {
                Result = request.Url.Host + ":" + request.Url.Port;
            }
            else
                Result = request.Url.Host;

            return Result;
        }
    }
}
