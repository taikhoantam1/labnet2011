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
            PanelSearchViewModel model = new PanelSearchViewModel(null);
            return View("Search", model);
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
            PanelViewModel model = new PanelViewModel(new VMPanel(),null);
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

            if (Session[SessionProperties.SessionPanelTestList] != null)
            {
                List<VMTestListItem> Rows = (List<VMTestListItem>)Session[SessionProperties.SessionPanelTestList];
                foreach (VMTestListItem item in Rows)
                {
                    PanelItem panelItem = Repository.GetPanelItemByTestIdAndPanelId(item.TestId, panel.Id);
                    if (item.IsDelete == false)
                    {
                        panelItem = new PanelItem();
                        panelItem.TestId = item.TestId;
                        panelItem.TestName = item.TestName;
                        panelItem.PanelId = panel.Id;
                        panelItem.IsActive = true;
                        panelItem.LastUpdated = DateTime.Now;
                        Repository.PanelItemInsert(panelItem);
                    }
                }
            }
            return RedirectToAction("Index");
        }
        
        //
        // GET: /Panel/Edit/5
 
        public ActionResult Edit(int id)
        {
            Session[SessionProperties.SessionPanelTestList] = id;
            List<VMTestListItem> testList= Repository.GetPanelTest(id);
            VMPanel panel= Mapper.Map<Panel, VMPanel>(Repository.GetPanel(id));
            PanelViewModel model = new PanelViewModel(panel,testList);
            model.Autocomplete.JsonData = Repository.GetTestByNameForPanel("", SearchTypeEnum.Contains.ToString().ToUpper()).ToJson();
            model.ViewMode = ViewMode.Edit;

            return View("Create",model);
        }

        //
        // POST: /Panel/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, PanelViewModel model)
        {
            Repository.PanelUpdate(id, Mapper.Map<VMPanel, Panel>(model.Panel));
            if (Session[SessionProperties.SessionPanelTestList] != null)
            {
                List<VMTestListItem> Rows = (List<VMTestListItem>)Session[SessionProperties.SessionPanelTestList];
                foreach (VMTestListItem item in Rows)
                {
                    PanelItem panelItem = Repository.GetPanelItemByTestIdAndPanelId(item.TestId, id);

                    if (panelItem != null)
                    {
                        if (item.IsDelete == true)
                        {
                            panelItem.IsActive = false;
                        }
                        else
                        {
                            panelItem.IsActive = true;
                        }
                        Repository.PanelItemUpdate(panelItem.Id, panelItem);
                    }
                    else
                    {
                        if (item.IsDelete == false)
                        {
                            panelItem = new PanelItem();
                            panelItem.TestId = item.TestId;
                            panelItem.TestName = item.TestName;
                            panelItem.PanelId = id;
                            panelItem.IsActive = true;
                            panelItem.LastUpdated = DateTime.Now;
                            Repository.PanelItemInsert(panelItem);
                        }
                    }
                }
            }
            return RedirectToAction("Index");

        }
        
        [HttpPost]
        public string SavePanelTest(List<VMTestListItem> Rows)
        {
            Session[SessionProperties.SessionPanelTestList] = Rows;
            return "success";
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


        [HttpPost]
        public ActionResult Search(string filterText)
        {
            List<VMPanel> lstPanel = Mapper.Map<List<Panel>, List<VMPanel>>(Repository.GetPanelByName(filterText));
            JQGridModel model = new JQGridModel(typeof(VMPanel),false,lstPanel,"");
            return View("DataTable", model);
        }
    }
}
