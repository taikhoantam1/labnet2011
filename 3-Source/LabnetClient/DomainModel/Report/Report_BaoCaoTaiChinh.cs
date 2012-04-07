using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainModel.Report
{
    public class Report_BaoCaoTaiChinh
    {

        public string PartnerName { get; set; }
        public string PatientName { get; set; }
        public string Age { get; set; }
        public string Phone { get; set; }
        public string ReceiveDate { get; set; }
        public string ReturnDate { get; set; }
        public string Result { get; set; }
        public string Note { get; set; }
        public string StaffName { get; set; }
        public bool Male { get; set; }
        public decimal Cost { get; set; }
    }
}
