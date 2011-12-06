using DomainModel;
using System.Collections.Generic;

namespace LabnetClient.Models
{
    public class PatientViewModel : BaseModel
    {
        public PatientViewModel()
        {
            Patient = new VMPatient();
            LabManagement = new LabPatientManagement();
            Grid = new JQGridModel(typeof(VMTestListItem), "", "");
        }

        /// <summary>
        /// Gets or set partner info
        /// </summary>
        public VMPatient Patient { get; set; }

        /// <summary>
        /// Autocomplete model
        /// </summary>
        public LabPatientManagement LabManagement { get; set; }

        /// <summary>
        /// Gets or sets list test assigned to partner
        /// </summary>
        public List<VMTestListItem> PartnerTestList { get; set; }

        public JQGridModel Grid { get; set; }
    }
}