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
        /// Remove connection of a doctor on server
        /// </summary>
        /// <param name="doctorId"></param>
        /// <param name="labId"></param>
        /// <param name="connectionCode"></param>
        /// <returns></returns>
        bool RemoveDoctorConnect(int? serverDoctorId,int clientDoctor, int labId, string connectionCode);

        /// <summary>
        /// Call when a exmaination created . Client need to submit examination value to server
        /// and set UpdatedOnServer flag in LabExamination to true
        /// </summary>
        void SubmitExaminationToServer(int labId,int labExaminationID,int patientId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientDoctorId"></param>
        /// <param name="labId"></param>
        /// <param name="connectionCode"></param>
        /// <returns></returns>
        bool SubmitConnectionCodeToServer(int clientDoctorId, int labId, string connectionCode);

        /// <summary>
        /// Periodically update which need to submit to server ex: Exmaination , ConnectionCode ...
        /// </summary>
        void UpdateToServer(int labId);

    }
}
