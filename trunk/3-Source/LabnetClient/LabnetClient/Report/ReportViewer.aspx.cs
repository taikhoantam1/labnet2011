using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using DataRepository;
using DomainModel.Report;
using DomainModel;
using AutoMapper;

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
                    case "report_SoQuanLyTaiChinh":
                        Report_SoQuanLyTaiChinh(reportName);
                        break;
                }
            }
        }

        private void Report_SoQuanLyTaiChinh(string ReportName)
        {
            IDataRepository repository = new Repository();
            string startDate = Request["StartDate"];
            string endDate = Request["EndDate"];
            string partnerName = Request["PartnerName"];
            string partnerType = Request["PartnerType"];
            int partnerId = int.Parse(Request["PartnerId"]);

            ReportParameter paramStartDate = new ReportParameter("StartDate", startDate);
            ReportParameter paramEndDate = new ReportParameter("EndDate", endDate);
            ReportParameter paramPartnerName = new ReportParameter("PartnerName", partnerName);
            List<Report_BaoCaoTaiChinh> results = null;
            results = repository.ReportDataBaoCaoTaiChinh(DateTime.Parse(startDate), DateTime.Parse(endDate), partnerId, partnerType);


            string reportFullName = "/Report/" + ReportName + ".rdlc";
            reportViewer.LocalReport.ReportPath = Server.MapPath(reportFullName);
            reportViewer.LocalReport.SetParameters(paramStartDate);
            reportViewer.LocalReport.SetParameters(paramEndDate);
            reportViewer.LocalReport.SetParameters(paramPartnerName);
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("BaoCaoTaiChinh", results));
        }

        private void Report_TestResultNoteBook(string ReportName)
        {
            IDataRepository repository = new Repository();
            string startDate = Request["StartDate"];
            string endDate = Request["EndDate"];
            ReportParameter paramStartDate = new ReportParameter("StartDate", startDate);
            ReportParameter paramEndDate = new ReportParameter("EndDate", endDate);
            List<Report_TestResultNoteBook> results = repository.ReportDataTestResultNoteBook(DateTime.Parse(startDate), DateTime.Parse(endDate));

            string reportFullName = "/Report/" + ReportName + ".rdlc";
            reportViewer.LocalReport.ReportPath = Server.MapPath(reportFullName);
            reportViewer.LocalReport.SetParameters(paramStartDate);
            reportViewer.LocalReport.SetParameters(paramEndDate);
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ResultNoteBook", results));
        }


        private void Report_PatientResult(string ReportName)
        {
            string labName = System.Configuration.ConfigurationManager.AppSettings["LabName"].ToString();
            reportViewer.LocalReport.EnableExternalImages = true;
            IDataRepository repository = new Repository();
            int labExamination = int.Parse(Request["LabExaminationId"]);
            Partner partner = repository.GetPartnerByLabExamination(labExamination);
            LabExamination lab = Mapper.Map<VMLabExamination, LabExamination>(repository.GetLabExaminationById(labExamination));
            var fileRootPath = GetReportLogo(partner);
            ReportParameter path = new ReportParameter("Path", fileRootPath.AbsoluteUri);
            List<Report_PatientResult> results = repository.ReportDataPatientResult(labExamination);
            string reportFullName = "";
            if (string.IsNullOrEmpty(labName))
            {
                reportFullName = "/Report/" + ReportName + ".rdlc";
            }
            else
            {
                reportFullName = "/Report/" + labName + "/" + ReportName + ".rdlc";
            }
            reportViewer.LocalReport.ReportPath = Server.MapPath(reportFullName);
            reportViewer.LocalReport.SetParameters(path);
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("PatientResult", results));
        }

        private Uri GetReportLogo(Partner partner)
        {
            string defaultLogo = "Content\\Images\\Sites_Banner\\defaultLogo.JPG";
            String rootPath = Server.MapPath("~");
            if (partner != null && !string.IsNullOrEmpty(partner.Logo))
            {
                rootPath += partner.Logo;
            }
            else
            {
                rootPath += defaultLogo;
            }
            Uri fileRootPath = new Uri(rootPath);
            return fileRootPath;
        }
    }
}