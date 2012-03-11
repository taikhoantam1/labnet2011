using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.JQGrid;

namespace DomainModel
{
    public class VMTestResult
    {
        [JQColumnAttribute("VMPatientTestResult_TestId", true, true, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public int TestId { get; set; }

        [JQColumnAttribute("VMPatientTestResult_Index", true, true, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public int Index { get; set; }

        [JQColumnAttribute("VMPatientTestResult_Name", true, false, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public string Name { get; set; }

        [JQColumnAttribute("VMPatientTestResult_Result", true, false, true, EditTypeEnum.Text, FormatterEnum.Text)]
        public string  Result { get; set; }

        [JQColumnAttribute("VMPatientTestResult_Range", false, false, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public string Range { get; set; }

        [JQColumnAttribute("VMPatientTestResult_Unit", true, false, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public string Unit { get; set; }

        [JQColumnAttribute("VMPatientTestResult_Status", true, false, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public string StatusString { get; set; }

        [JQColumnAttribute("",false, true, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public int Status { get; set; }

        [JQColumnAttribute("", false, true, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public int AnalysisId { get; set; }

        [JQColumnAttribute("", false, true, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public int? ResultId { get; set; }

        public string MoTa { get; set; }
    }
}
