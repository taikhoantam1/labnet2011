using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Collections.Specialized;

namespace DataRepository
{
    public class ClientConnector : IClientConnector
    {
        public string SetupConnectionWithLab(string connectionCode, int serverDoctorId, int clientDoctorId, string clientUrl,string doctorName)
        {
            //local test url
            clientUrl = "http://localhost:14587";
            string URI = clientUrl + "/Service/SetupDoctorConnection";
            WebClient wc = new WebClient();
            string myParamters = string.Format("ConnectionCode={0}&ServerDoctorId={1}&ClientDoctorId={2}&DoctorConnectName={3}", connectionCode, serverDoctorId, clientDoctorId, doctorName);
            wc.Headers["Content-type"] = "application/x-www-form-urlencoded";
            string HtmlResult = wc.UploadString(URI,  myParamters);
            return HtmlResult.ToString();
        }

        public string SetupLabConnectionWithLab(string connectionCode, int serverLabId, int clientLabId, string clientUrl, string labName)
        {
            //local test url
            clientUrl = "http://localhost:14587";
            string URI = clientUrl + "/Service/SetupLabConnection";
            WebClient wc = new WebClient();
            string myParamters = string.Format("ConnectionCode={0}&ServerLabId={1}&ClientLabId={2}&LabConnectName={3}", connectionCode, serverLabId, clientLabId, labName);
            wc.Headers["Content-type"] = "application/x-www-form-urlencoded";
            string HtmlResult = wc.UploadString(URI,  myParamters);
            return HtmlResult.ToString();
        }
    }
}
