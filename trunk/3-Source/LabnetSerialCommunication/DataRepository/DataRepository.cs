using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataRepository
{
    public class Repository : IDataRepository
    {
        private LabnetCommunicationEntities myDb;
        public LabnetCommunicationEntities Context { get { return myDb; } }

        public Repository()
        {
            myDb = new LabnetCommunicationEntities();
        }

        public List<Instrument> GetInstruments()
        {
            List<Instrument> lstInstruments = (from _ins in myDb.Instruments where _ins.IsActive select _ins).ToList();
            return lstInstruments;
        }

        public int GetTestIdByInstrumentAndTestCode(int instrumentId, string testCode)
        {
            List<GetTestIdByInstrumentAndInstrumentTestCode_Result> lst  = myDb.GetTestIdByInstrumentAndInstrumentTestCode(instrumentId, testCode).ToList();
            if(lst.Count> 0)
                return lst[0].TestId;

            return 0;
        }

        public void InstrumentResult(string orderNumber, int testId, string value, string instrumentPatient, int instrumentId)
        {
            myDb.InstrumentResult(orderNumber, testId, value, instrumentPatient, instrumentId);
        }

        public List<InstrumentResult> GetAllValidInstrumentResultByCondition(DateTime? receivedDate, string orderNumber, int? instrumentId)
        {
            List<InstrumentResult> lstInstrumentResults = (from _insResult in myDb.InstrumentResults
                                                           where _insResult.Flag == false
                                                             && _insResult.ReceivedDate == receivedDate
                                                             && (orderNumber == null || _insResult.OrderNumber == orderNumber)
                                                             && (instrumentId == null || _insResult.InstrumentId == instrumentId)
                                                           select _insResult).ToList();
            return lstInstrumentResults;
        }

        public void InsertToResult(int? orderNumber, DateTime? receivedDate, int? testId, string value, int? instrumentResultId)
        {
            myDb.Result(orderNumber, receivedDate, testId, value, instrumentResultId);
        }
    }
}
