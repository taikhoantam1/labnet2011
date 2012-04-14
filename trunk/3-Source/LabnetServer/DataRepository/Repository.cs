using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel;

namespace DataRepository
{
    public class Repository : IRepository
    {
        private LabnetServerEntities myDb;
        public LabnetServerEntities Context { get { return myDb; } }

        /// <summary>
        /// 
        /// </summary>
        public Repository()
        {
            myDb = new LabnetServerEntities();
        }

        public LabnetAccount GetAccount(string UserName)
        {
            LabnetAccount account = Context.LabnetAccounts.Where(p => p.UserName == UserName).FirstOrDefault();
            return account;
        }
        public Examination GetExamination(string examinatioNumber)
        {
            return myDb.Examinations.Where(p => p.ExaminationNumber == examinatioNumber).FirstOrDefault();
        }

        public void ExaminationInsert(string examinationNumber, int labId, int status)
        {
            Examination ex = new Examination { LabId = labId, ExaminationNumber = examinationNumber, Status = status, CreatedDate = DateTime.Now };
            myDb.Examinations.AddObject(ex);
            myDb.SaveChanges();
        }

        public void UpdateLabAmount(int labId)
        {
            LabClient lab = myDb.LabClients.Where(p => p.LabId == labId).FirstOrDefault();
            if (lab != null)
            {
                lab.Amount--;
            }
            myDb.SaveChanges();
        }


        #region Service
        public string GenerateExaminationNumber(int length)
        {
            return myDb.GenerateExaminationNumber(length).FirstOrDefault();
        }

        public string GenerateConnectionCode(int length)
        {
            return myDb.GenerateConnectionCode(length).FirstOrDefault();
        }

        public void RemoveDoctorConnect(int serverDoctorId,int clientDoctorId, int labId, string connectionCode,int connectionState)
        {
            DoctorConnectMapping doctorConnectMapping = myDb.DoctorConnectMappings.Where(p => p.ClientDoctorId == clientDoctorId
                                                                                        && p.DoctorId == serverDoctorId
                                                                                        && p.LabId == labId
                                                                                        && p.ConnectionCode == connectionCode)
                                                                                        .FirstOrDefault();
            if (doctorConnectMapping != null)
            {
                doctorConnectMapping.ConnectionState = connectionState;
                myDb.SaveChanges();
            }
            else
            {
                throw new Exception("Không tìm thấy dử liệu liên kết hợp lệ");
            }
        }
        #endregion
        #region DoctorConnectMapping

        public DoctorConnectMapping GetDoctorConnectMapping(string ConnectionCode)
        {
            return myDb.DoctorConnectMappings.Where(p => p.ConnectionCode == ConnectionCode).FirstOrDefault();
        }

        public List<VMDoctorConnectMapping> GetDoctorConnectMappings(int doctorId)
        {
            var result = myDb.DoctorConnectMappings.Where(p => p.DoctorId == doctorId)
                                    .Select(p => new VMDoctorConnectMapping { 
                                    DateConnected = p.DateConnected,
                                    Address = p.LabClient.Address,
                                    PhoneNumber = p.LabClient.Phone,
                                    LabName = p.LabClient.Name,
                                    MappingId = p.Id,
                                    LabId = p.LabId
                                    }).ToList();
            foreach (var item in result)
            {
                int patientInMonth = GetPatientInMonth(doctorId, item.LabId);
                int patientInDay = GetPatientInDay(doctorId, item.LabId);
                item.NumberPatientInDay = patientInDay;
                item.NumberPatientInMonth = patientInMonth;
            }
            return result;
        }

        private int GetPatientInDay(int doctorId,int labId)
        {

            return myDb.Examinations.Where(p => p.LabId == labId && p.CreatedDate >= DateTime.Today).Count();
        }

        private int GetPatientInMonth(int doctorId, int labId)
        {
            DateTime firstDateOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            return myDb.Examinations.Where(p => p.LabId == labId && p.CreatedDate > firstDateOfMonth).Count();
        }
        #endregion
    }
}
