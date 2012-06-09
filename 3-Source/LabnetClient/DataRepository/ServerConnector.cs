using System;
using System.Linq;
using System.Net;

namespace DataRepository
{
    public class ServerConnector : IServerConnector
    {
        private readonly LabManager_ClientEntities _myDb;
        private string severUrl = "http://labnet.vn";
        private const string LocalUrl = "http://localhost:2821";

        public ServerConnector()
        {
            _myDb = new LabManager_ClientEntities();
        }

        public string ServerUrl
        {
            //get { return LocalUrl; }
            get { return severUrl; }
        }

        public bool InsertExaminationOnLabServer(int labId, string examinationNumber, int status, string patientName, string phone, string age, int? partnerId, int? doctorId)
        {
            string uri = ServerUrl + "/Service/InsertExamination";
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
            string htmlResult = string.Empty;
            try
            {
                htmlResult = wc.UploadString(uri, myParamters);
            }
            catch
            {
                // Log exception
                return false;
            }
            return htmlResult == "Success";
        }

        public bool RemoveDoctorConnect(int? serverDoctorId, int clientDoctorId, int labId, string connectionCode)
        {
            string uri = ServerUrl + "/Service/RemoveConnection";
            WebClient wc = new WebClient();
            string myParamters = string.Format("ServerDoctorId={0}&LabId={1}&ConnectionCode={2}&ClientDoctorId={3}", serverDoctorId, labId, connectionCode, clientDoctorId);
            wc.Headers["Content-type"] = "application/x-www-form-urlencoded";
            string htmlResult = wc.UploadString(uri, myParamters);
            return htmlResult == "Success";
        }

        public bool SubmitConnectionCodeToServer(int clientDoctorId, int labId, string connectionCode)
        {

            string uri = ServerUrl + "/Service/AddNewDoctorMapping";
            WebClient wc = new WebClient();
            string myParamters = string.Format("ClientDoctorId={0}&LabId={1}&ConnectionCode={2}", clientDoctorId, labId, connectionCode);
            wc.Headers["Content-type"] = "application/x-www-form-urlencoded";
            string htmlResult = wc.UploadString(uri, myParamters);
            return htmlResult == "Success";
        }

        public bool SubmitLabConnectionCodeToServer(int clientLabId, int labId, string connectionCode)
        {

            string uri = ServerUrl + "/Service/AddNewLabMapping";
            WebClient wc = new WebClient();
            string myParamters = string.Format("ClientLabId={0}&LabId={1}&ConnectionCode={2}", clientLabId, labId, connectionCode);
            wc.Headers["Content-type"] = "application/x-www-form-urlencoded";
            string htmlResult = wc.UploadString(uri, myParamters);
            return htmlResult == "Success";
        }

        /// <summary>
        /// Call when a exmaination created . Client need to submit examination value to server
        /// and set UpdatedOnServer flag in LabExamination to true
        /// </summary>
        public void SubmitExaminationToServer(int labId, int labExaminationID, int patientId)
        {
            LabExamination labExamination = _myDb.LabExaminations.FirstOrDefault(p => p.Id == labExaminationID);
            Patient patient = _myDb.Patients.FirstOrDefault(p => p.Id == patientId);
            if (labExamination != null && patient != null)
            {
                bool result = InsertExaminationOnLabServer(labId,
                                                            labExamination.ExaminationNumber,
                                                            labExamination.Status,
                                                            patient.FirstName,
                                                            patient.Phone,
                                                            patient.Age,
                                                            labExamination.PartnerId,
                                                            labExamination.DoctorId);
                if (result)
                {
                    labExamination.UpdatedOnServer = true;
                }
                else
                {
                    labExamination.UpdatedOnServer = false;
                }
                _myDb.SaveChanges();
            }
        }

