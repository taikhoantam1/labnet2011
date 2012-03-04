using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using DomainModel.Properties;

namespace DomainModel
{
    public class VMTest
    {
        public VMTest()
        {
            LastUpdated = DateTime.Now;
            IsActive = true;
        }
        /// <summary>
        /// Sets or gets test's name
        /// </summary>
        /// 
        [Required(ErrorMessageResourceName = "VMTest_NameRequired", ErrorMessageResourceType = typeof(Resources))]
        public string Name { get; set; }

        /// <summary>
        /// Sets or gets test's description
        /// </summary>
        /// 
        [Required(ErrorMessageResourceName = "VMTest_DescriptionRequired", ErrorMessageResourceType = typeof(Resources))]
        public string Description { get; set; }

        /// <summary>
        /// Sets or gets low index of test
        /// </summary>
        public double? LowIndex { get; set; }

        /// <summary>
        /// Sets or gets high index of test
        /// </summary>
        public double? HighIndex { get; set; }

        /// <summary>
        /// Sets or gets test's unit
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// Sets or gets range of test
        /// </summary>
        public string Range { get; set; }

        /// <summary>
        /// Sets or gets department id of test belongs
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// Sets or gets user id that creates test
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Sets or gets sort order of test
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// Sets or gets test section id that test belongs
        /// </summary>
        [Required(ErrorMessageResourceName = "VMTest_TestSectionNameRequired", ErrorMessageResourceType = typeof(Resources))]
        public int TestSectionId { get; set; }

        /// <summary>
        /// Sets or gets status of test
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Sets or gets last updated of test
        /// </summary>
        public DateTime LastUpdated { get; set; }

        /// <summary>
        /// Sets or gets cost of test
        /// </summary>
        public decimal Cost { get; set; }

        /// <summary>
        /// Sets or gets IsBold
        /// </summary>
        public bool IsBold { get; set; }

        /// <summary>
        /// Gets or sets Department Name that test belongs to
        /// </summary>
       
        public string TestSectionName { get; set; }

        /// <summary>
        /// Gets or sets Test Id
        /// </summary>
        public int Id { get; set; }
    }
}
