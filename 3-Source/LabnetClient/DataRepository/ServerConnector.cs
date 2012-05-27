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

        public string InsertExaminationOnLabServer(int labId, string examinationNumber, int status, string patientName, string phone, string age, int? partnerId, int? doctorId)
        {
            string URI = ServerUrl + "/Service/InsertExamination";
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

        public bool RemoveDoctorConnect(int? serverDoctorId, int clientDoctorId, int labId, string connectionCode)
        {
            string URI = ServerUrl + "/Service/RemoveConnection";
            WebClient wc = new WebClient();
            string myParamters = string.Format("ServerDoctorId={0}&LabId={1}&ConnectionCode={2}&ClientDoctorId={3}", serverDoctorId, labId, connectionCode, clientDoctorId);
            wc.Headers["Content-type"] = "application/x-www-form-urlencoded";
            string HtmlResult = wc.UploadString(URI, myParamters);
            return HtmlResult == "Success" ? true : false;
        }

        public bool SubmitConnectionCodeToServer(int clientDoctorId, int labId, string connectionCode)
        {

            string URI = ServerUrl + "/Service/AddNewDoctorMapping";
            WebClient wc = new WebClient();
            string myParamters = string.Format("ClientDoctorId={0}&LabId={1}&ConnectionCode={2}", clientDoctorId, labId, connectionCode);
            wc.Headers["Content-type"] = "application/x-www-form-urlencoded";
            string HtmlResult = wc.UploadString(URI, myParamters);
            return HtmlResult == "Success" ? true : false;
        }

        public bool SubmitLabConnectionCodeToServer(int clientLabId, int labId, string connectionCode)
        {

            string URI = ServerUrl + "/Service/AddNewLabMapping";
            WebClient wc = new WebClient();
            string myParamters = string.Format("ClientLabId={0}&LabId={1}&ConnectionCode={2}", clientLabId, labId, connectionCode);
            wc.Headers["Content-type"] = "application/x-www-form-urlencoded";
            string HtmlResult = wc.UploadString(URI, myParamters);
            return HtmlResult == "Success" ? true : false;
        }

        /// <summary>
        /// Call when a exmaination created . Client need to submit examination value to server
        /// and set UpdatedOnServer flag in LabExamination to true
        /// </summary>
        public void SubmitExaminationToServer(int labId, int labExaminationID, int patientId)
        {
            LabExamination labExamination = myDb.LabExaminations.Where(p => p.Id == labExaminationID).FirstOrDefault();
            Patient patient = myDb.Patients.Where(p => p.Id == patientId).FirstOrDefault();
            if (labExamination != null && patient != null)
            {
                string result = InsertExaminationOnLabServer(labId,
                                                            labExamination.ExaminationNumber,
                                                            labExamination.Status,
                                                            patient.FirstName,
                                                            patient.Phone,
                                                            patient.Age,
                                                            labExamination.PartnerId,
                                                            labExamination.DoctorId);
                if (result == "Success")
                {
                    labExamination.UpdatedOnServer = true;
                    myDb.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Periodically update which need to submit to server ex: Exmaination , ConnectionCode ...
        /// </summary>
        public void UpdateToServer(int labId)
        {
            //Examination
            var listExamNotUpdate = myDb.LabExaminations.Where(p => !p.UpdatedOnServer.Value);
            foreach (var exam in listExamNotUpdate)
            {
                Patient patient = myDb.Patients.Where(p => p.Id == exam.PatientId).FirstOrDefault();
                if (exam != null && patient != null)
                {
                    string result = InsertExaminationOnLabServer(labId,
                                                                exam.ExaminationNumber,
                                                                exam.Status,
                                                                patient.FirstName,
                                                                patient.Phone,
                                                                patient.Age,
                                                                exam.PartnerId,
                                                                exam.DoctorId);
                    if (result == "Success")
                    {
                        exam.UpdatedOnServer = true;
                    }
                }

            }

            myDb.SaveChanges();
            //DoctorMapping
            var listDoctorNotUpdate = myDb.Doctors.Where(p => !p.UpdatedOnServer.Value);
            foreach (var doctor in listDoctorNotUpdate)
            {
                if (!string.IsNullOrEmpty(doctor.ConnectionCode))
                {
                    // Set up new connection but out of update to server

                    bool result = SubmitConnectionCodeToServer(doctor.Id, labId, doctor.ConnectionCode);
                    if (result)
                        doctor.UpdatedOnServer = true;
                }
                else
                {
                    //  remove connection but out of update to server
                    RemoveDoctorConnect(null, doctor.Id, labId, string.Empty);
                }
            }
            myDb.SaveChanges();

            //LabMapping
        }
    }
}
