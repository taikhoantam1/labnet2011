using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataRepository;
using DomainModel;
using LabnetClient.Models;
using LibraryFuntion;
using DomainModel.Constant;
using LabnetClient.App_Code;
using System;
using System.Transactions;

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
            ReporViewModel model = new ReporViewModel("report_PatientResult","Phiếu Kết Quả");
            model.ReportParams.Add("LabExaminationId", labExaminationId.ToString());
            return View("ReportViewer", model);
        }

        public ActionResult PatientResultReport(int? OrderNumber, string ReceivedDate)
        {
            ReporViewModel model = new ReporViewModel("report_PatientResult", "Phiếu Kết Quả");
            DateTime? receivedDate = null;
            if (!string.IsNullOrEmpty(ReceivedDate))
                receivedDate = Convert.ToDateTime(ReceivedDate);
            VMLabExamination labExamination = new VMLabExamination();
            labExamination = Repository.GetLabExamination(OrderNumber.Value, receivedDate.Value);
            if(labExamination == null)
                model.ReportParams.Add("LabExaminationId", "0");
            else
                model.ReportParams.Add("LabExaminationId", labExamination.Id.ToString());

            return View("ReportViewer", model);
        }
        [HttpGet]
        public ActionResult ResultNoteBook()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ResultNoteBook(string startDate, string endDate)
        {
            if (string.IsNullOrEmpty(startDate) || string.IsNullOrEmpty(endDate))
            {
                ModelState.AddModelError("Input Error", "Vui lòng nhập đầy đủ ngày bắt đầu và ngày kết thúc");
                ResultNoteBookViewModel m = new ResultNoteBookViewModel();
                m.StartDate = startDate;
                m.EndDate = endDate;
                return View(m);
            }

            ReporViewModel model = new ReporViewModel("report_TestResultNoteBook", "SỔ LƯU KẾT QUẢ XÉT NGHIỆM");
            model.ReportParams.Add("StartDate",startDate);
            model.ReportParams.Add("EndDate", endDate);
            return View("ReportViewer", model);
        }
    }
}
