using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using DomainModel.Properties;

namespace DomainModel
{
    public class VMTestSearch
    {
        /// <summary>
        /// Sets or gets test name
        /// </summary>
        [Required(ErrorMessageResourceName = "VMTestSearch_NameRequired", ErrorMessageResourceType = typeof(Resources))]
        public string TestName { get; set; }

        /// <summary>
        /// Sets or gets Test section name
        /// </summary>
        public string TestSectionName { get; set; }

        /// <summary>
        /// Sets or gets Panel name
        /// </summary>
        public string PanelName { get; set; }

        public List<TestSearchObject> ObjSearchResult { get; set; }
    }

    public class TestSearchObject
    {
        /// <summary>
        /// Sets or gets Test Id
        /// </summary>
        public int TestId { get; set; }

        /// <summary>
        /// Sets or gets Test Name
        /// </summary>
        public string TestName { get; set; }

        /// <summary>
        /// Sets or gets Panel name
        /// </summary>
        public string PanelName { get; set; }

        /// <summary>
        /// Sets or gets Test range
        /// </summary>
        public string TestRange { get; set; }

        /// <summary>
        /// Sets or gets Test unit
        /// </summary>
        public string TestUnit { get; set; }

        public string TestSectionName { get; set; }
    }
}
