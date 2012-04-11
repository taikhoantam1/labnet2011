using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomainModel;
using System.Web.Mvc;

namespace LabnetClient.Models
{
    public class InstrumentResultViewModel : BaseModel
    {
        //public VMInstrumentResult InstrumentResult { get; set; }
        public DateTime? ReceivedDate { get; set; }

        public string OrderNumber { get; set; }

        public int InstrumentId { get; set; }

        public JQGridModel JQGrid { get; set; }

        public SelectList SelectListInstrument { get; set; }

        public InstrumentResultViewModel(List<VMInstrumentResultList> lstInstrumentResult, List<VMInstrument> listInstrument, DateTime? receivedDate)
        {
            ReceivedDate = receivedDate;
            JQGrid = new JQGridModel(typeof(VMInstrumentResultList), true, lstInstrumentResult, "");

            VMInstrument ins = new VMInstrument();
            ins.Id = -1;
            ins.Name = " ";
            listInstrument.Insert(0, ins);
            SelectListInstrument = new SelectList(listInstrument, "Id", "Name");
        }
    }
}