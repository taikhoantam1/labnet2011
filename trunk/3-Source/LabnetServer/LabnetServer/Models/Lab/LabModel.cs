using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomainModel;

namespace LabnetServer.Models
{
    public class LabModel
    {
        public VMLab Lab { get; set; }

        public LabModel()
        {
            Lab = new VMLab();
        }
    }
}