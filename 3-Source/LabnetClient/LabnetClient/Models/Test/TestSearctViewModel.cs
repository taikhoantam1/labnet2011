using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomainModel;

namespace LabnetClient.Models
{
    public class TestSearctViewModel : BaseModel
    {
        public VMTestSearch TestSearch { get; set; }

        public JQGridModel JQGrid { get; set; }

        public TestSearctViewModel()
        {
            TestSearch = new VMTestSearch();
            JQGrid = new JQGridModel(typeof(TestSearchObject), true, new List<TestSearchObject>(), "");
        }

        public TestSearctViewModel(List<TestSearchObject> lstTestSearch)
        {
            TestSearch = new VMTestSearch();
            JQGrid = new JQGridModel(typeof(TestSearchObject), true, lstTestSearch, "");
        }
    }
}