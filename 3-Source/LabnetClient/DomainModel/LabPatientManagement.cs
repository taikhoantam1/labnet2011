using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainModel
{
    public class LabPatientManagement
    {
        public int Id { get; set; }

        public int PatientId { get; set; }

        public int? Partner { get; set; }

        public DateTime ReceivedDate { get; set; }

        public int CreatedBy { get; set; }

        public int Status { get; set; }

        public int OrderNumber { get; set; }
    }
}
