using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.JQGrid;

namespace DomainModel
{
    public class VMInstrumentResultList
    {
        [JQColumnAttribute("VMInstrumentResult_OrderNumber", true, false, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public string OrderNumber { get; set; }

        [JQColumnAttribute("VMInstrumentResult_TestName", true, false, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public string TestName { get; set; }

        [JQColumnAttribute("VMInstrumentResult_TestId", true, true, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public int TestId { get; set; }

        [JQColumnAttribute("VMInstrumentResult_Result", true, false, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public string Result { get; set; }

        [JQColumnAttribute("VMInstrumentResult_InstrumentName", true, false, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public string InstrumentName { get; set; }

        [JQColumnAttribute("VMInstrumentResult_Status", true, false, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public string Flag { get; set; }

        [JQColumnAttribute("VMInstrumentResult_ReceivedDate", true, false, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public string ReceivedDate { get; set; }
    }
}
