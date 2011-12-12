using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LabnetClient.Models;
using DomainModel;
using DataRepository;
using AutoMapper;

namespace LabnetClient.Controllers
{
    public class PatientController : BaseController
    {
        public ActionResult Create()
        {
            List<VMPartner> lstPartner =Mapper.Map<List<Partner>,List<VMPartner>>( Repository.GetPartners());
            PatientViewModel Model = new PatientViewModel(new VMPatient(), new VMLabPatientManagement(), null, lstPartner);
            return View(Model);
        }
    }
}
