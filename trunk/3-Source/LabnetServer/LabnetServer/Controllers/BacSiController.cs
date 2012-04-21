﻿using System;
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
                if( Session["Doctor"] == null)
                     Session["Doctor"] = Repository.GetDoctor(1);

                return (Doctor)Session["Doctor"];
            }
        }

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
            List<VMDoctorConnectMapping> list = Repository.GetDoctorConnectMappings(CurrentDoctor.DoctorId);
            model.DanhSachKetNoiModel = new JQGridModel(typeof(VMDoctorConnectMapping), false, list, "");
            model.Doctor = (Doctor)Session["Doctor"];
            return View(model);
        }

        [HttpPost]
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
            
            bool isDoctorConnectWithLab = Repository.IsDoctorConnectWithLab(CurrentDoctor.DoctorId);
            if (isDoctorConnectWithLab)
            {

                model.IsSuccess = false;
                model.ErrorMessage = "Kết nối thất bại: Bạn đã kết nối với lab có mã kết nối này.";
            }
            try
            {
                string result = ClientConnector.SetupConnectionWithLab(mapping.ConnectionCode, CurrentDoctor.DoctorId, mapping.ClientDoctorId, mapping.LabClient.Url,CurrentDoctor.Name);
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

    }
}