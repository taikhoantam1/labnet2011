using DomainModel;
using System.Collections.Generic;
using LabnetClient.Constant;
using System.Web.Mvc;

namespace LabnetClient.Models
{
    public class PatientViewModel : BaseModel
    {
        public PatientViewModel(VMPatient patient, VMLabPatientManagement labManagement,List<VMPatientTest> patientTest,List<VMPartner> listPartner)
        {
            Patient =patient;
            LabManagement = new VMLabPatientManagement();
            JQGrid = new JQGridModel(typeof(VMTestListItem), true, patientTest, "");
            Autocomplete = new AutocompleteModel();
            VMPartner partner = new VMPartner();
            partner.Id = -1;
            partner.Name = "N/A";
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
        public VMLabPatientManagement LabManagement { get; set; }

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