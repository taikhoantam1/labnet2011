using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using DomainModel.Properties;

namespace DomainModel
{
    public class VMTestSection
    {
        public VMTestSection()
        {
            IsActive = true;
        }
        /// <summary>
        /// Sets or gets name of test section
        /// </summary>
        /// 
        [Required(ErrorMessageResourceName = "VMTestSection_NameRequired", ErrorMessageResourceType = typeof(Resources))]
        public string Name { get; set; }

        /// <summary>
        /// Sets or gets description of test section
        /// </summary>
        /// 
        [Required(ErrorMessageResourceName = "VMTestSection_DescriptionRequired", ErrorMessageResourceType = typeof(Resources))]
        public string Description { get; set; }

        /// <summary>
        /// Sets or gets status of test section
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Sets or gets cost of test section
        /// </summary>
        public decimal Cost { get; set; }

        /// <summary>
        /// Sets or gets sort order of test section
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// Gets or sets Test Section Id
        /// </summary>
        public int Id { get; set; }
    }
}
