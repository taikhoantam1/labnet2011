using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Properties;
using DomainModel.JQGrid;

namespace DomainModel
{
    public class VMTestListItem
    {
        public VMTestListItem()
        {
            IsEnable = true;

        }

        [JQColumnAttribute("VMPanel_TestName", true, false,false, EditTypeEnum.Text, FormatterEnum.Text)]
        public string TestName { get; set; }

        [JQColumnAttribute("VMPanel_TestId", true, true, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public int TestId { get; set; }

        [JQColumnAttribute("VMPanel_Cost", true, false, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public decimal Cost { get; set; }

        [JQColumnAttribute("VMPanel_Section", true, false, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public string TestSectionName { get; set; }

        [JQColumnAttribute("VMPanel_IsEnable", true, false, true, EditTypeEnum.Checkbox, FormatterEnum.Checkbox)]
        public bool IsEnable { get; set; }

    }
}
