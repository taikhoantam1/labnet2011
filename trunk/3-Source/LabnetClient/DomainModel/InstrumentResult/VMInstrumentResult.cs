using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.JQGrid;

namespace DomainModel
{
    public class VMInstrumentResult
    {
        public VMInstrumentResult()
        {
            ReceivedDate = DateTime.Now.Date;
        }

        public DateTime? ReceivedDate { get; set; }

        public string OrderNumber { get; set; }

        public int InstrumentId { get; set; }

    }

    
}
