using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainModel
{
    public class VMTestSection
    {
        /// <summary>
        /// Sets or gets name of test section
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Sets or gets description of test section
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Sets or gets status of test section
        /// </summary>
        public bool IsActive { get; set; }
    }
}
