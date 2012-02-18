using DomainModel;
using System.Collections.Generic;
using DomainModel.Constant;
using System.Web.Mvc;
using LibraryFuntion;
using System.Linq;
using DataRepository;
using DomainModel.Constant;
using System;

namespace LabnetClient.Models
{
    public class PatientViewModel : BaseModel
    {
        public PatientViewModel()
        {
            LabExamination = new VMLabExamination();
        }

        public PatientViewModel(VMPatient patient, VMLabExamination labExamination, List<VMPatientTest> patientTest, List<VMPartner> listPartner, List<VMPanel> lstPanel)
        {
            Repository Repository = new DataRepository.Repository();
            Patient =patient;
            LabExamination = labExamination;
            if(    LabExamination.OrderNumber==null)
            LabExamination.OrderNumber = Repository.GetLabExaminationOrderNumber();
            
            JQGrid = new JQGridModel(typeof(VMPatientTest), true, patientTest, "/BenhNhan/SavePatientTest");
            
            Autocomplete = new AutocompleteModel();
            Autocomplete.JsonData = lstPanel.Select(p => new { Label = p.Name, Value = p.Id}).ToJson();
            Autocomplete.UseAjaxLoading = false;

            VMPartner partner = new VMPartner();
            partner.Id = -1;
            partner.Name = " ";
            listPartner.Insert(0, partner);
            SelectListPartner = new SelectList(listPartner, "Id", "Name");
        }

        /// <summary>
        /// Gets or set partner info
        /// </summary>
        public VMPatient Patient { get; set; }

        /// <summary>
        /// Autocomplete model
        /// </summary>
        public VMLabExamination LabExamination { get; set; }

        /// <summary>
        /// Gets or sets list test assigned to partner
        /// </summary>
        public List<VMTestListItem> PartnerTestList { get; set; }

        public JQGridModel JQGrid { get; set; }

        public AutocompleteModel Autocomplete { get; set; }

        public ViewMode ViewMode { get; set; }

        public SelectList SelectListPartner { get; set; }
    }
}