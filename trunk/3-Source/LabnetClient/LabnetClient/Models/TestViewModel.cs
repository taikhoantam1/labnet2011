using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomainModel;

namespace LabnetClient.Models
{
    public class TestViewModel : BaseModel
    {
        public VMTest Test { get; set; }
        public TestViewModel()
        {
            Test = new VMTest();  
        }
    }
}