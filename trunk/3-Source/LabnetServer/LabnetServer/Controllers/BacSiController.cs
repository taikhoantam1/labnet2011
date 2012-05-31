using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LabnetServer.Models;
using DomainModel;
using Resources;
using DataRepository;

namespace LabnetServer.Controllers
{
    public class BacSiController : BaseController
    {
        //
        // GET: /BacSi/
        Doctor CurrentDoctor
        {
            get
            {
                if (Session[SessionProperties.SessionDoctor] == null)
                    return null;

                return (Doctor)Session[SessionProperties.SessionDoctor];
            }
            set {
                Session[SessionProperties.SessionDoctor]= value;
            }
        }

        [PermissionsAttribute(SessionProperties.SessionDoctor)]
        public ActionResult Index()
        {
            DanhSachBenhNhanModel model = new DanhSachBenhNhanModel(Repository.GetConnectedLab(CurrentDoctor.DoctorId));
            return View(model);
        }

        [HttpPost]
        [PermissionsAttribute(SessionProperties.SessionDoctor)]
        public ActionResult Index(DanhSachBenhNhanModel model)
        {
            List<VMExamination> examinations = Repository.GetExaminations(model.SentDate, model.LabId, CurrentDoctor.DoctorId);
            int? labIdSelected = model.LabId;
            model = new DanhSachBenhNhanModel(Repository.GetConnectedLab(CurrentDoctor.DoctorId));
            model.DanhSachBenhNhanDataTableModel = new JQGridModel(typeof(VMExamination), false, examinations, "");
            if (labIdSelected.HasValue)
                model.LabComboBox.SelectedValue = labIdSelected.Value.ToString();

            return View(model);
        }

        public ActionResult Login()
        {
            return PartialView();
        }

        [HttpPost]
        public string Login(string UserName, string Password)
        {
            Doctor doctor = Repository.GetDoctorByUserName(UserName);
            if (doctor != null)
            {
                if (doctor.Password == Password)
                {
                    Session[SessionProperties.SessionDoctor] = doctor;
                    return new { DoctorId = doctor.DoctorId, Message = "Success" }.ToJson();
                }
                else
                {
                    return new { Message = "Sai mật khẩu" }.ToJson(); ;
                }
            }
            return new { Message = "Không tồn tại tài khoản này" }.ToJson();
        }

        [HttpGet]
        [PermissionsAttribute(SessionProperties.SessionDoctor)]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [PermissionsAttribute(SessionProperties.SessionDoctor)]
        public string ChangePassword (string oldPass,string newPass)
        {
            if (CurrentDoctor!= null && CurrentDoctor.Password == oldPass)
            {
                Doctor doctor = Repository.DoctorChangePassword(CurrentDoctor.DoctorId,newPass);
                Session[SessionProperties.SessionDoctor] = doctor;
                    return new { Message = "Success" }.ToJson();
            }
            else
            {
                return new { Message = "Sai mật khẩu" }.ToJson(); ;
            }
        }

        public ActionResult Register()
        {
            return PartialView();
        }

        [HttpPost]
        public string Register(string Name, string UserName, string Password, string PasswordVerify,
                                    string ConnectionCode, string Address, string PhoneNumber, string Email)
        {
            DoctorConnectMapping doctorConnect = Repository.GetDoctorConnectMapping(ConnectionCode);

            // check user name
            bool isAccountExisted = Repository.CheckDoctorAccount(UserName);
            if (isAccountExisted)
            {
                return new { Message = "Tên đăng nhập đã được dùng bởi bác sĩ khác. Vui lòng chọn lại tên đăng nhập" }.ToJson();
            }

            if (null != doctorConnect)
            {

                Repository.DoctorInsert(Name, UserName, Password, Address, PhoneNumber, Email);
                Doctor doctor = Repository.GetDoctorByUserName(UserName);
                string result = ClientConnector.SetupConnectionWithLab(ConnectionCode, doctor.DoctorId, doctorConnect.ClientDoctorId, doctorConnect.LabClient.Url, doctor.Name);

                if (result == "Success")
                {
                    Repository.UpdateMappingForDoctorConnect(doctorConnect.Id, doctor.DoctorId);
                    Session[SessionProperties.SessionDoctor] = doctor;
                    return new { Message = "Success" }.ToJson();
                }
                else
                {
                    switch (result)
                    {
                        case "Doctor_Connection_Error1":
                            return new { Message = "Kết nối thất bại: Mã kết nối không tồn tại" }.ToJson();
                        case "Doctor_Connection_Error2":
                            return new { Message = "Kết nối thất bại: Mã kết nối đã được sử dụng" }.ToJson();
                        case "Doctor_Connection_Error3":
                            return new { Message = "Kết nối thất bại: Bạn đã kết nối với lab có mã kết nối này" }.ToJson();
                    }
                }

            }

            return new { Message = "Sai mã kết nối" }.ToJson();
        }

