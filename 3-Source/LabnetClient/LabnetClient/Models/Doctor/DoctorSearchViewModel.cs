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

        public bool IsActive { get; set; }

        public JQGridModel JQGrid { get; set; }

        public DoctorSearchViewModel()
        {
          
            DoctorSearch = new VMDoctorSearch();
            JQGrid = new JQGridModel(typeof(DoctorSearchObject), true, new List<DoctorSearchObject>(), "");
            IsActive = true;
        }

        public DoctorSearchViewModel(List<DoctorSearchObject> ListSearchResult)
        {
         
            DoctorSearch = new VMDoctorSearch();
            JQGrid = new JQGridModel(typeof(DoctorSearchObject), true, ListSearchResult, "");
            IsActive = true;
        }
    }
}