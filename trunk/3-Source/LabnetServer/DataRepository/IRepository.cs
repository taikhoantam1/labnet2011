using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel;

namespace DataRepository
{
    public interface IRepository
    {
        #region LabAccount
        
            LabnetAccount GetAccount(string UserName);
        
        #endregion
        
        #region Examination

            void ExaminationInsert(string ExaminationNumber, int LabId, int Status, string PatientName, string Phone, string BirthDay, int? ClientPartnerId, int? ClientDoctorId);
            Examination GetExamination(string examinatioNumber);
            List<VMExamination> GetExaminations(DateTime dateTime, int? labId);
        #endregion
        
        #region LabClient
        
            void UpdateLabAmount(int labId);
            List<LabClient> GetLabClients();
        #endregion
        
        #region DoctorConnectMapping

            DoctorConnectMapping GetDoctorConnectMapping(string ConnectionCode);
            List<VMDoctorConnectMapping> GetDoctorConnectMappings(int doctorId);
            void DoctorConnectMappingInsert(string connectionCode, int labId, int clientDoctoId,int connectionState);
            void UpdateMappingForDoctorConnect(int mappingId, int doctorId);
        #endregion
        
        #region Service
        
            void RemoveDoctorConnect(int? serverDoctorId,int clientDoctorId, int labId, string connectionCode );
        
        #endregion

        #region Doctor

            bool IsDoctorConnectWithLab(int currentDoctorId);
            Doctor GetDoctor(int doctorId);
            bool CheckDoctorAccount(string UserName);
            Doctor GetDoctorByUserName(string UserName);
            void DoctorInsert(string Name, string UserName, string Password, string Address, string PhoneNumber, string Email);
        #endregion





            
    }
}
