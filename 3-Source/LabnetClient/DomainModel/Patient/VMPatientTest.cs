using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.JQGrid;

namespace DomainModel
{
   public class VMPatientTest
   {
        [JQColumnAttribute("VMPatientTest_TestId", true, true, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public int TestId { get; set; }

        [JQColumnAttribute("VMPatientTest_TestName", true, false, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public string TestName { get; set; }

        [JQColumnAttribute("VMPatientTest_Cost", true, false, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public decimal Cost { get; set; }
       
        [JQColumnAttribute("VMPatientTest_Section", true, false, false, EditTypeEnum.Text, FormatterEnum.Text)] 
        public string Section { get; set; }

        [JQColumnAttribute("VMPatientTest_IsEnable", true, false,true, EditTypeEnum.Checkbox, FormatterEnum.Checkbox)]
        public bool IsEnable { get; set; }

        [JQColumnAttribute("", true, true, true, EditTypeEnum.Checkbox, FormatterEnum.Checkbox)]
        public bool IsTestFromTestSection { get; set; }

        [JQColumnAttribute("", true, true, true, EditTypeEnum.Text, FormatterEnum.Text)]
        public int AnalysisId { get; set; }

    }
}
