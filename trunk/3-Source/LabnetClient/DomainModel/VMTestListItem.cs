using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Properties;

namespace DomainModel
{
    public class VMTestListItem
    {
        public VMTestListItem()
        {
            IsDelete = true;

        }

        [JQColumnAttribute( "VMPanel_TestName", true, false, true, EditTypeEnum.Text)]
        public string TestName { get; set; }

        [JQColumnAttribute("TesstId", true, false, true, EditTypeEnum.Text)]
        public int TesstId { get; set; }

        [JQColumnAttribute("Cost", true, false, true, EditTypeEnum.Text)]
        public decimal Cost { get; set; }

        [JQColumnAttribute("IsDelete", true, false, true, EditTypeEnum.Checkbox)]
        public bool IsDelete { get; set; }

        [JQColumnAttribute("VMPanel_Section", true, false, true, EditTypeEnum.Text)]
        public string TestSectionName { get; set; }
    }
}
