using System;
using System.Collections.Generic;
using System.Data;
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
                return ex.Message;
            }


        }

        [HttpPost]
        public string RemoveConnection(int? ServerDoctorId, int ClientDoctorId, int LabId, string ConnectionCode)
        {
            try
            {
                Repository.RemoveDoctorConnect(ServerDoctorId, ClientDoctorId, LabId, ConnectionCode);
                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpPost]
        public string AddNewLabMapping(string ConnectionCode, int LabId, int ClientLabId)
        {
            try
            {
                Repository.LabConnectMappingInsert(ConnectionCode, LabId, ClientLabId, (int)ConnectionStateEnum.Available);
                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpPost]
        public string RemoveLabConnection(int? ServerLabId, int ClientLabId, int LabId, string ConnectionCode)
        {
            try
            {
                Repository.RemoveLabConnect(ServerLabId, ClientLabId, LabId, ConnectionCode);
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
            // Kiểm tra xem có dòng nào với ExaminationNumber tồn tại chưa
            Examination examination = Repository.GetExamination(ExaminationNumber);
            if (examination == null)
            {
                try
                {
                    //Create new record in Examination
                    Repository.ExaminationInsert(ExaminationNumber, LabId, Status, PatientName, Phone, BirthDay, ClientPartnerId, ClientDoctorId);
                    //Update amount of lab
                    Repository.UpdateLabAmount(LabId);
                    return "Success";
                }
                catch(Exception ex)
                {
                    return ex.Message;
                }

            }
            return "Can't create Examination because existed an Examination with the same ExaminationNumber";
        }
        [HttpPost]
        public string UpdateExamination(string ExaminationNumber, int LabId, int Status, string PatientName, string Phone, string BirthDay, int? ClientPartnerId, int? ClientDoctorId)
        {
            try
            {
                //Update  record in Examination
                Repository.ExaminationUpdate(ExaminationNumber, LabId, Status, PatientName, Phone, BirthDay, ClientPartnerId, ClientDoctorId);
            }
            catch (ObjectNotFoundException ex)
            {
                return ex.Message;
            }

            return "Success";
        }

        [HttpPost]
        public string UpdateExaminationStatus(string examinationNumber, int status)
        {
            try
            {
                Repository.UpdateExaminationStatus(examinationNumber, status);
                return "Success";
            }
            catch (ObjectNotFoundException ex)
            {
                return ex.Message;
            }

        }
        #endregion
    }
}
