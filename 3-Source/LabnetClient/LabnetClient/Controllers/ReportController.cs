using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using DataRepository;
using DomainModel;
using LabnetClient.Models;
using LibraryFuntion;
using DomainModel.Constant;
using LabnetClient.App_Code;
using System;
using System.Transactions;
using Resources;

namespace LabnetClient.Controllers
{
    public class ReportController : BaseController
    {
        //
        // GET: /Report/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PatientResult(int labExaminationId)
        {
            ReporViewModel model = new ReporViewModel("report_PatientResult", "Phiếu Kết Quả");
            model.ReportParams.Add("LabExaminationId", labExaminationId.ToString());
            return View("ReportViewer", model);
        }


        [HttpGet]
        public ActionResult PatientResultReport_ForServer(string examinationNumber)
        {
            ReporViewModel model = new ReporViewModel("report_PatientResult", "Phiếu Kết Quả");
            VMLabExamination labExamination = new VMLabExamination();
            labExamination = Repository.GetLabExamination(examinationNumber);
            if (labExamination != null)
                model.ReportParams.Add("LabExaminationId", labExamination.Id.ToString());
            return View("PatientResult_Server", model);
        }

        [HttpGet]
        public ActionResult PatientResultReport()
        {
            PatientReportViewModel m = new PatientReportViewModel();
            return View(m);
        }
        [HttpPost]
        public ActionResult PatientResultReport(int? OrderNumber, string ReceivedDate)
        {
            PatientReportViewModel m = new PatientReportViewModel();
            m.OrderNumber = OrderNumber;
            m.ReceivedDate = ReceivedDate;
            DateTime? receivedDate = null;
            if (!string.IsNullOrEmpty(ReceivedDate))
                receivedDate = Convert.ToDateTime(ReceivedDate);
            VMLabExamination labExamination = new VMLabExamination();
            
            if (String.IsNullOrEmpty(OrderNumber.ToString()) || String.IsNullOrEmpty(ReceivedDate))
            {
                ModelState.AddModelError("Input Error", "Vui lòng nhập số thứ tự và ngày xét nghiệm");
            }

            if (!String.IsNullOrEmpty(OrderNumber.ToString()) && !String.IsNullOrEmpty(ReceivedDate))
            {
                labExamination = Repository.GetLabExamination(OrderNumber.Value, receivedDate.Value);
                if (labExamination == null)
                {
                    ModelState.AddModelError("Input Error", "Không tìm thấy dữ liệu nào phù hợp với dữ liệu nhập vào");
                }
            }

            if (!ModelState.IsValid)
            {
                return View(m);
            }

            ReporViewModel reportModel = new ReporViewModel("report_PatientResult", "Phiếu Kết Quả");
            reportModel.ReportParams.Add("LabExaminationId", labExamination.Id.ToString());
            m.ReportModel = reportModel;
            return View(m);
        }
        [HttpGet]
        public ActionResult QuanLyTaiChinhReport()
        {
            List<VMPartner> lstPartner = Mapper.Map<List<Partner>, List<VMPartner>>(Repository.GetPartners());
            List<VMDoctor> lstBacSi = Mapper.Map<List<Doctor>, List<VMDoctor>>(Repository.GetDoctorByName(""));
            QuanLyTaiChinhViewModel model = new QuanLyTaiChinhViewModel(lstPartner, lstBacSi);
            return View("QuanLyTaiChinh", model);
        }

        [HttpPost]
        public ActionResult QuanLyTaiChinhReport(QuanLyTaiChinhViewModel model)
        {

            List<VMPartner> lstPartner = Mapper.Map<List<Partner>, List<VMPartner>>(Repository.GetPartners());
            List<VMDoctor> lstBacSi = Mapper.Map<List<Doctor>, List<VMDoctor>>(Repository.GetDoctorByName(""));
            string startDate = model.StartDate;
            string endDate = model.EndDate;
            string partnerName = "";
            int? partnerId = null;
            string type = "Lab";

            if (model.PartnerId != 0 && model.PartnerId != -1)
            {
                Partner partner = Repository.GetPartnerById(model.PartnerId);
                partnerName = partner.Name;
                partnerId = partner.Id;
            }
            else if (model.DoctorId != 0 && model.DoctorId != -1)
            {
                Doctor partner = Repository.GetDoctor(model.DoctorId);
                partnerName = partner.Name;
                partnerId = partner.Id;
                type = "Doctor";

            }

            model = new QuanLyTaiChinhViewModel(lstPartner, lstBacSi);
            model.ComboBoxNoiGuiMauModel.SelectedText = partnerName;
            model.ComboBoxNoiGuiMauModel.SelectedValue = partnerId.ToString();

            if (string.IsNullOrEmpty(startDate) || string.IsNullOrEmpty(endDate))
            {
                ModelState.AddModelError("Input Error", "Vui lòng nhập đầy đủ ngày bắt đầu và ngày kết thúc");
            }

            if (!ModelState.IsValid)
            {
                return View("QuanLyTaiChinh", model);
            }

            ReporViewModel reportModel = new ReporViewModel("report_SoQuanLyTaiChinh", ReportStrings.QuanLyTaiChinh_Title);

            reportModel.ReportParams.Add("StartDate", startDate);
            reportModel.ReportParams.Add("EndDate", endDate);
            reportModel.ReportParams.Add("PartnerName", partnerName);
            reportModel.ReportParams.Add("PartnerType", type);
            reportModel.ReportParams.Add("PartnerId", partnerId.ToString());
            model.ReportModel = reportModel;
            model.EndDate = endDate;
            model.StartDate = startDate;
            return View("QuanLyTaiChinh", model);
        }
        [HttpGet]
        public ActionResult ResultNoteBook()
        {
            ResultNoteBookViewModel m = new ResultNoteBookViewModel();
            return View(m);
        }

        [HttpPost]
        public ActionResult ResultNoteBook(string startDate, string endDate)
        {
            ResultNoteBookViewModel m = new ResultNoteBookViewModel();
            m.StartDate = startDate;
            m.EndDate = endDate;

            if (string.IsNullOrEmpty(startDate) || string.IsNullOrEmpty(endDate))
            {
                ModelState.AddModelError("Input Error", "Vui lòng nhập đầy đủ ngày bắt đầu và ngày kết thúc");
                return View(m);
            }

            ReporViewModel reportModel = new ReporViewModel("report_TestResultNoteBook", "SỔ LƯU KẾT QUẢ XÉT NGHIỆM");
            reportModel.ReportParams.Add("StartDate", startDate);
            reportModel.ReportParams.Add("EndDate", endDate);
            m.ReportModel = reportModel;
            return View(m);
        }
    }
}
