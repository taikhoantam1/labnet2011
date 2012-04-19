using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using DomainModel;

namespace DataRepository
{
    public class ServerConnector : IServerConnector
    {
        private LabManager_ClientEntities myDb;
        private string severUrl = "http://labnet.vn";
        private string localUrl = "http://localhost:2821";

        public ServerConnector()
        {
            myDb = new LabManager_ClientEntities();
        }

        public string ServerUrl
        {
            get { return localUrl; }
        }

        public string InsertExaminationOnLabServer(int labId, string examinationNumber,int status,string patientName, string phone,string age, int? partnerId, int? doctorId)
        {
            string URI = ServerUrl + "/Examination/InsertExamination";
            string myParamters = string.Format("LabId={0}&ExaminationNumber={1}&Status={2}&PatientName={3}&Phone={4}&BirthDay={5}&ClientPartnerID={6}&ClientDoctorId={7}",
                                                labId,
                                                examinationNumber,
                                                status,
                                                patientName,
                                                phone,
                                                age,
                                                partnerId,
                                                doctorId);
            WebClient wc = new WebClient();
            wc.Headers["Content-type"] = "application/x-www-form-urlencoded";
            string HtmlResult = wc.UploadString(URI, myParamters);
            return HtmlResult;
        }

        public string GetUniqueExaminationNumber(int length)
        {
            string URI = ServerUrl + "/Service/GetExaminationNumber";
            WebClient wc = new WebClient();
            string myParamters = string.Format("Length={0}", length);
            wc.Headers["Content-type"] = "application/x-www-form-urlencoded";
            string HtmlResult = wc.UploadString(URI, myParamters);
            return HtmlResult;
        }

        public string GetUniquePatientNumber(int length)
        {
            string URI = ServerUrl + "/Service/GetPatientNumber";
            WebClient wc = new WebClient();
            string myParamters = string.Format("Length={0}", length);
            wc.Headers["Content-type"] = "application/x-www-form-urlencoded";
            string HtmlResult = wc.UploadString(URI, myParamters);
            return HtmlResult;
        }

        public string GetUniqueConnectionCode(int length,int labId,int doctorId)
        {
            string URI = ServerUrl + "/Service/GetConnectionCode";
            WebClient wc = new WebClient();
            string myParamters = string.Format("Length={0}&LabId={1}&ClientDoctorId={2}", length,labId,doctorId);
            wc.Headers["Content-type"] = "application/x-www-form-urlencoded";
            string HtmlResult = wc.UploadString(URI, myParamters);
            return HtmlResult;
        }

        public bool RemoveDoctorConnect(int serverDoctorId, int clientDoctorId,int labId, string connectionCode)
        {
            string URI = ServerUrl + "/Service/RemoveConnection";
            WebClient wc = new WebClient();
            string myParamters = string.Format("ServerDoctorId={0}&LabId={1}&ConnectionCode={2}&ClientDoctorId={3}",serverDoctorId,clientDoctorId,labId,connectionCode);
            wc.Headers["Content-type"] = "application/x-www-form-urlencoded";
            string HtmlResult = wc.UploadString(URI, myParamters);
            return HtmlResult=="Success"?true:false;
        }


        /// <summary>
        /// Call when a exmaination created . Client need to submit examination value to server
        /// and set UpdatedOnServer flag in LabExamination to true
        /// </summary>
        public void SubmitExaminationToServer(int labId, int labExaminationID, int patientId)
        {
 
        }

        /// <summary>
        /// Periodically update which need to submit to server ex: Exmaination , ConnectionCode ...
        /// </summary>
        public void UpdateToServer()
        {
            var listExamNotUpdate = myDb.LabExaminations.Where(p => !p.UpdatedOnServer.Value);
            foreach (var exam in listExamNotUpdate)
            {
                
            }
        }
    }
}
