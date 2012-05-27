using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LabnetClient.Models;
using DomainModel;
using DomainModel.Constant;
using LibraryFuntion;
using DataRepository;
using AutoMapper;


namespace LabnetClient.Controllers
{
    public class DoiTacController : BaseController
    {
        //
        // GET: /Partner/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult FindTestNames(string searchText, string searchType)
        {
            var result = Repository.GetTestByName(searchText, searchType);
            return Json(result);
        }

        // GET: /Partner/Create

        public ActionResult Create()
        {
            PartnerViewModel model = new PartnerViewModel(new VMPartner(),new List<VMTestListItem>(), new List<VMTestSectionListItem>());
            model.ViewMode = ViewMode.Create;
            model.Autocomplete.JsonData = Repository.GetTestHaveCostNotDependenceTestSection().ToJson();
            model.TestSectionAutocomplete.JsonData = Repository.GetTestSectionByNameForPanel("", SearchTypeEnum.Contains.ToString().ToUpper()).ToJson();
            return View("Details", model);
        }

        //
        // POST: /Partner/Create
        [HttpPost]
        public ActionResult Create(PartnerViewModel model)
        {
            List<VMTestListItem> Rows = null;
            List<VMTestSectionListItem> TestSectionRows = null;

            if (Session[SessionProperties.SessionPartnerTestList] != null)
            {
                Rows = (List<VMTestListItem>)Session[SessionProperties.SessionPartnerTestList];
            }
            else
            {
                Rows = new List<VMTestListItem>();
            }

            if (Session[SessionProperties.SessionPartnerTestSectionList] != null)
            {
                TestSectionRows = (List<VMTestSectionListItem>)Session[SessionProperties.SessionPartnerTestSectionList];
            }
            else
            {
                TestSectionRows = new List<VMTestSectionListItem>();
            }

            if (!Repository.IsValidPartner(model.Partner.Name))
            {
                ModelState.AddModelError("Partner name already exists", Resources.PartnerStrings.PartnerInsert_NameError);
            }

            if (!ModelState.IsValid)
            {
                model.TestSectionAutocomplete = new AutocompleteModel("Partner.TestSectionName");
                model.Autocomplete.JsonData = Repository.GetTestHaveCostNotDependenceTestSection().ToJson();
                model.TestSectionAutocomplete.JsonData = Repository.GetTestSectionByNameForPanel("", SearchTypeEnum.Contains.ToString().ToUpper()).ToJson();
                model.JQGrid = new JQGridModel(typeof(VMTestListItem), true, Rows, "/Partner/SavePartnerTest");
                model.JQGridTestSection = new JQGridModel(typeof(VMTestSectionListItem), true, TestSectionRows, "/Partner/SavePartnerTestSection");
                return View("Details", model);
            }
            else
            {
                
                Partner partner = Mapper.Map<VMPartner, Partner>(model.Partner);
                Repository.PartnerInsert(partner);

                foreach (VMTestListItem item in Rows)
                {
                    if (item.IsEnable )
                    {
                        PartnerCost cost = new PartnerCost();
                        cost.Cost = item.Cost;
                        cost.TestId = item.TestId;
                        cost.PartnerId = partner.Id;
                        cost.IsActive = true;
                        cost.LastUpdated = DateTime.Now;
                        Repository.PartnerCostInsert(cost);
                    }
                }

                foreach (VMTestSectionListItem item in TestSectionRows)
                {
                    if (item.IsEnableTestSection)
                    {
                        TestSectionCommission ts = new TestSectionCommission();
                        
                        decimal tmpCost;
                        if (decimal.TryParse(item.TestSectionCost.ToString(), out tmpCost))
                            ts.Cost = tmpCost;

                        ts.PartnerId = partner.Id;
                        ts.TestSectionId = item.TestSectionId;
                        ts.IsActive = true;

                        Repository.TestSectionCommissionInsert(ts);
                    }
                }
            }
            return RedirectToAction("Search");
            
        }
        

        [HttpPost]
        public string SavePartnerTest(List<VMTestListItem> Rows)
        {
            Session[SessionProperties.SessionPartnerTestList] = Rows;
          
            return "success";
        }

