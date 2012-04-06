using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LabnetClient.Models
{
    public class QuanLyTaiChinhViewModel
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int PartnerId { get; set; }
        public ReporViewModel ReportModel { get; set; }
        public SelectList DropDownListPartner { get; set; }
    }
}