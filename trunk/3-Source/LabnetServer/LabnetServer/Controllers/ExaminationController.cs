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
        //LabId={0}&ExaminationNumber={1}&Status={2}&PatientName={3}&Phone={4}&BirthDay={5}&ClientPartnerID={6}&ClientDoctorId={7}
        [HttpPost]
        public string InsertExamination(string ExaminationNumber, int LabId, int Status, string PatientName, string Phone, string BirthDay, int? ClientPartnerId,int? ClientDoctorId)
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
