using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using DomainModel;

namespace DataRepository
{
    public class Repository : IRepository
    {
        private readonly LabnetServerEntities _myDb;
        public LabnetServerEntities Context { get { return _myDb; } }

        /// <summary>
        /// 
        /// </summary>
        public Repository()
        {
            _myDb = new LabnetServerEntities();
        }

        public LabnetAccount GetAccount(string userName)
        {
            LabnetAccount account = Context.LabnetAccounts.FirstOrDefault(p => p.UserName == userName);
            return account;
        }

        public int LabAccountInsert(string userName, string password, int labId, string domainUrl)
        {
            LabnetAccount labAccount = new LabnetAccount();

            labAccount.UserName = userName;
            labAccount.Password = password;
            labAccount.LabId = labId;
            labAccount.Domain = domainUrl;
            labAccount.Role = -1;

            _myDb.LabnetAccounts.AddObject(labAccount);
            _myDb.SaveChanges();

            return labAccount.UserId;
        }

        public LabnetAccount GetLabAccountByUserName(string userName)
        {
            return _myDb.LabnetAccounts.FirstOrDefault(p => p.UserName == userName);
        }
        #region Examination
        public Examination GetExamination(string examinatioNumber)
        {
            return _myDb.Examinations.FirstOrDefault(p => p.ExaminationNumber == examinatioNumber);
        }
        public List<VMExamination> GetExaminations(DateTime dateTime, int? labId)
        {
            var result = _myDb.Examinations.Where(p => (EntityFunctions.TruncateTime(p.CreatedDate) == dateTime)
                                                        && (!labId.HasValue || labId.Value == p.LabId)
                                                        && p.ClientDoctorId.HasValue).ToList();
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
            _myDb.Examinations.AddObject(ex);
            _myDb.SaveChanges();
        }

        public void UpdateLabAmount(int labId)
        {
            LabClient lab = _myDb.LabClients.FirstOrDefault(p => p.LabId == labId);
            if (lab != null)
            {
                lab.Amount--;
            }
            _myDb.SaveChanges();
        }
        #endregion

        #region Service

        public void RemoveDoctorConnect(int? serverDoctorId, int clientDoctorId, int labId, string connectionCode)
        {
            DoctorConnectMapping doctorConnectMapping = _myDb.DoctorConnectMappings.FirstOrDefault(p => p.ClientDoctorId == clientDoctorId
                                                                                                       && p.LabId == labId
                                                                                                       && p.ConnectionState == (int)ConnectionStateEnum.Connected);
            if (doctorConnectMapping != null)
            {
                doctorConnectMapping.ConnectionState = (int)ConnectionStateEnum.ConnectionRemoveByLab;
                _myDb.SaveChanges();
            }
        }

        public void RemoveLabConnect(int? serverLabId, int clientLabId, int labId, string connectionCode)
        {
            LabConnectMapping labConnectMapping = _myDb.LabConnectMappings.FirstOrDefault(p => p.ClientLabId == clientLabId
                                                                                               && p.LabId == labId
                                                                                               && p.ConnectionState == (int)ConnectionStateEnum.Connected);
            if (labConnectMapping != null)
            {
                labConnectMapping.ConnectionState = (int)ConnectionStateEnum.ConnectionRemoveByLab;
                _myDb.SaveChanges();
            }
            else
            {
                throw new Exception("Không tìm thấy dử liệu liên kết hợp lệ");
            }
        }
        #endregion

        #region DoctorConnectMapping

        public DoctorConnectMapping GetDoctorConnectMapping(string connectionCode)
        {
            return _myDb.DoctorConnectMappings.FirstOrDefault(p => p.ConnectionCode == connectionCode);
        }

        public List<VMDoctorConnectMapping> GetDoctorConnectMappings(int doctorId)
        {
            var result = _myDb.DoctorConnectMappings.Where(p => p.DoctorId == doctorId && p.ConnectionState == (int)ConnectionStateEnum.Connected)
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
            DoctorConnectMapping mapping = _myDb.DoctorConnectMappings.FirstOrDefault(p => p.Id == mappingId);
            if (mapping != null)
            {
                mapping.DoctorId = doctorId;
                mapping.DateConnected = DateTime.Now;
                mapping.ConnectionState = (int)ConnectionStateEnum.Connected;
            }
            _myDb.SaveChanges();
        }

        public void DoctorConnectMappingInsert(string connectionCode, int labId, int clientDoctoId, int connectionState)
        {
            //Kiem tra chua ton tai connection code thi moi insert
            var listOldMapping = _myDb.DoctorConnectMappings.Where(p => p.LabId == labId && p.ClientDoctorId == clientDoctoId).ToList();
            if (listOldMapping.Count != 0)
            {
                listOldMapping.ForEach(p => p.ConnectionState = (int)ConnectionStateEnum.ConnectionRemoveByLab);
            }
            //Insert new row
            DoctorConnectMapping mapping = new DoctorConnectMapping();
            mapping.LabId = labId;
            mapping.ClientDoctorId = clientDoctoId;
            mapping.ConnectionCode = connectionCode;
            mapping.ConnectionState = connectionState;
            _myDb.DoctorConnectMappings.AddObject(mapping);
            _myDb.SaveChanges();
        }

