using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using LibraryFuntion;
using DomainModel.JQGrid;

namespace LabnetClient.Models
{

    public class JQGridModel
    {
        #region public method

        /// <summary>
        /// Contructor using server site data
        /// </summary>
        /// <param name="dataSourceType"></param>
        /// <param name="requestUrl"></param>
        /// <param name="postBackUrl"></param>
        public JQGridModel(Type dataSourceType,bool allowEdit, string requestUrl, string postBackUrl)
        {
            DataSourceType = dataSourceType;
            RequestUrl = requestUrl;
            DataSource = null;
            AllowEdit = allowEdit;
            Columns = new List<JQGridColumn>();
            PopularGridInfo();
            PopularGridScript();
            TableId = "DataTable_"+Guid.NewGuid().ToString().Replace('-', '_');

        }

        /// <summary>
        /// Contructor using local data
        /// </summary>
        /// <param name="dataSourceType"></param>
        /// <param name="dataSource"></param>
        /// <param name="postBackUrl"></param>
        public JQGridModel(Type dataSourceType, bool allowEdit, object dataSource, string postBackUrl)
        {
            DataSourceType = dataSourceType;
            RequestUrl = "";
            PostBackUrl = postBackUrl;
            DataSource = dataSource;
            AllowEdit = allowEdit;
            TableId = "DataTable_" + Guid.NewGuid().ToString().Replace('-', '_');

            if (DataSource != null)
            {
                JsonDataArray = DataSource.ToJson();
                UseLocalData = true;
            }
            else
                UseLocalData = false;
            Columns = new List<JQGridColumn>();
            PopularGridInfo();
            PopularGridScript();

        }
        #endregion

        #region private method

        private void PopularGridScript()
        {
            List<string> cols = new List<string>();
            for(int i=0 ;i < Columns.Count;i++)
            {
                var column=Columns[i];
                string EditOption="";
                if(CustomAttributes[i].EditType == EditTypeEnum.Checkbox)
                      EditOption=  string.Format(   JQConstant.EditOptions, "value:'True:False',",TableId);
                else
                    EditOption = string.Format(JQConstant.EditOptions, "", TableId);  
                cols.Add(string.Format(JQConstant.columnTemplate,
                    column.ColumnName,
                    column.ColumnName,
                    column.Width,
                    column.Editable.ToString().ToLower(),
                    column.ColumnType,
                    column.Visible.ToString().ToLower(),
                    column.Formatter,
                    CustomAttributes[i].Formatter==FormatterEnum.Checkbox?"{disabled: false}":"{}",
                    EditOption
                    ));

            }
        

            ColModelScript = string.Join(",", cols.Select(p => "\n{" + p + "}"));
        }

        private void PopularGridInfo()
        {
            Type type = DataSourceType;
            CustomAttributes = new List<JQColumnAttribute>();
            foreach (PropertyInfo property in type.GetProperties())
            {

                object[] customeAttributes = Attribute.GetCustomAttributes(property);
                foreach (var attribute in customeAttributes)
                {
                    if (attribute is JQColumnAttribute)
                    {
                        JQColumnAttribute jqColumnAttribute = (JQColumnAttribute)attribute;
                        JQGridColumn column = new JQGridColumn();
                        column.Editable = jqColumnAttribute.Editable;
                        column.Sortable = jqColumnAttribute.Sortable;
                        column.Visible = jqColumnAttribute.Hidden;
                        column.DisplayName = jqColumnAttribute.DisplayName;
                        column.ColumnName = property.Name;
                        column.ColumnType = GetColumnType(jqColumnAttribute.EditType);
                        column.Width = jqColumnAttribute.Width;
                        column.Formatter = GetFormatter(jqColumnAttribute.Formatter);
                        Columns.Add(column);
                        CustomAttributes.Add((JQColumnAttribute)attribute);
                    }
                }

            }
        }

        private string GetFormatter(FormatterEnum formatterEnum)
        {
            string formatter = "text";
            switch (formatterEnum)
            {
                case FormatterEnum.Text:
                    formatter = "'text'";
                    break;

                case FormatterEnum.Currency:
                    formatter = "'currency'";
                    break;

                case FormatterEnum.Checkbox:
                    formatter = "'checkbox'";
                    break;

                case FormatterEnum.Number:
                    formatter = "'number'";
                    break;

                case FormatterEnum.EditLink:
                    formatter = "editlink_" + TableId;
                    break;

                case FormatterEnum.DeleteLink:
                    formatter = "deletelink_" + TableId;
                    break;
            }
            return formatter;

        }

        private string GetColumnType(EditTypeEnum columnTypeAttribute)
        {
            string columnType = "";
            switch (columnTypeAttribute)
            {
                case EditTypeEnum.Text:
                    columnType = "text";
                    break;
                case EditTypeEnum.Autocomplete:
                    columnType = "autocomplete";
                    break;
                case EditTypeEnum.Checkbox:
                    columnType = "checkbox";
                    break;
                case EditTypeEnum.DateTime:
                    columnType = "dateTime";
                    break;
                case EditTypeEnum.DropdownList:
                    columnType = "dropdownlist";
                    break;
                case EditTypeEnum.TextArea:
                    columnType = "textarea";
                    break;
                case EditTypeEnum.Numeric:
                    columnType = "numeric";
                    break;
                case EditTypeEnum.Password:
                    columnType = "password";
                    break;
                case EditTypeEnum.Link:
                    columnType = "Link";
                    break;
            }
            return columnType;
        }

        #endregion

        #region private property
        private object DataSource
        {
            get;
            set;
        }
        private Type DataSourceType { get; set; }
        private List<JQColumnAttribute> CustomAttributes { get; set; }
        #endregion

        #region public property
        public List<JQGridColumn> Columns { get; set; }

        public bool AutoWidth { get; set; }

        public int? Width { get; set; }
        public int? Height { get; set; }

        public string PageSize { get; set; }

        public string PostBackUrl { get; set; }

        public string GridTitle { get; set; }

        public bool UseLocalData { get; set; }

        public string RequestUrl { get; set; }

        public string JsonDataArray { get; set; }

        public string ColModelScript { get; set; }

        public bool AllowEdit { get; set; }

        public string TableId { get; set; }
        #endregion
    }
}