        [HttpGet]
        [PermissionsAttribute(SessionProperties.SessionDoctor)]
        public ActionResult DanhSachLabKetNoi()
        {
            ThietLapKetNoiModel model = new ThietLapKetNoiModel();
            List<VMDoctorConnectMapping> list = Repository.GetDoctorConnectMappings(CurrentDoctor.DoctorId);
            model.DanhSachKetNoiModel = new JQGridModel(typeof(VMDoctorConnectMapping), false, list, "");
            model.Doctor = CurrentDoctor;
            return View(model);
        }

        [HttpPost]
        [PermissionsAttribute(SessionProperties.SessionDoctor)]
        public string ConnectWithLab(string ConnectionCode)
        {
            AjaxResultModel model = new AjaxResultModel();
            DoctorConnectMapping mapping = Repository.GetDoctorConnectMapping(ConnectionCode);
            // Kiem tra connection code co ton tai chua

            if (mapping == null)
            {
                model.IsSuccess = false;
                model.ErrorMessage = "Kết nối thất bại: Mã kết nối không tồn tại.";
                return model.ToJson();
            }
            // Kiem tra connection code có được dùng chưa
            if (mapping != null && mapping.ConnectionState != (int)ConnectionStateEnum.Available)
            {

                model.IsSuccess = false;
                model.ErrorMessage = "Kết nối thất bại: Mã kết nối đã được sử dụng.";
            }

            // Kiem tra basi đã kết nối với lab chưa

            bool isDoctorConnectWithLab = Repository.IsDoctorConnectWithLab(CurrentDoctor.DoctorId,ConnectionCode);
            if (isDoctorConnectWithLab)
            {

                model.IsSuccess = false;
                model.ErrorMessage = "Kết nối thất bại: Bạn đã kết nối với lab có mã kết nối này.";
            }
            try
            {
                string result = ClientConnector.SetupConnectionWithLab(mapping.ConnectionCode, CurrentDoctor.DoctorId, mapping.ClientDoctorId, mapping.LabClient.Url, CurrentDoctor.Name);
                if (result == "Success")
                {
                    Repository.UpdateMappingForDoctorConnect(mapping.Id, CurrentDoctor.DoctorId);
                    model.IsSuccess = true;
                }
                else
                {
                    model.IsSuccess = false;
                    switch (result)
                    {
                        case "Doctor_Connection_Error1":
                            model.ErrorMessage = "Kết nối thất bại: Mã kết nối không tồn tại";
                            break;
                        case "Doctor_Connection_Error2":
                            model.ErrorMessage = "Kết nối thất bại: Mã kết nối đã được sử dụng";
                            break;
                        case "Doctor_Connection_Error3":
                            model.ErrorMessage = "Kết nối thất bại: Bạn đã kết nối với lab có mã kết nối này";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {

                model.IsSuccess = false;
                model.ErrorMessage = ex.Message;
            }

            return model.ToJson();
        }

        public ActionResult AccountInformation()
        {
            Doctor doctor = (Doctor) Session[SessionProperties.SessionDoctor];
            Doctor doctorView = Repository.GetDoctorByUserName(doctor.UserName);
            BacSiModel model = new BacSiModel();
            model.Doctor.Name = doctorView.Name;
            model.Doctor.Address = doctorView.Address;
            model.Doctor.Email = doctorView.Email;
            model.Doctor.Phone = doctorView.PhoneNumber;

            return PartialView(model);
        }

        [HttpPost]
        [PermissionsAttribute(SessionProperties.SessionDoctor)]
        public string AccountInformation(string Name, string Address, string PhoneNumber, string Email)
        {
            Doctor doctor = (Doctor)Session[SessionProperties.SessionDoctor];
            doctor.Name = Name;
            doctor.Address = Address;
            doctor.PhoneNumber = PhoneNumber;
            doctor.Email = Email;
            Repository.DoctorUpdate(doctor.DoctorId, doctor);

            Session[SessionProperties.SessionDoctor] = doctor;
            return new { Message = "Success" }.ToJson(); ;
        }
    }
}
