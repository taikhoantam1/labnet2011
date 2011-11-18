using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainModel
{
    public class VMDoctorSearch
    {
        public string Name { get; set; }

        public List<DoctorSearchObject> ListSearchResult { get; set; }
    }

    public class DoctorSearchObject
    {
        public int Id { get; set; }

        public string DoctorName { get; set; }
    }
}
