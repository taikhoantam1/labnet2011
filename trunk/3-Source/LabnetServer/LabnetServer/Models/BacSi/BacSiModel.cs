using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomainModel;

namespace LabnetServer.Models
{
    public class BacSiModel
    {
        public VMDoctor Doctor { get; set; }

        public BacSiModel()
        {
            Doctor = new VMDoctor();
        }
    }
}