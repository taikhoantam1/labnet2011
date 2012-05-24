using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using DomainModel.Properties;
using DomainModel.JQGrid;

namespace DomainModel
{
    public class VMTestSearch
    {
        /// <summary>
        /// Sets or gets test name
        /// </summary>
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
        [JQColumnAttribute("VMTestSearch_TestName", true, false, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public string TestName { get; set; }

        /// <summary>
        /// Sets or gets test description
        /// </summary>
        /// 
        [JQColumnAttribute("VMTestSearch_TestDescription", true, false, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public string TestDescription { get; set; }
        /// <summary>
        /// Sets or gets Test range
        /// </summary>
        [JQColumnAttribute("VMTestSearch_TestRange", false, false, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public string RealTestRange { get { return string.IsNullOrWhiteSpace(TestRange) ? "" : TestRange; } }

        public string TestRange { get; set; }

        /// <summary>
        /// Sets or gets Test unit
        /// </summary>
        [JQColumnAttribute("VMTestSearch_TestUnit", false, false, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public string RealTestUnit { get { return string.IsNullOrWhiteSpace(TestUnit) ? "" : TestUnit; } }

        public string TestUnit { get; set; }

        [JQColumnAttribute("VMTestSearch_TestSectionName", true, false, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public string TestSectionName { get; set; }

        /// <summary>
        /// Link edit panel use in search panel
        /// </summary>
        [JQColumnAttribute("", true, false, false, EditTypeEnum.Link, FormatterEnum.EditLink)]
        public string SearchEditLink
        {
            get
            {
                return "/XetNghiem/Edit/" + TestId;
            }
        }

    }
}
