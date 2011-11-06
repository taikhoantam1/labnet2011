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

        public TestSearctViewModel()
        {
            TestSearch = new VMTestSearch();
        }
    }
}