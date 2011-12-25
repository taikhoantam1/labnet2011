using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomainModel;
using DomainModel.Constant;
using System.Web.Mvc;

namespace LabnetClient.Models
{
    public class PatientTestViewModel
    {
        public PatientTestViewModel(VMPatient patient, VMLabExamination labExamination, List<VMTestResult> tests, List<VMPartner> listPartner)
        {
            Patient = patient;
            LabExamination = labExamination;
            JQGrid = new JQGridModel(typeof(VMTestResult), true, tests, "/BenhNhan/SaveTestResults");
            VMPartner partner = new VMPartner();
            partner.Id = -1;
            partner.Name = " ";
            listPartner.Insert(0, partner);
            SelectListPartner = new SelectList(listPartner, "Id", "Name");
        }
        public VMPatient Patient { get; set; }

        public VMLabExamination LabExamination { get; set; }

        public JQGridModel JQGrid { get; set; }

        public SelectList SelectListPartner { get; set; }
    }
}