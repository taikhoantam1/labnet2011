using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainModel
{
    public class VMPatient
    {
        public string PatientNumber { get; set; }

        public string FirstName { get; set; }

        public string Address { get; set; }

        public bool Gender { get; set; }

        public string BirthDate { get; set; }

        public string Age { get; set; }

        public int Status { get; set; }
    }
}
