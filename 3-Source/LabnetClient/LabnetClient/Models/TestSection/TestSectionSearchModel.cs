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

        public bool IsActive { get; set; }

        public TestSectionSearchModel()
        {
        }

        public TestSectionSearchModel(List<VMTestSection> testSections)
        {
            JQGrid = new JQGridModel(typeof(VMPanel), false, testSections, "");
            IsActive = true;
        }
    }
}