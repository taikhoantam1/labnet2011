using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel;
using System.Data.Objects;

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

        public int LabAccountInsert(string UserName, string Password, int LabId, string DomainUrl)
        {
            LabnetAccount labAccount = new LabnetAccount();

            labAccount.UserName = UserName;
            labAccount.Password = Password;
            labAccount.LabId = LabId;
            labAccount.Domain = DomainUrl;
            labAccount.Role = -1;

            myDb.LabnetAccounts.AddObject(labAccount);
            myDb.SaveChanges();

            return labAccount.UserId;
        }

        public LabnetAccount GetLabAccountByUserName(string UserName)
        {
            return myDb.LabnetAccounts.Where(p => p.UserName == UserName).FirstOrDefault();
        }
        #region Examination
        public Examination GetExamination(string examinatioNumber)
        {
            return myDb.Examinations.Where(p => p.ExaminationNumber == examinatioNumber).FirstOrDefault();
        }
        public List<VMExamination> GetExaminations(DateTime dateTime, int? labId)
        {
            var result = myDb.Examinations.Where(p => (EntityFunctions.TruncateTime(p.CreatedDate) == dateTime)
                                                        && (!labId.HasValue || labId.Value == p.LabId)).ToList();
            List<VMExamination> vmResult = result.Select(p => new VMExamination
            {
                BirthDay = p.BirthDay,
                CreatedDate = p.CreatedDate.ToString("d"),
                ExaminationId = p.LabId,
                ExaminationNumber = p.ExaminationNumber,
                PatientName = p.PatientName,
                Phone = p.Phone,
                Status = p.Status == 1 ? "Chưa có KQ" : "Có KQ"
            }).ToList();
            return vmResult;
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

        public void RemoveLabConnect(int? serverLabId, int clientLabId, int labId, string connectionCode)
        {
            LabConnectMapping labConnectMapping = myDb.LabConnectMappings.Where(p => p.ClientLabId == clientLabId
                                                                                        && p.LabId == labId
                                                                                        && p.ConnectionState == (int)ConnectionStateEnum.Connected)
                                                                                        .FirstOrDefault();
            if (labConnectMapping != null)
            {
                labConnectMapping.ConnectionState = (int)ConnectionStateEnum.ConnectionRemoveByLab;
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
            var result = myDb.DoctorConnectMappings.Where(p => p.DoctorId == doctorId && p.ConnectionState == (int)ConnectionStateEnum.Connected)
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
            if (listOldMapping != null && listOldMapping.Count != 0)
            {
                listOldMapping.ForEach(p => p.ConnectionState = (int)ConnectionStateEnum.ConnectionRemoveByLab);
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
            return myDb.Examinations.Where(p => p.LabId == labId && EntityFunctions.TruncateTime( p.CreatedDate) <= DateTime.Now
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
        public Doctor DoctorChangePassword(int doctorId,string newPass)
        {
            var doctor = myDb.Doctors.Where(p=>p.DoctorId == doctorId).FirstOrDefault();
            if(doctor== null)
                return null;
            try
            {
                doctor.Password = newPass;
                myDb.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
            return doctor;
        }
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

        public void DoctorUpdate(int id, Doctor doctor)
        {
            Doctor currentDoctor = (from _doctor in myDb.Doctors where _doctor.DoctorId == id select _doctor).First();
            currentDoctor.Name = doctor.Name;
            currentDoctor.Address = doctor.Address;
            currentDoctor.PhoneNumber = doctor.PhoneNumber;
            currentDoctor.Email = doctor.Email;
          
            myDb.SaveChanges();
        }

        #endregion

        #region LabClient
        public List<LabClient> GetLabClients()
        {
            return myDb.LabClients.ToList();
        }

        public List<LabClient> GetConnectedLab(int doctorId)
        {
          return  myDb.DoctorConnectMappings
                .Where(p => p.ConnectionState == (int)ConnectionStateEnum.Connected && p.DoctorId == doctorId)
                .Select(p => p.LabClient).ToList();
        }

        public int LabClientInsert(string Name, string Url, string Address, string Phone, int Type)
        {
            LabClient labClient = new LabClient();
            labClient.Name = Name;
            labClient.Url = Url;
            labClient.Address = Address;
            labClient.Phone = Phone;
            labClient.Type = Type;
            labClient.Amount = 0;

            myDb.LabClients.AddObject(labClient);
            myDb.SaveChanges();

            return labClient.LabId;
        }
        #endregion

        public void LabConnectMappingInsert(string connectionCode, int labId, int clientLabId, int connectionState)
        {
            //Kiem tra chua ton tai connection code thi moi insert
            var listOldMapping = myDb.LabConnectMappings.Where(p => p.LabId == labId && p.ClientLabId == clientLabId).ToList();
            if (listOldMapping != null && listOldMapping.Count != 0)
            {
                listOldMapping.ForEach(p => p.ConnectionState = (int)ConnectionStateEnum.ConnectionRemoveByLab);
            }
            //Insert new row
            LabConnectMapping mapping = new LabConnectMapping();
            mapping.LabId = labId;
            mapping.ClientLabId = clientLabId;
            mapping.ConnectionCode = connectionCode;
            mapping.ConnectionState = connectionState;
            myDb.LabConnectMappings.AddObject(mapping);
            myDb.SaveChanges();
        }

        public LabConnectMapping GetLabConnectMapping(string ConnectionCode)
        {
            return myDb.LabConnectMappings.Where(p => p.ConnectionCode == ConnectionCode).FirstOrDefault();
        }

        public bool CheckLabAccount(string UserName)
        {
            LabnetAccount lstLab = myDb.LabnetAccounts.Where(p => p.UserName == UserName).FirstOrDefault();
            if (null != lstLab)
                return true;
            return false;
        }

        public void UpdateMappingForLabConnect(int mappingId, int connectedLabId)
        {
            LabConnectMapping mapping = myDb.LabConnectMappings.Where(p => p.Id == mappingId).FirstOrDefault();
            mapping.ConnectedLabId = connectedLabId;
            mapping.DateConnected = DateTime.Now;
            mapping.ConnectionState = (int)ConnectionStateEnum.Connected;
            myDb.SaveChanges();
        }

        public List<LabClient> GetConnectedLabByLab(int? labId)
        {
            return myDb.LabConnectMappings
                  .Where(p => p.ConnectionState == (int)ConnectionStateEnum.Connected && p.ConnectedLabId == labId)
                  .Select(p => p.LabClient).ToList();
        }

        public List<VMLabConnectMapping> GetLabConnectMappings(int? labId)
        {
            var result = myDb.LabConnectMappings.Where(p => p.ConnectedLabId == labId && p.ConnectionState == (int)ConnectionStateEnum.Connected)
                                    .Select(p => new VMLabConnectMapping
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
                int patientInMonth = GetLabPatientInMonth(labId, item.LabId);
                int patientInDay = GetLabPatientInDay(labId, item.LabId);
                item.NumberPatientInDay = patientInDay;
                item.NumberPatientInMonth = patientInMonth;
            }
            return result;
        }

        private int GetLabPatientInDay(int? connectedLabId, int labId)
        {

            LabConnectMapping mapping = myDb.LabConnectMappings.Where(p => p.ConnectedLabId == connectedLabId && p.LabId == labId).FirstOrDefault();
            return myDb.Examinations.Where(p => p.LabId == labId && EntityFunctions.TruncateTime(p.CreatedDate) <= DateTime.Now
                                    && p.ClientPartnerId.HasValue
                                    && p.ClientPartnerId == mapping.ClientLabId).Count();
        }

        private int GetLabPatientInMonth(int? connectedLabId, int labId)
        {
            DateTime firstDateOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            LabConnectMapping mapping = myDb.LabConnectMappings.Where(p => p.ConnectedLabId == connectedLabId && p.LabId == labId).FirstOrDefault();

            return myDb.Examinations.Where(p => p.LabId == labId && p.CreatedDate > firstDateOfMonth
                                    && p.ClientPartnerId.HasValue
                                    && p.ClientPartnerId == mapping.ClientLabId).Count();
        }

        public bool IsLabConnectWithLab(int? currentLabId)
        {
            return myDb.LabConnectMappings.Any(p => p.ConnectedLabId == currentLabId && p.ConnectionState == (int)ConnectionStateEnum.Connected);
        }

        public List<VMLabExamination> GetLabExaminations(DateTime dateTime, int? labId)
        {
            var result = myDb.Examinations.Where(p => (EntityFunctions.TruncateTime(p.CreatedDate) == dateTime)
                                                        && (!labId.HasValue || labId.Value == p.LabId)).ToList();
            List<VMLabExamination> vmResult = result.Select(p => new VMLabExamination
            {
                BirthDay = p.BirthDay,
                CreatedDate = p.CreatedDate.ToString("d"),
                ExaminationId = p.LabId,
                ExaminationNumber = p.ExaminationNumber,
                PatientName = p.PatientName,
                Phone = p.Phone,
                Status = p.Status == 1 ? "Chưa có KQ" : "Có KQ"
            }).ToList();
            return vmResult;
        }

        public void LabClientUpdate(int id, LabClient lab)
        {
            LabClient currentLab = (from _lab in myDb.LabClients where _lab.LabId == id select _lab).First();
            currentLab.Name = lab.Name;
            currentLab.Address = lab.Address;
            currentLab.Phone = lab.Phone;

            myDb.SaveChanges();
        }

        public LabnetAccount LabChangePassword(int labId, string newPass)
        {
            var lab = myDb.LabnetAccounts.Where(p => p.UserId == labId).FirstOrDefault();
            if (lab == null)
                return null;
            try
            {
                lab.Password = newPass;
                myDb.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
            return lab;
        }
    }
}
