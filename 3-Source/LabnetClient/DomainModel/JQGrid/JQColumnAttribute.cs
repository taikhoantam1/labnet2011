using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Resources;
using DomainModel.Properties;

namespace DomainModel.JQGrid
{
    public class JQColumnAttribute:Attribute
    {
        public JQColumnAttribute(string displayName, bool sortable, bool visible, bool editable, EditTypeEnum editType, FormatterEnum formatter)
        {
            DisplayName = GetDisplayName(displayName);
            Sortable = sortable;
            Hidden = visible;
            Editable = editable;
            Width=80;
            EditType = editType;
            Formatter = formatter;
        }

        private string GetDisplayName(string displayName)
        {
            ResourceManager rec = new ResourceManager(typeof(Resources));
           return string.IsNullOrEmpty( rec.GetString(displayName))?displayName:rec.GetString(displayName);
        }

        public string DisplayName { get; set; }

        public bool Sortable { get; set; }

        public bool Hidden { get; set; }

        public bool Editable { get; set; }

        public EditTypeEnum EditType { get; set; }

        public int Width{get;set;}

        public FormatterEnum Formatter { get; set; }
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
        Numeric,
        Link

    }

    public enum FormatterEnum
    {
        Text,
        Checkbox,
        Number,
        Currency,
        EditLink,
        DeleteLink,
    }
}