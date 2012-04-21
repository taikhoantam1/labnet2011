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
       
    }
}
