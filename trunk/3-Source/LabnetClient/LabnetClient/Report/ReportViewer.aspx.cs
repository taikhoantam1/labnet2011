﻿using System;
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
                string reportName = Request["ReportName"];
                switch (reportName)
                {
                    case "report_PatientResult":
                        Report_PatientResult(reportName);
                        break;
                    case "report_TestResultNoteBook":
                        Report_TestResultNoteBook(reportName);
                        break;
                }
            }
        }

        private void Report_TestResultNoteBook(string ReportName)
        {
            IDataRepository repository = new Repository();
            string startDate= Request["StartDate"];
            string endDate = Request["EndDate"];
            ReportParameter paramStartDate = new ReportParameter("StartDate", startDate);
            ReportParameter paramEndDate = new ReportParameter("EndDate", endDate);
            List<report_TestResultNoteBook> results = repository.ReportData_TestResultNoteBook(DateTime.Parse(startDate),DateTime.Parse(endDate));

            string reportFullName = "/Report/" + ReportName + ".rdlc";
            reportViewer.LocalReport.ReportPath = Server.MapPath(reportFullName);
            reportViewer.LocalReport.SetParameters(paramStartDate);
            reportViewer.LocalReport.SetParameters(paramEndDate);
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ResultNoteBook", results));
        }


        private void Report_PatientResult(string ReportName)
        {
            IDataRepository repository = new Repository();
            int labExamination = int.Parse(Request["LabExaminationId"]);
            List<Report_PatientResult> results = repository.ReportData_PatientResult(labExamination);
            string reportFullName = "/Report/"+ReportName + ".rdlc";
            reportViewer.LocalReport.ReportPath = Server.MapPath(reportFullName);
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("PatientResult", results));
        }
    }
}