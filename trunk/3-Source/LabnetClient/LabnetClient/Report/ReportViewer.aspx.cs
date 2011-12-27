using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using DataRepository;

namespace LabnetClient.Report
{
    public partial class ReportViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Report_PatientResult();
            }
        }

        private void Report_PatientResult()
        {
            IDataRepository repository = new Repository();
            List<Report_PatientResult> results= repository.ReportData_PatientResult(9);
            reportViewer.LocalReport.ReportPath = Server.MapPath("/Report/report_PatientResult.rdlc");
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("PatientResult", results));
        }
    }
}