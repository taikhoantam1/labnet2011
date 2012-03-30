using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LabnetServer.Controllers
{
    public class ExaminationController : BaseController
    {
        //
        // GET: /Examination/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string InsertExamination(string ExaminationNumber, int LabId, int Status)
        {
            //Create new record in Examination
            Repository.ExaminationInsert(ExaminationNumber, LabId, Status);
            //Update amount of lab
            Repository.UpdateLabAmount(LabId);
            return "success";
        }

        public string UpdateExmaination(string examinationNumber, int labId, int status)
        {
            //Update status of Examination
            return "success";
        }
    }
}
