using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LabnetClient.Models;
using DomainModel;
using LabnetClient.Constant;
using LibraryFuntion;
using DataRepository;
using AutoMapper;

namespace LabnetClient.Controllers
{
    public class PanelController : BaseController
    {
        //
        // GET: /Panel/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Panel/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Panel/Create

        public ActionResult Create()
        {
            PanelViewModel model = new PanelViewModel();
            model.PanelTestList = new List<VMTestListItem>();
            model.Panel.IsActive = true;
            model.ViewMode = ViewMode.Create;
            model.Autocomplete.JsonData = Repository.GetTestByNameForPanel("", SearchTypeEnum.Contains.ToString().ToUpper()).ToJson();
            return View("Create", model);
        } 

        //
        // POST: /Panel/Create

        [HttpPost]
        public ActionResult Create(PanelViewModel model)
        {
            if (!Repository.IsValidPanel(model.Panel.Name))
            {
                ModelState.AddModelError("Panel name already exists", Resources.PanelStrings.PanelCreate_NameError);
            }

            if (!ModelState.IsValid)
            {
                if (model.PanelTestList == null)
                {
                    model.PanelTestList = new List<VMTestListItem>();
                }
                model.Autocomplete.JsonData = Repository.GetTestByNameForPanel("", SearchTypeEnum.Contains.ToString().ToUpper()).ToJson();
                return View("Create", model);
            }

            Panel panel = Mapper.Map<VMPanel, Panel>(model.Panel);
            Repository.PanelInsert(panel);

            foreach (VMTestListItem item in model.PanelTestList)
            {
                if (item.IsDelete == false)
                {
                    PanelItem panelItem = new PanelItem();
                    panelItem.PanelId = panel.Id;
                    panelItem.TestId = item.TesstId;
                    panelItem.TestName = item.TestName;
                    panelItem.IsActive = true;
                    panelItem.LastUpdated = DateTime.Now;

                    Repository.PanelItemInsert(panelItem);
                }
            }

            //model = new PanelViewModel();
            //model.Panel = new VMPanel();
            
            //model.Panel.Name = null;
            //model.Panel.Description = null;
            //model.Panel.IsActive = true;
            //model.PanelTestList = new List<VMTestListItem>();
            //model.Autocomplete.JsonData = Repository.GetTestByNameForPanel("", SearchTypeEnum.Contains.ToString().ToUpper()).ToJson();
            return RedirectToAction("Create");
        }
        
        //
        // GET: /Panel/Edit/5
 
        public ActionResult Edit(int id)
        {
            PanelViewModel model = new PanelViewModel();
            model.PanelTestList = new List<VMTestListItem>();

            model.Panel = Mapper.Map<Panel, VMPanel>(Repository.GetPanel(id));
            model.PanelTestList = Repository.GetPanelTest(id);

            model.Autocomplete.JsonData = Repository.GetTestByNameForPanel("", SearchTypeEnum.Contains.ToString().ToUpper()).ToJson();
            model.ViewMode = ViewMode.Edit;

            return View("Create", model);
        }

        //
        // POST: /Panel/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, PanelViewModel model)
        {
            Repository.PanelUpdate(id, Mapper.Map<VMPanel, Panel>(model.Panel));

            foreach (VMTestListItem item in model.PanelTestList)
            {
                bool isExist = Repository.IsPanelItemExist(item.TesstId);
                if (isExist)
                {
                    PanelItem panelItem = Repository.GetPanelItemByTestIdAndPanelId(item.TesstId, id);
                    
                    if (item.IsDelete == true)
                    {
                        panelItem.IsActive = false;
                    }
                    Repository.PanelItemUpdate(panelItem.Id, panelItem);
                }
                else
                {
                    if (item.IsDelete == false)
                    {
                        PanelItem panelItem = new PanelItem();
                        panelItem.TestId = item.TesstId;
                        panelItem.TestName = item.TestName;
                        panelItem.PanelId = id;
                        panelItem.IsActive = true;
                        panelItem.LastUpdated = DateTime.Now;

                        Repository.PanelItemInsert(panelItem);
                    }
                }
            }
            model.PanelTestList = model.PanelTestList.Where(p => p.IsDelete == false).ToList();

            return RedirectToAction("Create");
            
        }

        //
        // GET: /Panel/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Panel/Delete/5

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

        public ActionResult Search()
        {
            PanelSearchViewModel model = new PanelSearchViewModel();
            model.PanelSearch.ListSearchResult = new List<PanelSearchObject>();
            return View("Search", model);
        }

        [HttpPost]
        public ActionResult Search(PanelSearchViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.PanelSearch.ListSearchResult = new List<PanelSearchObject>();
                return View("Search", model);
            }

            List<Panel> lstPanel = Repository.GetPanelByName(model.PanelSearch.Name);
            model.PanelSearch.ListSearchResult = new List<PanelSearchObject>();
            foreach (Panel panel in lstPanel)
            {
                PanelSearchObject obj = new PanelSearchObject();
                obj.Id = panel.Id;
                obj.PanelName = panel.Name;

                model.PanelSearch.ListSearchResult.Add(obj);
            }

            return View("Search", model);
        }
    }
}
