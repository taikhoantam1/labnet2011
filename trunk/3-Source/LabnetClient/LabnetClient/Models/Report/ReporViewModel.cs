using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LabnetClient.Models
{
    public class ReporViewModel
    {
        public ReporViewModel(string reportFileName, string reportTitle)
        {
            ReportName = reportFileName;
            ReportTitle= reportTitle;
            ReportParams = new Dictionary<string, string>();
        }

        public string ReportTitle { get; set; }

        public string ReportName { get; set; }

        public Dictionary<string, string> ReportParams { get; set; }

    }
}