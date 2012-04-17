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
        int currentDoctorId = 1;
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DanhSachLabKetNoi()
        {
            //if (Session["Doctor"] == null)
            //    return Redirect(Constant.DomainUrl);
            ThietLapKetNoiModel model = new ThietLapKetNoiModel();
            List<VMDoctorConnectMapping> list= Repository.GetDoctorConnectMappings(1);
            model.DanhSachKetNoiModel = new JQGridModel(typeof(VMDoctorConnectMapping), false, list, "");
            model.Doctor = (VMDoctor)Session["Doctor"];
            return View(model);
        }

        [HttpPost]
        public string ConnectWithLab(string ConnectionCode)
        {
            AjaxResultModel model = new AjaxResultModel();
            DoctorConnectMapping mapping = Repository.GetDoctorConnectMapping(ConnectionCode);
            if (mapping == null)
            {
                model.IsSuccess = false;
                model.ErrorMessage = "Mã liên kết không tồn tại.";
            }
            else if (mapping != null && mapping.ConnectionState != (int)ConnectionStateEnum.Available)
            {

                model.IsSuccess = false;
                model.ErrorMessage = "Mã liên kết đã được sử dụng.";
            }

            try {
                string result = ClientConnector.SetupConnectionWithLab(mapping.ConnectionCode, currentDoctorId, mapping.ClientDoctorId, mapping.LabClient.Url);
                if (result == "Success")
                {
                    Repository.UpdateMappingForDoctorConnect(mapping.Id, currentDoctorId);
                    model.IsSuccess = true;
                }
                else
                {
                    model.IsSuccess = false;
                    model.ErrorMessage = result;
                }
            }
            catch (Exception ex)
            {

                model.IsSuccess = false;
                model.ErrorMessage = ex.Message;
            }

            return model.ToJson();
        }

    }
}
