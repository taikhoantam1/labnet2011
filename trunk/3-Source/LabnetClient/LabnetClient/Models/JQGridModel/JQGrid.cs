using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using LibraryFuntion;

namespace LabnetClient.Models
{
    
    public class JQGridModel
    {
        public JQGridModel(Type dataSourceType, string requestUrl)
        {
            DataSourceType = dataSourceType;
            RequestUrl = requestUrl;
            Columns = new List<JQGridColumn>();
            PopularGridInfo();
            PopularGridScript();

        }

        public JQGridModel(Type dataSourceType, string requestUrl, object dataSource)
        {
            DataSourceType = dataSourceType;
            RequestUrl = requestUrl;
            _DataSource = dataSource;
            if (dataSource != null)
            {
                HaveLocalData = true;
                JsonDataArray = DataSource.ToJson();
            }
            else
                HaveLocalData = false;

            Columns = new List<JQGridColumn>();
            PopularGridInfo();
            PopularGridScript();
            
        }

        private void PopularGridScript()
        {
            List<string> cols = new List<string>();
            foreach (var column in Columns)
            {
                cols.Add(string.Format(JQConstant.columnTemplate,
                    column.ColumnName, 
                    column.ColumnName, 
                    column.Width,
                    column.Editable.ToString().ToLower(), 
                    column.ColumnType, 
                    column.Visible.ToString().ToLower()));

            }
            cols[cols.Count - 1] += JQConstant.EditOptions.Replace("&#34;", "\"");
            ColModelScript = string.Join(",", cols.Select(p => "{" + p + "}"));
        }
        private object _DataSource;

        public string GridTitle { get; set; }

        public bool HaveLocalData { get; set; }

        public  string RequestUrl { get; set; }

        private Type DataSourceType { get; set; }

        public string JsonDataArray { get; set; }

        public object DataSource
        {
            get { return _DataSource; }
            set{
                _DataSource = value;
                if (_DataSource != null)
                {
                    HaveLocalData = true;
                    JsonDataArray = DataSource.ToJson();
                }
                else
                    HaveLocalData = false;

            
            } }

        private void PopularGridInfo()
        {
            Type type = DataSourceType;

            foreach (PropertyInfo property in type.GetProperties())
            {

                object[] customeAttributes = Attribute.GetCustomAttributes(property);
                foreach (var attribute in customeAttributes)
                {
                    if (attribute is DomainModel.JQColumnAttribute)
                    {
                        DomainModel.JQColumnAttribute jqColumnAttribute = (DomainModel.JQColumnAttribute)attribute;
                        JQGridColumn column = new JQGridColumn();
                        column.Editable = jqColumnAttribute.Editable;
                        column.Sortable = jqColumnAttribute.Sortable;
                        column.Visible = jqColumnAttribute.Visible;
                        column.DisplayName = jqColumnAttribute.DisplayName;
                        column.ColumnName = property.Name;
                        column.ColumnType = GetColumnType(jqColumnAttribute.EditType);
                        column.Width = jqColumnAttribute.Width;
                        Columns.Add(column);
                    }
                }

            }
        }

        private string GetColumnType(DomainModel.EditTypeEnum columnTypeAttribute)
        {
            string columnType = "";
            switch (columnTypeAttribute)
            {
                case DomainModel.EditTypeEnum.Text:
                    columnType = "text";
                    break;
                case DomainModel.EditTypeEnum.Autocomplete:
                    columnType = "autocomplete";
                    break;
                case DomainModel.EditTypeEnum.Checkbox:
                    columnType = "checkbox";
                    break;
                case DomainModel.EditTypeEnum.DateTime:
                    columnType = "checkbox";
                    break;
                case DomainModel.EditTypeEnum.DropdownList:
                    columnType = "dropdownlist";
                    break;
                case DomainModel.EditTypeEnum.TextArea:
                    columnType = "textarea";
                    break;
                case DomainModel.EditTypeEnum.Numeric:
                    columnType = "numeric";
                    break;
                case DomainModel.EditTypeEnum.Password:
                    columnType = "password";
                    break;
            }
            return columnType;
        }

        public List<JQGridColumn> Columns { get; set; }

        public bool AutoWidth { get; set; }

        public int Width { get; set; }

        public string PageSize { get; set; }

        #region Script
            public string ColModelScript { get; set; }
        #endregion
    }
}