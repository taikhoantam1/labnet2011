using System;
using System.Collections.Generic;
using DomainModel;

namespace DataRepository
{
    public interface IRepository
    {
        #region LabAccount

        LabnetAccount GetAccount(string userName);
        int LabAccountInsert(string userName, string password, int labId, string domainUrl);
        LabnetAccount GetLabAccountByUserName(string userName);
        #endregion

        #region Examination

        void ExaminationInsert(string examinationNumber, int labId, int status, string patientName, string phone, string birthDay, int? clientPartnerId, int? clientDoctorId);
        Examination GetExamination(string examinatioNumber);
        List<VMExamination> GetExaminations(DateTime dateTime, int? labId);
        #endregion

        #region LabClient

        void UpdateLabAmount(int labId);
        List<LabClient> GetLabClients();
        List<LabClient> GetConnectedLab(int doctorId);
        int LabClientInsert(string name, string url, string address, string phone, int type);
        #endregion

        #region DoctorConnectMapping

        DoctorConnectMapping GetDoctorConnectMapping(string connectionCode);
        List<VMDoctorConnectMapping> GetDoctorConnectMappings(int doctorId);
        void DoctorConnectMappingInsert(string connectionCode, int labId, int clientDoctoId, int connectionState);
        void UpdateMappingForDoctorConnect(int mappingId, int doctorId);
        #endregion

        #region Service

        void RemoveDoctorConnect(int? serverDoctorId, int clientDoctorId, int labId, string connectionCode);
        void RemoveLabConnect(int? serverLabId, int clientLabId, int labId, string connectionCode);

        #endregion

        #region Doctor

        bool IsDoctorConnectWithLab(int currentDoctorId);
        Doctor GetDoctor(int doctorId);
        bool CheckDoctorAccount(string userName);
        Doctor GetDoctorByUserName(string userName);
        void DoctorInsert(string name, string userName, string password, string address, string phoneNumber, string email);
        Doctor DoctorChangePassword(int doctorId, string newPass);
        void DoctorUpdate(int id, Doctor doctor);
        #endregion

        void LabConnectMappingInsert(string connectionCode, int labId, int clientLabId, int connectionState);

        LabConnectMapping GetLabConnectMapping(string connectionCode);
        bool CheckLabAccount(string userName);
        void UpdateMappingForLabConnect(int mappingId, int labId);
        List<LabClient> GetConnectedLabByLab(int? labId);
        List<VMLabConnectMapping> GetLabConnectMappings(int? labId);
        bool IsLabConnectWithLab(int? currentLabId);
        List<VMLabExamination> GetLabExaminations(DateTime dateTime, int? labId);
        void LabClientUpdate(int id, LabClient lab);
        LabnetAccount LabChangePassword(int labId, string newPass);
        void UpdateExaminationStatus(string examinationNumber, int status);
        void ExaminationUpdate(string examinationNumber, int labId, int status, string patientName, string phone, string birthDay, int? clientPartnerId, int? clientDoctorId);
    }
}
