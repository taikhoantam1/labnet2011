using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LabnetServer.Models;
using DataRepository;
using DomainModel;

namespace LabnetServer.Controllers
{
    public class LabController : BaseController
    {
        //
        // GET: /Lab/
        LabnetAccount CurrentLab
        {
            get
            {
                if (Session[SessionProperties.SessionLab] == null)
                    return null;

                return (LabnetAccount)Session[SessionProperties.SessionLab];
            }
            set
            {
                Session[SessionProperties.SessionLab] = value;
            }
        }

        [PermissionsAttribute(SessionProperties.SessionLab)]
        public ActionResult Index()
        {
            DanhSachBenhNhanModel model = new DanhSachBenhNhanModel(Repository.GetConnectedLabByLab(CurrentLab.LabId));
            return View(model);
        }

        [HttpPost]
        [PermissionsAttribute(SessionProperties.SessionLab)]
        public ActionResult Index(DanhSachBenhNhanModel model)
        {
            List<VMLabExamination> examinations = Repository.GetLabExaminations(model.SentDate, model.LabId);
            int? labIdSelected = model.LabId;
            model = new DanhSachBenhNhanModel(Repository.GetConnectedLabByLab(CurrentLab.LabId));
            model.DanhSachBenhNhanDataTableModel = new JQGridModel(typeof(VMLabExamination), false, examinations, "");
            if (labIdSelected.HasValue)
                model.LabComboBox.SelectedValue = labIdSelected.Value.ToString();

            return View(model);
        }
        //
        // GET: /Lab/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Lab/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Lab/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Lab/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Lab/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Lab/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Lab/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        [PermissionsAttribute(SessionProperties.SessionLab)]
        public ActionResult DanhSachLabKetNoi()
        {
            ThietLapLabKetNoiModel model = new ThietLapLabKetNoiModel();
            List<VMLabConnectMapping> list = Repository.GetLabConnectMappings(CurrentLab.LabId);
            model.DanhSachKetNoiModel = new JQGridModel(typeof(VMLabConnectMapping), false, list, "");
            model.LabAccount = CurrentLab;
            return View(model);
        }

        [HttpPost]
        [PermissionsAttribute(SessionProperties.SessionLab)]
        public string ConnectLabWithLab(string ConnectionCode)
        {
            AjaxResultModel model = new AjaxResultModel();
            LabConnectMapping mapping = Repository.GetLabConnectMapping(ConnectionCode);
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

            bool isLabConnectWithLab = Repository.IsLabConnectWithLab(CurrentLab.LabId);
            if (isLabConnectWithLab)
            {

                model.IsSuccess = false;
                model.ErrorMessage = "Kết nối thất bại: Bạn đã kết nối với lab có mã kết nối này.";
            }
            try
            {
                LabConnectMapping labConnect = Repository.GetLabConnectMapping(ConnectionCode);
                LabClient labClient = CurrentLab.LabClient;
                string result = ClientConnector.SetupLabConnectionWithLab(mapping.ConnectionCode, labClient.LabId, labConnect.ClientLabId, labConnect.LabClient.Url, CurrentLab.LabClient.Name);
                if (result == "Success")
                {
                    Repository.UpdateMappingForLabConnect(mapping.Id, labClient.LabId);
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
            LabnetAccount doctor = (LabnetAccount)Session[SessionProperties.SessionLab];
            LabClient labView = doctor.LabClient;
            LabModel model = new LabModel();
            model.Lab.Name = labView.Name;
            model.Lab.Address = labView.Address;
            model.Lab.Phone = labView.Phone;

            return PartialView(model);
        }

        [HttpPost]
        [PermissionsAttribute(SessionProperties.SessionLab)]
        public string AccountInformation(string Name, string Address, string PhoneNumber)
        {
            LabnetAccount labAccount = (LabnetAccount)Session[SessionProperties.SessionLab];
            LabClient labClient = labAccount.LabClient;
            labClient.Name = Name;
            labClient.Address = Address;
            labClient.Phone = PhoneNumber;
            Repository.LabClientUpdate(labClient.LabId, labClient);

            Session[SessionProperties.SessionLab] = labAccount;
            return new { Message = "Success" }.ToJson(); ;
        }

        [HttpGet]
        [PermissionsAttribute(SessionProperties.SessionLab)]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [PermissionsAttribute(SessionProperties.SessionLab)]
        public string ChangePassword(string oldPass, string newPass)
        {
            if (CurrentLab != null && CurrentLab.Password == oldPass)
            {
                LabnetAccount lab = Repository.LabChangePassword(CurrentLab.UserId, newPass);
                Session[SessionProperties.SessionLab] = lab;
                return new { Message = "Success" }.ToJson();
            }
            else
            {
                return new { Message = "Sai mật khẩu" }.ToJson(); ;
            }
        }
    }
}
