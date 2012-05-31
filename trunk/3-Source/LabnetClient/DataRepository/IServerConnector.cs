
namespace DataRepository
{
    public interface IServerConnector
    {
        /// <summary>
        /// Remove connection of a doctor on server
        /// </summary>
        /// <param name="serverDoctorId"> </param>
        /// <param name="clientDoctor"></param>
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

        bool SubmitLabConnectionCodeToServer(int clientLabId, int labId, string connectionCode);

        bool RemoveLabConnect(int? serverLabId, int clientLabId, int labId, string connectionCode);

        /// <summary>
        /// Update status of an examination on server
        /// </summary>
        /// <param name="examinationNumber">examination numver</param>
        /// <param name="status">status of examination</param>
        bool UpdateExaminationStatus(string examinationNumber, int status);

        /// <summary>
        /// Update examination on server when client updated examination
        /// </summary>
        /// <param name="labId"></param>
        /// <param name="examinationNumber"></param>
        /// <param name="status"></param>
        /// <param name="patientName"></param>
        /// <param name="phone"></param>
        /// <param name="age"></param>
        /// <param name="partnerId"></param>
        /// <param name="doctorId"></param>
        /// <returns></returns>
        bool UpdateExamination(int labId, string examinationNumber, int status, string patientName, string phone, string age, int? partnerId, int? doctorId);
    }
}
