using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel;

namespace DataRepository
{
    public interface IServerConnector
    {
        /// <summary>
        /// Insert new record of Examination on server
        /// </summary>
        /// <param name="labId"></param>
        /// <param name="examinationNumber"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        string InsertExaminationOnLabServer(int labId, string examinationNumber, int status, string patientName, string phone, string age, int? partnerId, int? doctorId);
        /// <summary>
        /// Get unique examination number on server
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        string GetUniqueExaminationNumber(int length);

        /// <summary>
        /// Get unique patient number on server
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        string GetUniquePatientNumber(int length);
        
        /// <summary>
        /// Get unique connection code on server
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        string GetUniqueConnectionCode(int length, int labId, int doctorId);

        /// <summary>
        /// Remove connection of a doctor on server
        /// </summary>
        /// <param name="doctorId"></param>
        /// <param name="labId"></param>
        /// <param name="connectionCode"></param>
        /// <returns></returns>
        bool RemoveDoctorConnect(int serverDoctorId,int clientDoctor, int labId, string connectionCode);

        /// <summary>
        /// Call when a exmaination created . Client need to submit examination value to server
        /// and set UpdatedOnServer flag in LabExamination to true
        /// </summary>
        void SubmitExaminationToServer(int labId,int labExaminationID,int patientId);
        
        /// <summary>
        /// Periodically update which need to submit to server ex: Exmaination , ConnectionCode ...
        /// </summary>
        void UpdateToServer();
    }
}
