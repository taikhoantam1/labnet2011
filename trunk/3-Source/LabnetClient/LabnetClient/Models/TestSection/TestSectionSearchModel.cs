using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomainModel;

namespace LabnetClient.Models
{
    public class TestSectionSearchModel : BaseModel
    {
        public string TestSectionName { get; set; }

        public JQGridModel JQGrid { get; set; }

        public AutocompleteModel Autocomplete { get; set; }

        public bool IsActive { get; set; }

        public TestSectionSearchModel(List<VMTestSection> testSections)
        {
            Autocomplete = new AutocompleteModel("TestSection.Name");
            JQGrid = new JQGridModel(typeof(VMPanel), false, testSections, "");
            IsActive = true;
        }
    }
}