        private int GetPatientInDay(int doctorId, int labId)
        {

            DoctorConnectMapping mapping = _myDb.DoctorConnectMappings.FirstOrDefault(p => p.DoctorId == doctorId && p.LabId == labId);
            return _myDb.Examinations.Count(p => p.LabId == labId && EntityFunctions.TruncateTime( p.CreatedDate) <= DateTime.Now
                                                && p.ClientDoctorId.HasValue
                                                && p.ClientDoctorId == mapping.ClientDoctorId);
        }

        private int GetPatientInMonth(int doctorId, int labId)
        {
            DateTime firstDateOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DoctorConnectMapping mapping = _myDb.DoctorConnectMappings.FirstOrDefault(p => p.DoctorId == doctorId && p.LabId == labId);

            return _myDb.Examinations.Count(p => p.LabId == labId && p.CreatedDate > firstDateOfMonth
                                                && p.ClientDoctorId.HasValue
                                                && p.ClientDoctorId == mapping.ClientDoctorId);
        }
        #endregion

        #region Doctor
        public Doctor DoctorChangePassword(int doctorId,string newPass)
        {
            var doctor = _myDb.Doctors.FirstOrDefault(p => p.DoctorId == doctorId);
            if(doctor== null)
                return null;
            doctor.Password = newPass;
            _myDb.SaveChanges();
            return doctor;
        }
        public bool IsDoctorConnectWithLab(int currentDoctorId)
        {
            return _myDb.DoctorConnectMappings.Any(p => p.DoctorId == currentDoctorId && p.ConnectionState == (int)ConnectionStateEnum.Connected);
        }

        public Doctor GetDoctor(int doctorId)
        {
            return _myDb.Doctors.FirstOrDefault(p => p.DoctorId == doctorId);
        }

        public bool CheckDoctorAccount(string userName)
        {
            Doctor lstDoctor = _myDb.Doctors.FirstOrDefault(p => p.UserName == userName);
            if (null != lstDoctor)
                return true;
            return false;
        }

        public Doctor GetDoctorByUserName(string userName)
        {
            return _myDb.Doctors.FirstOrDefault(p => p.UserName == userName);
        }

        public void DoctorInsert(string name, string userName, string password, string address, string phoneNumber, string email)
        {
            Doctor doctor = new Doctor
            {
                Name = name,
                UserName = userName,
                Password = password,
                Address = address,
                PhoneNumber = phoneNumber,
                Email = email
            };
            _myDb.Doctors.AddObject(doctor);
            _myDb.SaveChanges();
        }

        public void DoctorUpdate(int id, Doctor doctor)
        {
            Doctor currentDoctor = (from _doctor in _myDb.Doctors where _doctor.DoctorId == id select _doctor).First();
            currentDoctor.Name = doctor.Name;
            currentDoctor.Address = doctor.Address;
            currentDoctor.PhoneNumber = doctor.PhoneNumber;
            currentDoctor.Email = doctor.Email;
          
            _myDb.SaveChanges();
        }

        #endregion

        #region LabClient
        public List<LabClient> GetLabClients()
        {
            return _myDb.LabClients.ToList();
        }

        public List<LabClient> GetConnectedLab(int doctorId)
        {
          return  _myDb.DoctorConnectMappings
                .Where(p => p.ConnectionState == (int)ConnectionStateEnum.Connected && p.DoctorId == doctorId)
                .Select(p => p.LabClient).ToList();
        }

        public int LabClientInsert(string name, string url, string address, string phone, int type)
        {
            LabClient labClient = new LabClient();
            labClient.Name = name;
            labClient.Url = url;
            labClient.Address = address;
            labClient.Phone = phone;
            labClient.Type = type;
            labClient.Amount = 0;

            _myDb.LabClients.AddObject(labClient);
            _myDb.SaveChanges();

            return labClient.LabId;
        }
        #endregion

        public void LabConnectMappingInsert(string connectionCode, int labId, int clientLabId, int connectionState)
        {
            //Kiem tra chua ton tai connection code thi moi insert
            var listOldMapping = _myDb.LabConnectMappings.Where(p => p.LabId == labId && p.ClientLabId == clientLabId).ToList();
            if (listOldMapping.Count != 0)
            {
                listOldMapping.ForEach(p => p.ConnectionState = (int)ConnectionStateEnum.ConnectionRemoveByLab);
            }
            //Insert new row
            LabConnectMapping mapping = new LabConnectMapping();
            mapping.LabId = labId;
            mapping.ClientLabId = clientLabId;
            mapping.ConnectionCode = connectionCode;
            mapping.ConnectionState = connectionState;
            _myDb.LabConnectMappings.AddObject(mapping);
            _myDb.SaveChanges();
        }

