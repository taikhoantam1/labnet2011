using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LabnetServer.Models;
using DataRepository;

namespace LabnetServer.Controllers
{
    public class KetQuaXetNghiemController : BaseController
    {
        //
        // GET: /KetQuaXetNghiem/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BenhNhan()
        {
            KQXNModel model = new KQXNModel();
            return View("KQXN_BenhNhan",model);
        }

        [HttpPost]
        public ActionResult BenhNhan(KQXNModel model)
        {
            Examination examination = Repository.GetExamination(model.ExaminationNumber);
            //model.LabUrl = "http://localhost:14587/Report/PatientResultReport_ForServer?ExaminationNumber=" + model.ExaminationNumber;
            
            if (examination != null)
            {
                model.LabUrl =string.Format("{0}/{1}={2}",examination.LabClient.Url,"Report/PatientResultReport_ForServer?ExaminationNumber",model.ExaminationNumber);
            }
            else 
            {
                ModelState.AddModelError("Examination not exist", "Mã số không tồn tại");
            }
            return View("KQXN_BenhNhan",model);
        }

        public ActionResult BacSi()
        {
            return View();
        }
    }
}
