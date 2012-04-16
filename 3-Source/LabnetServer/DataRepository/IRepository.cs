﻿using System;
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
        
            void ExaminationInsert(string examinationNumber, int labId, int status);
            Examination GetExamination(string examinatioNumber);
        
        #endregion
        
        #region LabClient
        
            void UpdateLabAmount(int labId);
        
        #endregion
        
        #region DoctorConnectMapping

            DoctorConnectMapping GetDoctorConnectMapping(string ConnectionCode);
            List<VMDoctorConnectMapping> GetDoctorConnectMappings(int doctorId);
        #endregion
        
        #region Service
        
            string GenerateExaminationNumber(int length);
            string GenerateConnectionCode(int length);
            void RemoveDoctorConnect(int serverDoctorId,int clientDoctorId, int labId, string connectionCode,int connectionState);
        
        #endregion



            
    }
}