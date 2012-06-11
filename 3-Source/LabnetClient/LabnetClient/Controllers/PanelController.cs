using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using DataRepository;
using DomainModel;
using DomainModel.Constant;
using LabnetClient.Models;
using LibraryFuntion;

namespace LabnetClient.Controllers
{
    public class PanelController : BaseController
    {
        //
        // GET: /Panel/

        public ActionResult Index()
        {
            PanelSearchViewModel model = new PanelSearchViewModel(null);
            model.Autocomplete.JsonData = Repository.GetPanelNameByName("", SearchTypeEnum.Contains.ToString().ToUpper()).ToJson();
            return View("Search", model);
        }
        //
        // GET: /Panel/Create

        public ActionResult Create()
        {
            PanelViewModel model = new PanelViewModel(new VMPanel(), null);
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
                    if (item.IsEnable)
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
            List<VMTestListItem> testList = Repository.GetPanelTest(id);
            VMPanel panel = Mapper.Map<Panel, VMPanel>(Repository.GetPanel(id));
            PanelViewModel model = new PanelViewModel(panel, testList);
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
            if (Session[SessionProperties.SessionPanelTestList] != null)
            {
                List<VMTestListItem> Rows = (List<VMTestListItem>)Session[SessionProperties.SessionPanelTestList];
                foreach (VMTestListItem item in Rows)
                {
                    PanelItem panelItem = Repository.GetPanelItemByTestIdAndPanelId(item.TestId, id);

                    if (panelItem != null)
                    {
                        if (item.IsEnable == false)
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
                        if (item.IsEnable)
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
            return RedirectToAction("Edit", id);

        }

        [HttpPost]
        public string SavePanelTest(List<VMTestListItem> Rows)
        {
            Session[SessionProperties.SessionPanelTestList] = Rows;
            return "success";
        }

        public ActionResult BackToSearch()
        {
            string filterText = string.Empty;
            if (Session[SessionProperties.SessionSearchPanel] != null)
                filterText = (string)Session[SessionProperties.SessionSearchPanel];
            PanelSearchViewModel model = new PanelSearchViewModel(null);
            model.Autocomplete.SelectedText = filterText;
            return View("Search", model);
        }

        [HttpPost]
        public ActionResult Search(string filterText, bool isActive)
        {
            Session[SessionProperties.SessionSearchPanel] = filterText;
            List<VMPanel> lstPanel = Mapper.Map<List<Panel>, List<VMPanel>>(Repository.GetPanelByName(filterText, isActive));
            for (int i = 0; i < lstPanel.Count; i++)
            {
                lstPanel[i].Status = lstPanel[i].IsActive ? "Kích Hoạt" : "Chưa Kích Hoạt";
            }
            JQGridModel model = new JQGridModel(typeof(VMPanel), false, lstPanel, "");
            return View("DataTable", model);
        }
    }
}
