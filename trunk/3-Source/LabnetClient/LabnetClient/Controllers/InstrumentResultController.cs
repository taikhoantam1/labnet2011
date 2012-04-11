using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LabnetClient.Models;
using DataRepository;
using DomainModel;
using AutoMapper;
using DomainModel.Constant;
using System.Transactions;

namespace LabnetClient.Controllers
{
    public class InstrumentResultController : BaseController
    {
        //
        // GET: /InstrumentResult/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PopUp()
        {
            //do some work...

            //I want this returned in a popup window/modal dialog of some sort...
            return View("ChangeSID");
        }

        //
        // GET: /InstrumentResult/Details/5

        public ActionResult Details()
        {
            List<SearchInstrumentResult_Result> lstInstrumentResult = Repository.InstrumentResultSearch(DateTime.Now.Date, null, null);
            List<VMInstrument> lstInstrument = Mapper.Map<List<Instrument>, List<VMInstrument>>(Repository.GetInstruments());
            List<VMInstrumentResultList> ObjInstrumentResult = new List<VMInstrumentResultList>();
            foreach (SearchInstrumentResult_Result item in lstInstrumentResult)
            {
                VMInstrumentResultList obj = new VMInstrumentResultList();
                obj.OrderNumber = item.OrderNumber;
                obj.TestId = item.TestId;
                obj.TestName = item.TestName;
                obj.Result = item.Result;
                obj.InstrumentName = item.InstrumentName;
                obj.Flag = item.Flag;
                obj.ReceivedDate = item.ReceivedDate.ToString("d");
                ObjInstrumentResult.Add(obj);
            }
            InstrumentResultViewModel Model = new InstrumentResultViewModel(ObjInstrumentResult, lstInstrument, DateTime.Now.Date);
            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem
            {
                Text = "",
                Value = ""
            });
            foreach(VMInstrument item in lstInstrument)
            {
                items.Add(new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Id.ToString()
                });
            }
            //Model.SelectListInstrument= items;
            return View(Model);
        }

        //
        // GET: /InstrumentResult/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /InstrumentResult/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /InstrumentResult/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /InstrumentResult/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /InstrumentResult/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /InstrumentResult/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

       
        [HttpPost]
        public ActionResult SearchInstrumentResult(string ReceivedDate, string OrderNumber, int InstrumentId)
        {

            DateTime? receivedDate = null;
            if (!string.IsNullOrEmpty(ReceivedDate))
                receivedDate = Convert.ToDateTime(ReceivedDate);

            List<SearchInstrumentResult_Result> lstInstrumentResult = new List<SearchInstrumentResult_Result>();
            if (string.IsNullOrEmpty(OrderNumber) && InstrumentId == -1)
            {
                lstInstrumentResult = Repository.InstrumentResultSearch(receivedDate, null, null);
            }
            if (!string.IsNullOrEmpty(OrderNumber) && InstrumentId == -1)
            {
                lstInstrumentResult = Repository.InstrumentResultSearch(receivedDate, OrderNumber, null);
            }
            if (string.IsNullOrEmpty(OrderNumber) && InstrumentId != -1)
            {
                lstInstrumentResult = Repository.InstrumentResultSearch(receivedDate, null, InstrumentId);
            }
            if (!string.IsNullOrEmpty(OrderNumber) && InstrumentId != -1)
            {
                lstInstrumentResult = Repository.InstrumentResultSearch(receivedDate, OrderNumber, InstrumentId);
            }

            List<VMInstrument> lstInstrument = Mapper.Map<List<Instrument>, List<VMInstrument>>(Repository.GetInstruments());
            List<VMInstrumentResultList> ObjInstrumentResult = new List<VMInstrumentResultList>();
            foreach (SearchInstrumentResult_Result item in lstInstrumentResult)
            {
                VMInstrumentResultList obj = new VMInstrumentResultList();
                obj.OrderNumber = item.OrderNumber;
                obj.TestId = item.TestId;
                obj.TestName = item.TestName;
                obj.Result = item.Result;
                obj.InstrumentName = item.InstrumentName;
                obj.Flag = item.Flag;
                obj.ReceivedDate = item.ReceivedDate.ToString("d");
                ObjInstrumentResult.Add(obj);
            }

            JQGridModel grid = new JQGridModel(typeof(VMInstrumentResultList), false, ObjInstrumentResult, "");
            return View("DataTable", grid);
        }

        public string SearchInstrument(List<VMInstrumentResultList> Rows)
        {
            Session[SessionProperties.SessionInstrumentResultList] = Rows;
            return "Success";
        }

        [HttpPost]
        public ActionResult UpdateToResult(string ReceivedDate, string OrderNumber, int InstrumentId)
        {
            InstrumentResultViewModel Model = new InstrumentResultViewModel(new List<VMInstrumentResultList>(), new List<VMInstrument>(), DateTime.Now.Date);
            DateTime? receivedDate = null;
            if (!string.IsNullOrEmpty(ReceivedDate))
                receivedDate = Convert.ToDateTime(ReceivedDate);

            List<SearchInstrumentResult_Result> lstInstrumentResult = new List<SearchInstrumentResult_Result>();
            List<InstrumentResult> lstInstrumentResults = new List<InstrumentResult>();
            if (string.IsNullOrEmpty(OrderNumber) && InstrumentId == -1)
            {
                lstInstrumentResults = Repository.GetAllValidInstrumentResultByCondition(receivedDate, null, null);
            }
            if (!string.IsNullOrEmpty(OrderNumber) && InstrumentId == -1)
            {
                lstInstrumentResults = Repository.GetAllValidInstrumentResultByCondition(receivedDate, OrderNumber, null);
            }
            if (string.IsNullOrEmpty(OrderNumber) && InstrumentId != -1)
            {
                lstInstrumentResults = Repository.GetAllValidInstrumentResultByCondition(receivedDate, null, InstrumentId);
            }
            if (!string.IsNullOrEmpty(OrderNumber) && InstrumentId != -1)
            {
                lstInstrumentResults = Repository.GetAllValidInstrumentResultByCondition(receivedDate, OrderNumber, InstrumentId);
            }

            if (lstInstrumentResults.Count > 0)
            {
                foreach (InstrumentResult ins in lstInstrumentResults)
                {
                    int orderNumber;
                    if (int.TryParse(ins.OrderNumber,out orderNumber))
                    {
                        Repository.InsertToResult(orderNumber, receivedDate, ins.TestId, ins.Result, ins.Id);
                    }
                }
            }

            List<VMInstrumentResultList> ObjInstrumentResult = GetInstrumentResultListObject(ReceivedDate, OrderNumber, InstrumentId);
            JQGridModel grid = new JQGridModel(typeof(VMInstrumentResultList), false, ObjInstrumentResult, "");
            return View("DataTable", grid);
        }

        [HttpPost]
        public string ChangeSID(string ReceivedDate, string OldOrderNumber, string NewOrderNumber, int InstrumentId)
        {
            bool updateSuccess = true;
            string errorMessage="";
            DateTime? receivedDate = null;
            if (!string.IsNullOrEmpty(ReceivedDate))
                receivedDate = Convert.ToDateTime(ReceivedDate);
            else
            {
                updateSuccess = false;
                errorMessage="Vui lòng chọn ngày nhận";
            }

            int oldSID=0;
            int newSID=0;
            try
            {
                oldSID = int.Parse(OldOrderNumber);
                newSID = int.Parse(NewOrderNumber);
            }
            catch (Exception ex)
            {
                updateSuccess = false;
                errorMessage = "Nhập SID là số tự nhiên";
            }
            List<VMInstrumentResultList> ObjInstrumentResult = new List<VMInstrumentResultList>();
            if (updateSuccess)
            {
                try
                {
                    if (InstrumentId == -1)
                    {
                        Repository.UpdateSID(receivedDate, oldSID, newSID, null);
                    }
                    else
                    {
                        Repository.UpdateSID(receivedDate, oldSID, newSID, InstrumentId);
                    }
                }
                catch (Exception ex)
                {

                    updateSuccess = false;
                    errorMessage = ex.Message;
                }

            
            }

            // Return "Success" if change SID success
            // Return message error if update faile
            if(updateSuccess)
                return "Success";

            return errorMessage;
        }

        public List<VMInstrumentResultList> GetInstrumentResultListObject(string ReceivedDate, string OrderNumber, int InstrumentId)
        {
            DateTime? receivedDate = null;
            if (!string.IsNullOrEmpty(ReceivedDate))
                receivedDate = Convert.ToDateTime(ReceivedDate);

            List<SearchInstrumentResult_Result> lstInstrumentResult = new List<SearchInstrumentResult_Result>();
            if (string.IsNullOrEmpty(OrderNumber) && InstrumentId == -1)
            {
                lstInstrumentResult = Repository.InstrumentResultSearch(receivedDate, null, null);
            }
            if (!string.IsNullOrEmpty(OrderNumber) && InstrumentId == -1)
            {
                lstInstrumentResult = Repository.InstrumentResultSearch(receivedDate, OrderNumber, null);
            }
            if (string.IsNullOrEmpty(OrderNumber) && InstrumentId != -1)
            {
                lstInstrumentResult = Repository.InstrumentResultSearch(receivedDate, null, InstrumentId);
            }
            if (!string.IsNullOrEmpty(OrderNumber) && InstrumentId != -1)
            {
                lstInstrumentResult = Repository.InstrumentResultSearch(receivedDate, OrderNumber, InstrumentId);
            }

            List<VMInstrumentResultList> ObjInstrumentResult = new List<VMInstrumentResultList>();
            foreach (SearchInstrumentResult_Result item in lstInstrumentResult)
            {
                VMInstrumentResultList obj = new VMInstrumentResultList();
                obj.OrderNumber = item.OrderNumber;
                obj.TestId = item.TestId;
                obj.TestName = item.TestName;
                obj.Result = item.Result;
                obj.InstrumentName = item.InstrumentName;
                obj.Flag = item.Flag;
                obj.ReceivedDate = item.ReceivedDate.ToString("d");
                ObjInstrumentResult.Add(obj);
            }

            return ObjInstrumentResult;
        }
    }
}
    