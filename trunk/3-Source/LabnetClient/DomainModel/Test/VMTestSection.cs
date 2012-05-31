using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using DomainModel.Properties;
using DomainModel.JQGrid;

namespace DomainModel
{
    public class VMTestSection
    {
        public VMTestSection()
        {
            //IsActive = true;
        }
        /// <summary>
        /// Sets or gets name of test section
        /// </summary>
        /// 
        [Required(ErrorMessageResourceName = "VMTestSection_NameRequired", ErrorMessageResourceType = typeof(Resources))]
        [JQColumnAttribute("VMTestSection_Name", true, false, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public string Name { get; set; }

        /// <summary>
        /// Sets or gets description of test section
        /// </summary>
        /// 
        [Required(ErrorMessageResourceName = "VMTestSection_DescriptionRequired", ErrorMessageResourceType = typeof(Resources))]
        [JQColumnAttribute("VMTestSection_Description", true, false, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public string Description { get; set; }

        /// <summary>
        /// Sets or gets cost of test section
        /// </summary>
        [JQColumnAttribute("VMTestSection_Cost", true, false, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public decimal Cost { get; set; }

        [JQColumnAttribute("VMTestSection_IsActive", true, false, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public string Status { get; set; }
        /// <summary>
        /// Sets or gets status of test section
        /// </summary>
        
        public bool IsActive { get; set; }

        /// <summary>
        /// Sets or gets sort order of test section
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// Gets or sets Test Section Id
        /// </summary>
        [JQColumnAttribute("", true, true, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public int Id { get; set; }

        public bool UseCostForAssociateTest { get; set; }
        /// <summary>
        /// Link edit panel use in search panel
        /// </summary>
        [JQColumnAttribute("", true, false, false, EditTypeEnum.Link, FormatterEnum.EditLink)]
        public string TestSectionEditLink
        {
            get
            {
                return "/NhomXetNghiem/Edit/" + Id;
            }
        }
    }
}
