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
        public string GetConnectionCode(int length,int LabId, int ClientDoctorId)
        {
            try
            {
                string connectionCode = Repository.GenerateConnectionCode(length);
                Repository.DoctorConnectMappingInsert(connectionCode, LabId, ClientDoctorId, (int)ConnectionStateEnum.Available);
                return connectionCode;
            }
            catch (Exception ex)
            {
                return "Error";
            }
            

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
