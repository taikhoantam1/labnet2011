using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LabnetServer.Controllers
{
    public class ServiceController : BaseController
    {
        //
        // GET: /Service/
        
        [HttpPost]
        public string GetExaminationNumber(int length)
        {
            return Repository.GenerateExaminationNumber(length);
        }

        [HttpPost]
        public string GetPatientNumber(int length)
        {
            return Repository.GenerateExaminationNumber(length);
        }

        [HttpPost]
        public string GetConnectionCode(int length)
        {
            return Repository.GenerateConnectionCode(length);
        }

        [HttpPost]
        public string RemoveConnection(int ServerDoctorId,int ClientDoctorId,int LabId, string ConnectionCode)
        {
            try
            {
                Repository.RemoveDoctorConnect(ServerDoctorId,ClientDoctorId, LabId, ConnectionCode, (int)ConnectionStateEnum.ConnectionRemoveByLab);
                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
