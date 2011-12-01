using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LabnetClient.Models
{
    public class JQColumnAttribute:Attribute
    {
        public JQColumnAttribute(string displayName, bool sortable, bool visible, bool editable)
        {
            DisplayName = displayName;
            Sortable = sortable;
            Visible = visible;
            Editable = editable;
            Width=80;
        }

        public string DisplayName { get; set; }

        public bool Sortable { get; set; }

        public bool Visible { get; set; }

        public bool Editable { get; set; }

        public EditTypeEnum EditType { get; set; }

        public int Width{get;set;}
    }

    public enum EditTypeEnum
    {
        Text,
        Checkbox,
        DateTime,
        TextArea,
        DropdownList,
        Autocomplete,
        Password,
        Numeric

    }
}