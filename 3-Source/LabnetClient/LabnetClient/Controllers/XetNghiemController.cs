using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LabnetClient.Models;
using DomainModel.Constant;
using DomainModel;
using DataRepository;
using AutoMapper;
using LibraryFuntion;

namespace LabnetClient.Controllers
{
    public class XetNghiemController : BaseController
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult FindTestSectionNames(string searchText, string searchType)
        {
            var result = Repository.GetTestSectionByName(searchText, searchType);
            return Json(result);
        }

        //
        // GET: /Test/Create

        public ActionResult Create()
        {
            TestViewModel model = new TestViewModel();
            model.ViewMode = ViewMode.Create;
            model.Autocomplete.JsonData = Repository.GetTestSectionByName("", SearchTypeEnum.Contains.ToString().ToUpper()).ToJson();
            return View("Create", model);
        }

        //
        // POST: /Test/Create

        [HttpPost]
        public ActionResult Create(TestViewModel model)
        {
            if ((model.Test.LowIndex == null && model.Test.HighIndex != null)
                || (model.Test.LowIndex != null && model.Test.HighIndex == null))
            {
                ModelState.AddModelError("Low and High are not valid", Resources.TestStrings.TestCreate_IndexError);
            }

            if (!Repository.IsValidTest(model.Test.Name))
            {
                ModelState.AddModelError("Test name already exists", Resources.TestStrings.Test_Create_NameError);
            }

            if (!ModelState.IsValid)
            {
                model.Autocomplete.JsonData = Repository.GetTestSectionByName("", SearchTypeEnum.Contains.ToString().ToUpper()).ToJson();
                return View("Create", model);
            }

            Test test = Mapper.Map<VMTest, Test>(model.Test);
            Repository.TestInsert(test);

            return RedirectToAction("Create");
        }

        //
        // GET: /Test/Edit/5

        public ActionResult Edit(int id)
        {
            TestViewModel model = new TestViewModel();
            Test test = Repository.GetTest(id);
            TestSection testSection = test.TestSection;
            model.Test = Mapper.Map<Test, VMTest>(test);
            model.ViewMode = ViewMode.Edit;
            model.Autocomplete.JsonData = Repository.GetTestSectionByName("", SearchTypeEnum.Contains.ToString().ToUpper()).ToJson();
            model.Autocomplete.SelectedText = testSection.Name;
            model.Autocomplete.SelectedValue = testSection.Id.ToString();
            return PartialView("Create", model);
        }

        //
        // POST: /Test/Edit/5

        [HttpPost]
        public ActionResult Edit(TestViewModel model)
        {
            if ((model.Test.LowIndex == null && model.Test.HighIndex != null)
                || (model.Test.LowIndex != null && model.Test.HighIndex == null))
            {
                ModelState.AddModelError("Low and High are not valid", Resources.TestStrings.TestCreate_IndexError);
            }

            if (!ModelState.IsValid)
            {
                model.Autocomplete.JsonData = Repository.GetTestSectionByName("", SearchTypeEnum.Contains.ToString().ToUpper()).ToJson();
                model.ViewMode = ViewMode.Edit;
                return View("Create", model);
            }

            Test test = Mapper.Map<VMTest, Test>(model.Test);
            Repository.TestUpdate(model.Test.Id, test);

            TestSearctViewModel modelSearch = new TestSearctViewModel();
            modelSearch.TestSearch.TestName = model.Test.Name;
            return RedirectToAction("Search", "XetNghiem");
        }

        //
        // GET: /Test/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Test/Delete/5

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
            TestSearctViewModel model = new TestSearctViewModel();
            model.TestSearch.ObjSearchResult = new List<TestSearchObject>();
            return View("Search", model);
        }

        //
        // POST: /Test/Search

        [HttpPost]
        public ActionResult SearchTest(TestSearctViewModel model)
        {


            List<SearchTest_Result> lstResult = Repository.TestSearch(model.TestSearch.TestName, model.TestSearch.TestSectionName, model.TestSearch.PanelName);
            List<TestSearchObject> ObjSearchResult = new List<TestSearchObject>();
            foreach (SearchTest_Result item in lstResult)
            {
                TestSearchObject obj = new TestSearchObject();
                obj.TestId = item.Id;
                obj.TestName = item.TestName;
                obj.TestDescription = item.TestDescription;
                obj.TestSectionName = item.TestSectionName;
                obj.TestRange = item.Range;
                obj.TestUnit = item.Unit;
                ObjSearchResult.Add(obj);
            }
            JQGridModel grid = new JQGridModel(typeof(TestSearchObject), false, ObjSearchResult, "");
            return View("DataTable", grid);
            //return RedirectToAction("Index");
        }
         
    }
}
