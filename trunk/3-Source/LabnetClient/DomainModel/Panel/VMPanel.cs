using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using DomainModel.Properties;
using DomainModel.JQGrid;

namespace DomainModel
{
    public class VMPanel
    {
        public VMPanel()
        {
            Lastupdated = DateTime.Now;
        }

        public int Id { get; set; }

        [Required(ErrorMessageResourceName = "VMPanel_NameRequired", ErrorMessageResourceType = typeof(Resources))]
        [JQColumnAttribute("VMPanel_PanelName", true, false, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public string Name { get; set; }
        
        [JQColumnAttribute("VMPanel_Description", true, false, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public string RealDescription{ get { return string.IsNullOrWhiteSpace(Description) ? "" : Description; } }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public DateTime Lastupdated { get; set; }

        /// <summary>
        /// Link edit panel use in search panel
        /// </summary>
        [JQColumnAttribute("", true, false, false, EditTypeEnum.Link, FormatterEnum.EditLink)]
        public string PanelEditLink
        {
            get
            {
                return "/Panel/Edit/"+Id;
            }
        }
    }
}