        /// <summary>
        /// Periodically update which need to submit to server ex: Exmaination , ConnectionCode ...
        /// </summary>
        public void UpdateToServer(int labId)
        {
            //Examination
            var listExamNotUpdate = _myDb.LabExaminations.Where(p => !p.UpdatedOnServer.HasValue || !p.UpdatedOnServer.Value);
            foreach (var exam in listExamNotUpdate)
            {
                Patient patient = _myDb.Patients.FirstOrDefault(p => p.Id == exam.PatientId);
                if (exam != null && patient != null)
                {
                    bool result = InsertExaminationOnLabServer(labId,
                                                                exam.ExaminationNumber,
                                                                exam.Status,
                                                                patient.FirstName,
                                                                patient.Phone,
                                                                patient.Age,
                                                                exam.PartnerId,
                                                                exam.DoctorId);
                    if (result)
                    {
                        exam.UpdatedOnServer = true;
                    }
                }

            }

            _myDb.SaveChanges();

            //DoctorMapping
            var listDoctorNotUpdate = _myDb.Doctors.Where(p => !p.UpdatedOnServer.HasValue || !p.UpdatedOnServer.Value);
            foreach (var doctor in listDoctorNotUpdate)
            {
                bool result = false;
                if (!string.IsNullOrEmpty(doctor.ConnectionCode))
                {
                    // Set up new connection but out of update to server

                    result = SubmitConnectionCodeToServer(doctor.Id, labId, doctor.ConnectionCode);
                    if (result)
                        doctor.UpdatedOnServer = true;
                }
                else
                {
                    //  remove connection but out of update to server
                    result = RemoveDoctorConnect(null, doctor.Id, labId, string.Empty);
                    if (result)
                        doctor.UpdatedOnServer = true;
                }
            }
            _myDb.SaveChanges();

            //LabMapping
            var listPartnerNotUpdate = _myDb.Partners.Where(p => !p.UpdatedOnServer.HasValue || !p.UpdatedOnServer.Value);
            foreach (var partner in listPartnerNotUpdate)
            {
                bool result = false;
                if (!string.IsNullOrEmpty(partner.ConnectionCode))
                {
                    // Set up new connection but out of update to server

                     result = SubmitLabConnectionCodeToServer(partner.Id, labId, partner.ConnectionCode);
                    if (result)
                        partner.UpdatedOnServer = true;
                }
                else
                {
                    //  remove connection but out of update to server
                   result= RemoveLabConnect(null, partner.Id, labId, string.Empty);
                    if (result)
                        partner.UpdatedOnServer = true;
                }
            }
            _myDb.SaveChanges();
        }

        public bool RemoveLabConnect(int? serverLabId, int clientLabId, int labId, string connectionCode)
        {
            string uri = ServerUrl + "/Service/RemoveLabConnection";
            WebClient wc = new WebClient();
            string myParamters = string.Format("ServerLabId={0}&LabId={1}&ConnectionCode={2}&ClientLabId={3}", serverLabId, labId, connectionCode, clientLabId);
            wc.Headers["Content-type"] = "application/x-www-form-urlencoded";
            string htmlResult = wc.UploadString(uri, myParamters);
            return htmlResult == "Success";
        }

        public bool UpdateExaminationStatus(string examinationNumber, int status)
        {
            string uri = ServerUrl + "/Service/UpdateExaminationStatus";
            WebClient wc = new WebClient();
            string myParamters = string.Format("ExaminationNumber={0}&Status={1}", examinationNumber, status);
            wc.Headers["Content-type"] = "application/x-www-form-urlencoded";
            string htmlResult = wc.UploadString(uri, myParamters);
            return htmlResult == "Success";
        }

        public bool UpdateExamination(int labId, string examinationNumber, int status, string patientName, string phone, string age, int? partnerId, int? doctorId)
        {
            string uri = ServerUrl + "/Service/UpdateExamination";
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
            string htmlResult = string.Empty;
            try
            {
                htmlResult = wc.UploadString(uri, myParamters);
            }
            catch
            {
                // Log exception
                return false;
            }
            return htmlResult == "Success";
        }
    }
}
