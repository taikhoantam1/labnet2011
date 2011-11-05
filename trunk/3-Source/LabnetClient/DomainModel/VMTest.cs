using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainModel
{
    public class VMTest
    {
        /// <summary>
        /// Sets or gets test's name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Sets or gets test's description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Sets or gets low index of test
        /// </summary>
        public float? LowIndex { get; set; }

        /// <summary>
        /// Sets or gets high index of test
        /// </summary>
        public float? HighIndex { get; set; }

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
