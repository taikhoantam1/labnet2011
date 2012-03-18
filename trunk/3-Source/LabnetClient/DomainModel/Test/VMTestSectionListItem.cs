using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.JQGrid;

namespace DomainModel
{
    public class VMTestSectionListItem
    {
        public VMTestSectionListItem()
        {
            IsEnableTestSection = true;
        }

        [JQColumnAttribute("VMPanel_TestSectionName", true, false, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public string TestSectionName { get; set; }

        [JQColumnAttribute("VMPanel_TestSectionId", true, true, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public int TestSectionId { get; set; }

        [JQColumnAttribute("VMPanel_TestSectionCost", true, false, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public decimal? TestSectionCost { get; set; }

        [JQColumnAttribute("VMPanel_IsEnableTestSection", true, false, true, EditTypeEnum.Checkbox, FormatterEnum.Checkbox)]
        public bool IsEnableTestSection { get; set; }
    }
}
