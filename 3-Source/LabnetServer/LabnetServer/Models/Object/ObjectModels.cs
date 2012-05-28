using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomainModel;

namespace LabnetServer.Models
{
    public class ObjectModels
    {
        public VMLab Object { get; set; }

        public ObjectModels()
        {
            Object = new VMLab();
        }
    }
}