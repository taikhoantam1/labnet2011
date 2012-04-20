using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomainModel;

namespace LabnetClient.Models
{
    public class DoctorSearchViewModel : BaseModel
    {
        public VMDoctorSearch DoctorSearch { get; set; }

        public AutocompleteModel Autocomplete { get; set; }

        public JQGridModel JQGrid { get; set; }

        public DoctorSearchViewModel()
        {
            Autocomplete = new AutocompleteModel("Doctor.Name");
            DoctorSearch = new VMDoctorSearch();
            JQGrid = new JQGridModel(typeof(DoctorSearchObject), true, new List<DoctorSearchObject>(), "");
        }

        public DoctorSearchViewModel(List<DoctorSearchObject> ListSearchResult)
        {
            Autocomplete = new AutocompleteModel("Doctor.Name");
            DoctorSearch = new VMDoctorSearch();
            JQGrid = new JQGridModel(typeof(DoctorSearchObject), true, ListSearchResult, "");
        }
    }
}