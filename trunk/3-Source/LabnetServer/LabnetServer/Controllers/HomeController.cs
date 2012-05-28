using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataRepository;
using DomainModel;

namespace LabnetServer.Controllers
{
    [HandleError]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return PartialView();
        }

        [HttpPost]
        public string Login(string UserName, string Password)
        {
            LabnetAccount account = Repository.GetAccount(UserName);
            if (account != null)
            {
                if (account.Password == Password)
                {
                    return new { Url = account.Domain, LabId = account.LabClient.LabId, Message = "Success" }.ToJson();
                }
                else
                {
                    return new {Message = "Sai mật khẩu" }.ToJson(); ;
                }
            }
            return new { Message = "Không tồn tại tài khoản này"}.ToJson(); 
        }

        public ActionResult Register()
        {
            return PartialView();
        }

        [HttpPost]
        public string Register(int Type, string Name, string UserName, string Password, string PasswordVerify,
                                    string ConnectionCode, string Address, string PhoneNumber, string Email)
        
        
        {
            DoctorConnectMapping doctorConnect = null;
            LabConnectMapping labConnect = null;

            if (Type == (int)ConnectionType.Doctor)
            {
                doctorConnect = Repository.GetDoctorConnectMapping(ConnectionCode);
            }

            if (Type == (int)ConnectionType.Lab)
            {
                labConnect = Repository.GetLabConnectMapping(ConnectionCode);
            }

            // check user name
            bool isDoctorAccountExisted = Repository.CheckDoctorAccount(UserName);
            bool isLabAccountExisted = Repository.CheckLabAccount(UserName);
            if (isDoctorAccountExisted)
            {
                return new { Message = "Tên đăng nhập đã được dùng bởi bác sĩ khác. Vui lòng chọn lại tên đăng nhập" }.ToJson();
            }

            if (isLabAccountExisted)
            {
                return new { Message = "Tên đăng nhập đã được dùng bởi lab khác. Vui lòng chọn lại tên đăng nhập" }.ToJson();
            }

            if (null != doctorConnect || null != labConnect)
            {
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

                if (null != labConnect)
                {
                    int connectedLabId = Repository.LabClientInsert(Name, Constant.DomainUrl, Address, PhoneNumber, (int)LabClientType.ConnectionLab);
                    int labAccountId = Repository.LabAccountInsert(UserName, Password, connectedLabId, Domain.DomainUrl);

                    //Doctor doctor = Repository.GetDoctorByUserName(UserName);
                    string result = ClientConnector.SetupLabConnectionWithLab(ConnectionCode, connectedLabId, labConnect.ClientLabId, labConnect.LabClient.Url, Name);

                    if (result == "Success")
                    {
                        Repository.UpdateMappingForLabConnect(labConnect.Id, connectedLabId);
                        //Session[SessionProperties.SessionDoctor] = doctor;
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

            }

            return new { Message = "Sai mã kết nối" }.ToJson();
        }

        public ActionResult ObjectLogin()
        {
            return PartialView();
        }

        [HttpPost]
        public string ObjectLogin(int Type, string UserName, string Password)
        {
            Doctor doctor = Repository.GetDoctorByUserName(UserName);
            LabnetAccount labAccount = Repository.GetLabAccountByUserName(UserName);
            if (doctor != null || labAccount != null)
            {
                if (Type == (int)ConnectionType.Doctor)
                {
                    if (doctor.Password == Password)
                    {
                        Session[SessionProperties.SessionDoctor] = doctor;
                        return new { DoctorId = doctor.DoctorId, Message = "SuccessDoctor" }.ToJson();
                    }

                    else
                    {
                        return new { Message = "Sai mật khẩu" }.ToJson(); ;
                    }
                }
                if (Type == (int)ConnectionType.Lab)
                {
                    if (labAccount.Password == Password)
                    {
                        Session[SessionProperties.SessionLab] = labAccount;
                        return new { LabId = labAccount.UserId, Message = "SuccessLab" }.ToJson();
                    }

                    else
                    {
                        return new { Message = "Sai mật khẩu" }.ToJson(); ;
                    }
                }

                else
                {
                    return new { Message = "" }.ToJson(); ;
                }
            }
            return new { Message = "Không tồn tại tài khoản này" }.ToJson();
        }
    }
}
