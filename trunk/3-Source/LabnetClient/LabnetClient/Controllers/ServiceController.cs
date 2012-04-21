using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace LabnetClient.Controllers
{
    public class ServiceController : BaseController
    {
        public string SetupDoctorConnection(string ConnectionCode, int ServerDoctorId, int ClientDoctorId, string DoctorConnectName)
        {
            // Create a UTF-8 encoding.
            UTF8Encoding utf8 = new UTF8Encoding();
            try
            {
                string message = Repository.SetupDoctorConnection(ConnectionCode, ServerDoctorId, ClientDoctorId, DoctorConnectName);
                return message;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

    }
}