        [HttpPost]
        public string SavePartnerTestSection(List<VMTestSectionListItem> Rows)
        {
            Session[SessionProperties.SessionPartnerTestSectionList] = Rows;

            return "success";
        }

        //
        // GET: /Partner/Edit/5

        public ActionResult Edit(int id)
        {
            VMPartner partner = Mapper.Map<Partner, VMPartner>(Repository.GetPartnerById(id));
            List<VMTestListItem> partnerTestList = Repository.GetPartnerTest(id);
            List<VMTestSectionListItem> partnerTestSectionList = Repository.GetPartnerTestSection(id);
            PartnerViewModel model = new PartnerViewModel(partner, partnerTestList, partnerTestSectionList);
            model.ViewMode = ViewMode.Edit;
            model.Autocomplete.JsonData = Repository.GetTestHaveCostNotDependenceTestSection().ToJson();
            model.TestSectionAutocomplete.JsonData = Repository.GetTestSectionByNameForPanel("", SearchTypeEnum.Contains.ToString().ToUpper()).ToJson();

            return View("Details", model);
        }

        //
        // POST: /Partner/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, PartnerViewModel model)
        {
            //model.PartnerTestList = model.PartnerTestList.Where(p => p.IsDelete == false).ToList();
            Repository.PartnerUpdate(id, Mapper.Map<VMPartner, Partner>(model.Partner));
            if (Session[SessionProperties.SessionPartnerTestList] != null)
            {
                List<VMTestListItem> Rows = (List<VMTestListItem>)Session[SessionProperties.SessionPartnerTestList];
                foreach (VMTestListItem item in Rows)
                {
                    bool isExist = Repository.IsPartnerCostExist(item.TestId, id);
                    if (isExist)
                    {
                        PartnerCost partnerCost = Repository.GetPartnerCostByTestId(item.TestId, id);
                        partnerCost.Cost = item.Cost;
                        if (item.IsEnable== false)
                        {
                            partnerCost.IsActive = false;
                        }
                        Repository.PartnerCostUpdate(partnerCost.Id, partnerCost);
                    }
                    else
                    {
                        if (item.IsEnable)
                        {
                            PartnerCost partnerCost = new PartnerCost();
                            partnerCost.TestId = item.TestId;
                            partnerCost.PartnerId = id;
                            partnerCost.IsActive = true;
                            partnerCost.Cost = item.Cost;
                            partnerCost.LastUpdated = DateTime.Now;
                            Repository.PartnerCostInsert(partnerCost);
                        }
                    }
                }
            }
            return RedirectToAction("Create");

        }


        public ActionResult Search()
        {
            PartnerSearchViewModel model = new PartnerSearchViewModel();
            model.Autocomplete.JsonData = Repository.GetPartnerNameByName("", SearchTypeEnum.Contains.ToString().ToUpper()).ToJson();
            return View("Search", model);
        }


        [HttpPost]
        public ActionResult SearchPartner(string filterText)
        {
            List<Partner> lstPartner = Repository.GetPartnerByName(filterText);
            List<VMPartnerSearch> ListSearchResult = new List<VMPartnerSearch>();
            foreach (Partner partner in lstPartner)
            {
                VMPartnerSearch obj = new VMPartnerSearch();
                obj.Id = partner.Id;
                obj.PartnerName = partner.Name;
                obj.Phone = partner.Phone;
                obj.Email = partner.Email;
                obj.Address = partner.Address;
                ListSearchResult.Add(obj);
            }
            JQGridModel grid = new JQGridModel(typeof(VMPartnerSearch), false, ListSearchResult, "");
            return View("DataTable", grid);
        }
        //
        // GET: /Partner/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Partner/Delete/5

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
        public string CreateConnectionCode(int clientLabId)
        {
            AjaxResultModel result = new AjaxResultModel();
            try
            {
                int labId = (int)Session["LabId"];
                string connectionCode = Repository.CreateLabConnectionCode(clientLabId, labId);
                result.IsSuccess = true;
                result.ResponseData = connectionCode;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessage = ex.Message;
            }
            return result.ToJson();
        }
    }
}
