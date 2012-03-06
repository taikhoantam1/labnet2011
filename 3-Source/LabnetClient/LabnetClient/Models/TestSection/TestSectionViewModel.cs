using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomainModel;

namespace LabnetClient.Models
{
    public class TestSectionViewModel : BaseModel
    {
        public TestSectionViewModel()
        {
            TestSection = new VMTestSection();
        }

        public VMTestSection TestSection { get; set; }
    }
}