        public LabConnectMapping GetLabConnectMapping(string connectionCode)
        {
            return _myDb.LabConnectMappings.FirstOrDefault(p => p.ConnectionCode == connectionCode);
        }

        public bool CheckLabAccount(string userName)
        {
            LabnetAccount lstLab = _myDb.LabnetAccounts.FirstOrDefault(p => p.UserName == userName);
            if (null != lstLab)
                return true;
            return false;
        }

        public void UpdateMappingForLabConnect(int mappingId, int connectedLabId)
        {
            LabConnectMapping mapping = _myDb.LabConnectMappings.FirstOrDefault(p => p.Id == mappingId);
            if (mapping != null)
            {
                mapping.ConnectedLabId = connectedLabId;
                mapping.DateConnected = DateTime.Now;
                mapping.ConnectionState = (int)ConnectionStateEnum.Connected;
            }
            _myDb.SaveChanges();
        }

        public List<LabClient> GetConnectedLabByLab(int? labId)
        {
            return _myDb.LabConnectMappings
                  .Where(p => p.ConnectionState == (int)ConnectionStateEnum.Connected && p.ConnectedLabId == labId)
                  .Select(p => p.LabClient).ToList();
        }

        public List<VMLabConnectMapping> GetLabConnectMappings(int? labId)
        {
            var result = _myDb.LabConnectMappings.Where(p => p.ConnectedLabId == labId && p.ConnectionState == (int)ConnectionStateEnum.Connected)
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

            LabConnectMapping mapping = _myDb.LabConnectMappings.FirstOrDefault(p => p.ConnectedLabId == connectedLabId && p.LabId == labId);
            return _myDb.Examinations.Count(p => p.LabId == labId && EntityFunctions.TruncateTime(p.CreatedDate) <= DateTime.Now
                                                && p.ClientPartnerId.HasValue
                                                && p.ClientPartnerId == mapping.ClientLabId);
        }

        private int GetLabPatientInMonth(int? connectedLabId, int labId)
        {
            DateTime firstDateOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            LabConnectMapping mapping = _myDb.LabConnectMappings.FirstOrDefault(p => p.ConnectedLabId == connectedLabId && p.LabId == labId);

            return _myDb.Examinations.Count(p => p.LabId == labId && p.CreatedDate > firstDateOfMonth
                                                && p.ClientPartnerId.HasValue
                                                && p.ClientPartnerId == mapping.ClientLabId);
        }

        public bool IsLabConnectWithLab(int? currentLabId)
        {
            return _myDb.LabConnectMappings.Any(p => p.ConnectedLabId == currentLabId && p.ConnectionState == (int)ConnectionStateEnum.Connected);
        }

        public List<VMLabExamination> GetLabExaminations(DateTime dateTime, int? labId)
        {
            var result = _myDb.Examinations.Where(p => (EntityFunctions.TruncateTime(p.CreatedDate) == dateTime)
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
            LabClient currentLab = (from _lab in _myDb.LabClients where _lab.LabId == id select _lab).First();
            currentLab.Name = lab.Name;
            currentLab.Address = lab.Address;
            currentLab.Phone = lab.Phone;

            _myDb.SaveChanges();
        }

        public LabnetAccount LabChangePassword(int labId, string newPass)
        {
            var lab = _myDb.LabnetAccounts.FirstOrDefault(p => p.UserId == labId);
            if (lab == null)
                return null;
            lab.Password = newPass;
            _myDb.SaveChanges();
            return lab;
        }

        public void UpdateExaminationStatus(string examinationNumber, int status)
        {
            var examination = _myDb.Examinations.FirstOrDefault(p => p.ExaminationNumber== examinationNumber);
            if (examination == null)
                throw new ObjectNotFoundException(string.Format("Not found any examination match with examination number {0}",examinationNumber));
            examination.Status = status;
            _myDb.SaveChanges();
        }

        public void ExaminationUpdate(string examinationNumber, int labId, int status, string patientName, string phone, string birthDay, int? clientPartnerId, int? clientDoctorId)
        {
            var examination = _myDb.Examinations.FirstOrDefault(p => p.ExaminationNumber == examinationNumber);

            // Kiểm tra xem có dòng nào với ExaminationNumber và LabId tồn tại chưa
            if (examination == null)
                throw new ObjectNotFoundException(string.Format("Not found any examination match with examination number {0}", examinationNumber));
            examination.LabId = labId;
            examination.Status = status;
            examination.PatientName = patientName;
            examination.Phone = phone;
            examination.Phone = phone;
            examination.BirthDay = birthDay;
            examination.ClientDoctorId=clientDoctorId;
            examination.ClientPartnerId= clientPartnerId;
            _myDb.SaveChanges();
        }
    }
}
