using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LabnetClient.Models
{
    public class DataTableModel
    {
        public string ActionLink { get; set; }

        public string JsonData { get; set; }

        public string UseEditTable { get; set; }

        public List<string> ColumnNames { get; set; }

        public string PostBackLink { get; set; }

        /// <summary>
        /// Gets or sets the value indicate wherever control dynamic loading (ajax loading)
        /// by default this vaule is false
        /// </summary>
        public bool UseAjaxLoading { get; set; }

        public string DataTableId { get; set; }
    }
}