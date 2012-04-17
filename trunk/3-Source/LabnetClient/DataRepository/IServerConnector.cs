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
        string InsertExaminationOnLabServer(int labId, LabExamination labExamination, VMPatient patient);
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
    }
}
