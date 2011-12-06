using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomainModel;

namespace LabnetClient.Models
{
    public class DoctorViewModel : BaseModel
    {
        public VMDoctor Doctor { get; set; }

        public DoctorViewModel()
        {
            Doctor = new VMDoctor();
        }
    }
}