using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainModel
{
    public class VMLabExamination
    {
        public VMLabExamination()
        {
            ReceivedDate = DateTime.Now.Date;
        }
        public int Id { get; set; }

        public int PatientId { get; set; }

        public int? PartnerId { get; set; }

        public string PartnerName { get; set; }

        public DateTime? ReceivedDate { get; set; }

        public int CreatedBy { get; set; }

        public int Status { get; set; }

        public int? OrderNumber { get; set; }

        public string Diagnosis { get; set; }

        public string ExaminationNumber { get; set; }

        public int? DoctorId { get; set; }

        public string DoctorName{ get; set; }

    }
}
