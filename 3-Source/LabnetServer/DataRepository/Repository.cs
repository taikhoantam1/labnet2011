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
        #region Examination
        public Examination GetExamination(string examinatioNumber)
        {
            return myDb.Examinations.Where(p => p.ExaminationNumber == examinatioNumber).FirstOrDefault();
        }

        public void ExaminationInsert(string examinationNumber, int labId, int status, string patientName, string phone, string birthDay, int? clientPartnerId, int? clientDoctorId)
        {
            Examination ex = new Examination
            {
                LabId = labId,
                ExaminationNumber = examinationNumber,
                Status = status,
                PatientName = patientName,
                Phone = phone,
                ClientDoctorId = clientDoctorId,
                ClientPartnerId = clientPartnerId,
                BirthDay = birthDay,
                CreatedDate = DateTime.Now
            };
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
        #endregion

        #region Service

        public void RemoveDoctorConnect(int? serverDoctorId, int clientDoctorId, int labId, string connectionCode)
        {
            DoctorConnectMapping doctorConnectMapping = myDb.DoctorConnectMappings.Where(p => p.ClientDoctorId == clientDoctorId
                                                                                        && p.LabId == labId
                                                                                        && p.ConnectionState == (int)ConnectionStateEnum.Connected)
                                                                                        .FirstOrDefault();
            if (doctorConnectMapping != null)
            {
                doctorConnectMapping.ConnectionState = (int)ConnectionStateEnum.ConnectionRemoveByLab;
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
            var result = myDb.DoctorConnectMappings.Where(p => p.DoctorId == doctorId && p.ConnectionState == (int) ConnectionStateEnum.Connected)
                                    .Select(p => new VMDoctorConnectMapping
                                    {
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

        public void UpdateMappingForDoctorConnect(int mappingId, int doctorId)
        {
            DoctorConnectMapping mapping = myDb.DoctorConnectMappings.Where(p => p.Id == mappingId).FirstOrDefault();
            mapping.DoctorId = doctorId;
            mapping.DateConnected = DateTime.Now;
            mapping.ConnectionState = (int)ConnectionStateEnum.Connected;
            myDb.SaveChanges();
        }

        public void DoctorConnectMappingInsert(string connectionCode, int labId, int clientDoctoId, int connectionState)
        {
            //Kiem tra chua ton tai connection code thi moi insert
            var listOldMapping = myDb.DoctorConnectMappings.Where(p => p.LabId == labId && p.ClientDoctorId == clientDoctoId).ToList();
            if (listOldMapping != null && listOldMapping.Count!=0)
            {
                listOldMapping.ForEach(p=>p.ConnectionState = (int)ConnectionStateEnum.ConnectionRemoveByLab);
            }
            //Insert new row
            DoctorConnectMapping mapping = new DoctorConnectMapping();
            mapping.LabId = labId;
            mapping.ClientDoctorId = clientDoctoId;
            mapping.ConnectionCode = connectionCode;
            mapping.ConnectionState = connectionState;
            myDb.DoctorConnectMappings.AddObject(mapping);
            myDb.SaveChanges();
        }

        private int GetPatientInDay(int doctorId, int labId)
        {
            DoctorConnectMapping mapping = myDb.DoctorConnectMappings.Where(p => p.DoctorId == doctorId && p.LabId == labId).FirstOrDefault();
            return myDb.Examinations.Where(p => p.LabId == labId && p.CreatedDate >= DateTime.Today
                                    && p.ClientDoctorId.HasValue
                                    && p.ClientDoctorId == mapping.ClientDoctorId).Count();
        }

        private int GetPatientInMonth(int doctorId, int labId)
        {
            DateTime firstDateOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DoctorConnectMapping mapping = myDb.DoctorConnectMappings.Where(p => p.DoctorId == doctorId && p.LabId == labId).FirstOrDefault();

            return myDb.Examinations.Where(p => p.LabId == labId && p.CreatedDate > firstDateOfMonth
                                    && p.ClientDoctorId.HasValue
                                    && p.ClientDoctorId == mapping.ClientDoctorId).Count();
        }
        #endregion

        #region Doctor
        public bool IsDoctorConnectWithLab(int currentDoctorId)
        {
            return myDb.DoctorConnectMappings.Any(p => p.DoctorId == currentDoctorId && p.ConnectionState == (int)ConnectionStateEnum.Connected);
        }

        public Doctor GetDoctor(int doctorId)
        {
            return myDb.Doctors.Where(p => p.DoctorId == doctorId).FirstOrDefault();
        }

        public bool CheckDoctorAccount(string UserName)
        {
            Doctor lstDoctor = myDb.Doctors.Where(p => p.UserName == UserName).FirstOrDefault();
            if (null != lstDoctor)
                return true;
            return false;
        }

        public Doctor GetDoctorByUserName(string UserName)
        {
            return myDb.Doctors.Where(p => p.UserName == UserName).FirstOrDefault();
        }

        public void DoctorInsert(string Name, string UserName, string Password, string Address, string PhoneNumber, string Email)
        {
            Doctor doctor = new Doctor
            {
                Name = Name,
                UserName = UserName,
                Password = Password,
                Address = Address,
                PhoneNumber = PhoneNumber,
                Email = Email
            };
            myDb.Doctors.AddObject(doctor);
            myDb.SaveChanges();
        }

        #endregion
    }
}
