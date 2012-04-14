using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LabnetServer.Models
{
    public class ComboBoxModel
    {
        public ComboBoxModel()
        { }

        public ComboBoxModel(string bindingName, List<OptionItem> comboBoxData)
        {
            BindingName = bindingName;
            ComboBoxId = String.Format("comboBox_{0}", Guid.NewGuid().ToString().Replace("-",""));
            ComboBoxData = comboBoxData;
            IsEnabled = true;
        }


        /// <summary>
        /// Gets or sets value of name control value field 
        /// which specific dependent each parent model use this control ex: [Partner_Name]
        /// </summary>
        public string BindingName { get; set; }

        /// <summary>
        /// Gets or set id of ComboBox control
        /// </summary>
        public string ComboBoxId { get; set; }

        /// <summary>
        /// Gets or sets custome css class for this control
        /// </summary>
        public string CustomeCss { get; set; }

        /// <summary>
        /// Gets or sets data of control 
        /// </summary>
        public List<OptionItem> ComboBoxData { get; set; }

        /// <summary>
        /// Gets or sets selected value 
        /// </summary>
        public string SelectedValue { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string SelectedText { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SelectedTag { get; set; }

        public bool IsEnabled { get; set; }
    }
}