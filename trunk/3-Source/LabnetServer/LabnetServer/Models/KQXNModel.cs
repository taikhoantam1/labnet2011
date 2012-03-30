using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LabnetServer.Models
{
    public class KQXNModel
    {
        public KQXNModel()
        {
            ReportParams = new Dictionary<string, string>();
        }
        public string ExaminationNumber { get; set; }
        public string ReportTitle { get; set; }
        public string ReportName { get; set; }
        public string LabUrl { get; set; }
        public Dictionary<string, string> ReportParams { get; set; }
    }
}