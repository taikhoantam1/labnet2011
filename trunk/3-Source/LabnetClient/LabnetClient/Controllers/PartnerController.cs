using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainModel;
using DataRepository;
using LabnetClient.Models;
using AutoMapper;
using LabnetClient.App_Code;
using LabnetClient.Constant;
using LibraryFuntion;

namespace LabnetClient.Controllers
{
    public class PartnerController : BaseController
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
            PartnerViewModel model = new PartnerViewModel();
            model.PartnerTestList = new List<VMTestListItem>();
            model.Partner.IsActive = true;
            model.ViewMode = ViewMode.Create;
            model.Autocomplete.JsonData = Repository.GetTestByName("", SearchTypeEnum.Contains.ToString().ToUpper()).ToJson();
            return View("Details", model);
        }

        //
        // POST: /Partner/Create

        [HttpPost]
        public ActionResult Create(PartnerViewModel model)
        {
            if (model.PartnerTestList == null)
            {
                ModelState.AddModelError("Partner Cost not null", Resources.PartnerStrings.PartnerInsert_PartnerCostError);
            }

            if (!Repository.IsValidPartner(model.Partner.Name))
            {
                ModelState.AddModelError("Partner name already exists", Resources.PartnerStrings.PartnerInsert_NameError);
            }

            if (!ModelState.IsValid)
            {
                if (model.PartnerTestList == null)
                {
                    model.PartnerTestList = new List<VMTestListItem>();
                }
                model.Autocomplete.JsonData = Repository.GetTestByName("", SearchTypeEnum.Contains.ToString().ToUpper()).ToJson();
                return View("Details", model);
            }
            else
            {
                Partner partner = Mapper.Map<VMPartner, Partner>(model.Partner);
                Repository.PartnerInsert(partner);

                foreach (VMTestListItem item in model.PartnerTestList)
                {
                    if (item.IsDelete == false)
                    {
                        PartnerCost cost = new PartnerCost();
                        cost.Cost = item.Cost;
                        cost.TestId = item.TesstId;
                        cost.PartnerId = partner.Id;
                        cost.IsActive = true;
                        cost.LastUpdated = DateTime.Now;
                        Repository.PartnerCostInsert(cost);
                    }
                }
            }
            return RedirectToAction("Create");
            
        }

        //
        // GET: /Partner/Edit/5

        public ActionResult Edit(int id)
        {
            PartnerViewModel model = new PartnerViewModel();
            //model.Partner.PartnerCostDetails = new List<VMPartnerCost>();
            model.ViewMode = ViewMode.Edit;
            model.Autocomplete.JsonData = Repository.GetTestByName("", SearchTypeEnum.Contains.ToString().ToUpper()).ToJson();

            model.Partner = Mapper.Map<Partner, VMPartner>(Repository.GetPartnerById(id));
            model.PartnerTestList = Repository.GetPartnerTest(id);
            return View("Details", model);
        }

        //
        // POST: /Partner/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, PartnerViewModel model)
        {
            //model.PartnerTestList = model.PartnerTestList.Where(p => p.IsDelete == false).ToList();
            Repository.PartnerUpdate(id, Mapper.Map<VMPartner, Partner>(model.Partner));

            foreach (VMTestListItem item in model.PartnerTestList)
            {
                bool isExist = Repository.IsPartnerCostExist(item.TesstId, id);
                if (isExist)
                {
                    PartnerCost partnerCost = Repository.GetPartnerCostByTestId(item.TesstId, id);
                    partnerCost.Cost = item.Cost;
                    if (item.IsDelete == true)
                    {
                        partnerCost.IsActive = false;
                    }
                    Repository.PartnerCostUpdate(partnerCost.Id, partnerCost);
                }
                else
                {
                    if (item.IsDelete == false)
                    {
                        PartnerCost partnerCost = new PartnerCost();
                        partnerCost.TestId = item.TesstId;
                        partnerCost.PartnerId = id;
                        partnerCost.IsActive = true;
                        partnerCost.Cost = item.Cost;
                        partnerCost.LastUpdated = DateTime.Now;
                        Repository.PartnerCostInsert(partnerCost);
                    }
                }
            }
            model.PartnerTestList = model.PartnerTestList.Where(p => p.IsDelete == false).ToList();
            return RedirectToAction("Create");

        }

        public ActionResult Search()
        {
            PartnerSearchViewModel model = new PartnerSearchViewModel();
            model.PartnerSearch.ListSearchResult = new List<PartnerSearchObject>();
            return View("Search", model);
        }


        [HttpPost]
        public ActionResult Search(PartnerSearchViewModel model)
        {
            List<Partner> lstPartner = Repository.GetPartnerByName(model.PartnerSearch.Name);
            model.PartnerSearch.ListSearchResult = new List<PartnerSearchObject>();
            foreach (Partner partner in lstPartner)
            {
                PartnerSearchObject obj = new PartnerSearchObject();
                obj.Id = partner.Id;
                obj.PartnerName = partner.Name;

                model.PartnerSearch.ListSearchResult.Add(obj);
            }
            return View("Search", model);
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
    }
}
