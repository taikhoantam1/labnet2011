using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LabnetClient.Models
{
    public class JQGridColumn
    {
        public JQGridColumn() { 
            Width=80;
        }
        public string ColumnName { get; set; }

        public string DisplayName { get; set; }

        public string ColumnType { get; set; }

        public string EditType { get; set; }

        public bool Editable { get; set; }

        public bool Visible { get; set; }

        public bool Sortable { get; set; }

        public int Width { get; set; }
    }
}