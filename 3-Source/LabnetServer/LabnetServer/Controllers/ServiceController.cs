using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataRepository;
using DomainModel;

namespace LabnetServer.Controllers
{
    public class ServiceController : BaseController
    {
        //
        // GET: /Service/
        

        [HttpPost]
        public string AddNewDoctorMapping(string ConnectionCode, int LabId, int ClientDoctorId)
        {
            try
            {
                Repository.DoctorConnectMappingInsert(ConnectionCode, LabId, ClientDoctorId, (int)ConnectionStateEnum.Available);
                return "Success";
            }
            catch (Exception ex)
            {
                return "Error";
            }
            

        }

        [HttpPost]
        public string RemoveConnection(int? ServerDoctorId,int ClientDoctorId,int LabId, string ConnectionCode)
        {
            try
            {
                Repository.RemoveDoctorConnect(ServerDoctorId,ClientDoctorId, LabId, ConnectionCode);
                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #region Examination
            [HttpPost]
            public string InsertExamination(string ExaminationNumber, int LabId, int Status, string PatientName, string Phone, string BirthDay, int? ClientPartnerId, int? ClientDoctorId)
            {
                // Kiểm tra xem có dòng nào với ExaminationNumber và LabId tồn tại chưa
                Examination examination = Repository.GetExamination(ExaminationNumber);
                if (examination == null)
                {
                    //Create new record in Examination
                    Repository.ExaminationInsert(ExaminationNumber, LabId, Status,PatientName,Phone,BirthDay,ClientPartnerId,ClientDoctorId);
                    //Update amount of lab
                    Repository.UpdateLabAmount(LabId);
                }
                return "Success";
            }

            public string UpdateExmaination(string examinationNumber, int labId, int status)
            {
                //Update status of Examination
                return "Success";
            }
        #endregion
    }
}
