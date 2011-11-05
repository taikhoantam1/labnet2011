using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LabnetClient.Models
{
    public class AutocompleteModel
    {

        public AutocompleteModel()
        {
            IsAjaxLoading = false;
            AutoCompleteId = String.Format("autoComplete_{0}", DateTime.Now.Ticks);
        }

        public AutocompleteModel(string bindingName):this()
        {
            BindingName = bindingName;
        }

        /// <summary>
        /// Gets or sets value of link to get data for control
        /// </summary>
        public string ActionLink { get; set; }

        /// <summary>
        /// Gets or sets value of name control value field 
        /// which specific dependent each parent model use this control ex: [Partner_Name]
        /// </summary>
        public string BindingName { get; set; }

        /// <summary>
        /// Gets or set id of autocomplete control
        /// </summary>
        public string AutoCompleteId { get; set; }

        /// <summary>
        /// Gets or sets the value indicate wherever control dynamic loading (ajax loading)
        /// by default this vaule is false
        /// </summary>
        public bool IsAjaxLoading { get; set; }

        /// <summary>
        /// Gets or sets custome css class for this control
        /// </summary>
        public string CustomeCss { get; set; }

        /// <summary>
        /// Gets or sets data of control if IsAjaxLoading is false
        /// </summary>
        public string JsonData { get; set; }
    }
}