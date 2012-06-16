using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LabnetClient.Models;
using DomainModel.Constant;
using DataRepository;
using DomainModel;
using AutoMapper;
using LibraryFuntion;

namespace LabnetClient.Controllers
{
    public class NhomXetNghiemController : BaseController
    {
        //
        // GET: /NhomXetNghiem/
        public ActionResult Index()
        {
            TestSectionSearchModel model = new TestSectionSearchModel(null);
            return View("Index", model);
        }

        //
        // GET: /NhomXetNghiem/Create
        public ActionResult Create()
        {
            TestSectionViewModel model = new TestSectionViewModel();
            model.ViewMode = ViewMode.Create;
            return View("Create", model);
        }

        //
        // POST: /NhomXetNghiem/Create
        [HttpPost]
        public ActionResult Create(TestSectionViewModel model)
        {
            if (!Repository.IsValidTestSection(model.TestSection.Name, null))
            {
                ModelState.AddModelError("Test section name already exists", Resources.TestSectionStrings.TestSection_NameError);
            }

            if (!ModelState.IsValid)
            {
                return View("Create", model);
            }

            TestSection ts = Mapper.Map<VMTestSection, TestSection>(model.TestSection);
            Repository.TestSectionInsert(ts);

            return RedirectToAction("Create");
        }

        //
        // GET: /NhomXetNghiem/Edit/5
        public ActionResult Edit(int id)
        {
            TestSectionViewModel model = new TestSectionViewModel();
            VMTestSection testSection = Mapper.Map<TestSection, VMTestSection>(Repository.GetTestSection(id));
            model.ViewMode = ViewMode.Edit;
            model.TestSection = testSection;
            return View("Create", model);
        }

        //
        // POST: /NhomXetNghiem/Edit/5
        [HttpPost]
        public ActionResult Edit(TestSectionViewModel model)
        {
            if (!Repository.IsValidTestSection(model.TestSection.Name, model.TestSection.Id))
            {
                ModelState.AddModelError("Test section name already exists", Resources.TestSectionStrings.TestSection_NameError);
            }

            if (!ModelState.IsValid)
            {
                model.ViewMode = ViewMode.Edit;
                return View("Create", model);
            }
            try
            {
                TestSection ts = Mapper.Map<VMTestSection, TestSection>(model.TestSection);
                Repository.TestSectionUpdate(ts);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Test section name already exists", ex.Message);
                model.ViewMode = ViewMode.Edit;
                return View("Create", model);
            }

        }

        public ActionResult BackToSearch()
        {
            TestSectionSearchModel model = new TestSectionSearchModel(null);
            if (Session[SessionProperties.SessionSearchTestSection] != null)
                model = (TestSectionSearchModel)Session[SessionProperties.SessionSearchTestSection];
            return View("Index", model);
        }

        [HttpPost]
        public ActionResult Search(TestSectionSearchModel filter)
        {
            Session[SessionProperties.SessionSearchTestSection] = filter;
            List<VMTestSection> lstTestSection = Mapper.Map<List<TestSection>, List<VMTestSection>>(Repository.GetTestSectionByNameAndStatus(filter.TestSectionName, filter.IsActive));
            for (int i = 0; i < lstTestSection.Count; i++)
            {
                lstTestSection[i].Status = lstTestSection[i].IsActive ? "Kích Hoạt" : "Chưa Kích Hoạt";
            }
            JQGridModel model = new JQGridModel(typeof(VMTestSection), false, lstTestSection, "");
            return View("DataTable", model);
        }
    }
}
